using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using log4net;
using MissionPlanner.GCSViews;
using MissionPlanner.Utilities;
using CustomMessageBox = MissionPlanner.MsgBox.CustomMessageBox;

namespace MissionActionsPlugin
{
    internal class ElevationProfileValidator
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        
        private readonly GMapOverlay _elevationValidationOverlay;
        private readonly MissionActionsPlugin _plugin;
        private readonly FlightPlanner _plannerModule;
        private List<ValidationRule> _validationRules;
        private List<PointLatLngAlt> _mission;

        public ElevationProfileValidator(MissionActionsPlugin plugin)
        {
            _plugin = plugin;
            _plannerModule = plugin.Host.MainForm.FlightPlanner;
            _elevationValidationOverlay = new GMapOverlay("Elevation Validation");
            _plugin.Host.MainForm.FlightPlanner.MainMap.Overlays.Add(_elevationValidationOverlay);
            _validationRules = new List<ValidationRule>();

            _plannerModule.BUT_loadwpfile.Click -= OnMissionFileLoad;
            _plannerModule.BUT_loadwpfile.Click += OnMissionFileLoad;

            _plannerModule.Commands.Rows.CollectionChanged -= OnMissionEdited;
            _plannerModule.Commands.Rows.CollectionChanged += OnMissionEdited;
            _mission = _plannerModule.pointlist;
        }

        private void OnMissionFileLoad(object sender, EventArgs e)
        {
            log.Info($"WP file reloaded, clear rules");
            _validationRules.Clear();
            _elevationValidationOverlay.Routes.Clear();
        }
        
        private void OnMissionEdited(object sender, CollectionChangeEventArgs e)
        {
            log.Info($"Mission edited");
            if (e.Action == CollectionChangeAction.Add || e.Action == CollectionChangeAction.Remove)
            {
                log.Info($"Mission entries: {e.Action}");
                _validationRules = new List<ValidationRule>();
                _elevationValidationOverlay.Routes.Clear();
            }
        }
        
        public void ValidateElevationProfile()
        {
            _elevationValidationOverlay.Routes.Clear();
            var mission = _plannerModule.pointlist;
            var missionStr = mission.Select(p => p == null ? "[]" : $"{p.Tag}").Aggregate((s, s1) => $"{s}, {s1}");
            log.Info($"Mission items: {missionStr}");
            var waypoints = new List<int>();
            for (var i = 0; i < mission?.Count; i++)
            {
                if (mission[i] != null && mission[i].Tag != "H")
                {
                    waypoints.Add(i);
                }
            }

            var waypointsStr = waypoints.Select(i => $"{i}").Aggregate((s, s1) => $"{s}, {s1}");
            log.Info($"Mission waypoints: {waypointsStr}");
            if (!waypoints.Any())
            {
                CustomMessageBox.Show("No valid route loaded!");
                return;
            }

            var form = new AltitudeValidationParamForm(waypoints, _validationRules);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _validationRules = form.Rules;
            var validationRules = new Queue<ValidationRule>(_validationRules);
            var routeWaypoints = mission.Select((point, index) => (point, index))
                .Where(tuple => tuple.point != null && tuple.point.Tag != "H")
                .ToList();

            var missionRoute =
                new GMapRoute(routeWaypoints.Select(tuple => new PointLatLng(tuple.point.Lat, tuple.point.Lng)), "");
            var routePen = new Pen(new SolidBrush(Color.DarkSeaGreen), 5f);
            routePen.LineJoin = LineJoin.Round;
            routePen.StartCap = LineCap.Round;
            routePen.EndCap = LineCap.Round;
            missionRoute.Stroke = routePen;
            _elevationValidationOverlay.Routes.Add(missionRoute);

            var (segmentStartCoord, startWaypoint) = routeWaypoints[0];
            var validationRule = validationRules.Dequeue();

            var invalidSections = new List<(PointLatLng, PointLatLng)>();
            for (var i = 1; i < routeWaypoints.Count - 1; i++)
            {
                var (segmentEndCoord, endWaypoint) = routeWaypoints[i];
                var dist = segmentEndCoord.GetDistance(segmentStartCoord);

                var points = (int)(dist / 100) + 1;

                var dLat = segmentEndCoord.Lat - segmentStartCoord.Lat;
                var dLng = segmentEndCoord.Lng - segmentStartCoord.Lng;
                var dAlt = segmentEndCoord.Alt - segmentStartCoord.Alt;

                var latStep = dLat / points;
                var lngStep = dLng / points;
                var altStep = dAlt / points;

                PointLatLng invalidStart = PointLatLng.Empty;
                PointLatLng invalidEnd = PointLatLng.Empty;
                for (var j = 0; j < points; j++)
                {
                    var lat = segmentStartCoord.Lat + latStep * j;
                    var lng = segmentStartCoord.Lng + lngStep * j;
                    var alt = segmentStartCoord.Alt + altStep * j;

                    var terrainElevation = srtm.getAltitude(lat, lng).alt;
                    var overlimitDegree = validationRule.OverlimitDegree(alt, terrainElevation);
                    if (overlimitDegree > 0.0)
                    {
                        if (invalidStart.IsEmpty)
                        {
                            invalidStart = new PointLatLng(lat, lng);
                        }
                        else
                        {
                            invalidEnd = new PointLatLng(lat, lng);
                        }
                    }
                    else
                    {
                        if (!invalidStart.IsEmpty && !invalidEnd.IsEmpty)
                        {
                            invalidSections.Add((invalidStart, invalidEnd));
                        }

                        invalidStart = PointLatLng.Empty;
                        invalidEnd = PointLatLng.Empty;
                    }
                }

                if (endWaypoint >= validationRule.SegmentEnd)
                {
                    validationRule = validationRules.Dequeue();
                }

                if (!invalidStart.IsEmpty && !invalidEnd.IsEmpty)
                {
                    invalidSections.Add((invalidStart, invalidEnd));
                }

                (segmentStartCoord, startWaypoint) = routeWaypoints[i];
            }

            Console.Out.WriteLine($"Found {invalidSections.Count} invalid intervals");

            foreach (var invalidSection in invalidSections)
            {
                var invalidRouteSegment = new GMapRoute(new[] { invalidSection.Item1, invalidSection.Item2 }, "");
                var pen = new Pen(new SolidBrush(Color.Orange), 8f);
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                invalidRouteSegment.Stroke = pen;

                _elevationValidationOverlay.Routes.Add(invalidRouteSegment);
            }

            _plugin.Host.MainForm.FlightPlanner.MainMap.Invalidate();
        }
    }
}