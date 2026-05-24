using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private List<RuleAssignment> _ruleAssignments;
        private List<PointLatLngAlt> _mission;

        public ElevationProfileValidator(MissionActionsPlugin plugin)
        {
            _plugin = plugin;
            _plannerModule = plugin.Host.MainForm.FlightPlanner;
            _elevationValidationOverlay = new GMapOverlay("Elevation Validation");
            _plugin.Host.MainForm.FlightPlanner.MainMap.Overlays.Add(_elevationValidationOverlay);
            _validationRules = new List<ValidationRule>();
            _ruleAssignments = new List<RuleAssignment>();

            _plannerModule.BUT_loadwpfile.Click -= OnMissionFileLoad;
            _plannerModule.BUT_loadwpfile.Click += OnMissionFileLoad;

            _plannerModule.Commands.Rows.CollectionChanged -= OnMissionEdited;
            _plannerModule.Commands.Rows.CollectionChanged += OnMissionEdited;
            _plannerModule.clearMissionToolStripMenuItem.Click -= OnMissionCleared;
            _plannerModule.clearMissionToolStripMenuItem.Click += OnMissionCleared;
            _mission = _plannerModule.pointlist;
        }

        private void OnMissionFileLoad(object sender, EventArgs e)
        {
            log.Info($"WP file reloaded, clear rules");
            _validationRules.Clear();
            _ruleAssignments.Clear();
            _elevationValidationOverlay.Routes.Clear();
        }
        
        private void OnMissionCleared(object sender, EventArgs e)
        {
            log.Info($"Mission cleared, clear rules");
            _validationRules.Clear();
            _ruleAssignments.Clear();
            _elevationValidationOverlay.Routes.Clear();
        }
        
        private void OnMissionEdited(object sender, CollectionChangeEventArgs e)
        {
            if (_ruleAssignments.Count == 0)
            {
                return;
            }

            log.Info($"Mission edited");
            var row = e.Element as DataGridViewRow;
            if (row == null)
            {
                return;
            }

            if (e.Action == CollectionChangeAction.Add)
            {
                log.Info($"Mission entries: {e.Action}");
                var waypoint = row.Index + 1;
                for (var i = 0; i < _ruleAssignments.Count; i++)
                {
                    if (_ruleAssignments[i].SegmentStart >= waypoint)
                    {
                        _ruleAssignments[i] = new RuleAssignment
                        {
                            SegmentStart = _ruleAssignments[i].SegmentStart + 1,
                            SegmentEnd = _ruleAssignments[i].SegmentEnd + 1,
                            Rule = _ruleAssignments[i].Rule

                        };
                    }
                    
                    if (_ruleAssignments[i].SegmentStart < waypoint && _ruleAssignments[i].SegmentEnd >= waypoint)
                    {
                        _ruleAssignments[i] = new RuleAssignment
                        {
                            SegmentStart = _ruleAssignments[i].SegmentStart,
                            SegmentEnd = _ruleAssignments[i].SegmentEnd + 1,
                            Rule = _ruleAssignments[i].Rule

                        };
                    }
                }
                _elevationValidationOverlay.Routes.Clear();
            }
            
            if (e.Action == CollectionChangeAction.Refresh)
            {
                log.Info($"Mission entries: {e.Action}");
                
                var waypoint = row.Index + 1;
                // if (_plannerModule.pointlist[waypoint] == null)
                // {
                //     for (var i = 0; i < _ruleAssignments.Count; i++)
                //     {
                //         if (_ruleAssignments[i].SegmentStart >= waypoint)
                //         {
                //         }
                //     }
                // } 
                _validationRules.Clear();
                _ruleAssignments.Clear();
                _elevationValidationOverlay.Routes.Clear();
            }
            
            if (e.Action == CollectionChangeAction.Remove)
            {
                log.Info($"Mission entries: {e.Action}");
                _validationRules.Clear();
                _ruleAssignments.Clear();
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
                    waypoints.Add(int.Parse(mission[i].Tag));
                }
            }
            if (!waypoints.Any())
            {
                CustomMessageBox.Show("No valid route loaded!");
                return;
            }

            var waypointsStr = waypoints.Select(i => $"{i}").Aggregate((s, s1) => $"{s}, {s1}");
            log.Info($"Mission waypoints: {waypointsStr}");

            var form = new AltitudeValidationParamForm(waypoints, _validationRules, _ruleAssignments);
            _validationRules = form.Rules;
            _ruleAssignments = form.RulesAssignments;
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            var validationRuleAssignments = new Queue<RuleAssignment>(_ruleAssignments);
            var routeWaypoints = mission
                .Where(point => point != null && point.Tag != "H")
                .Select(point => (point, int.Parse(point.Tag)))
                .ToList();

            var missionRoute =
                new GMapRoute(routeWaypoints.Select(tuple => new PointLatLng(tuple.point.Lat, tuple.point.Lng)), "");
            var routePen = new Pen(new SolidBrush(Color.DarkSeaGreen), 5f);
            routePen.LineJoin = LineJoin.Round;
            routePen.StartCap = LineCap.Round;
            routePen.EndCap = LineCap.Round;
            missionRoute.Stroke = routePen;
            _elevationValidationOverlay.Routes.Add(missionRoute);

            var assignedRule = validationRuleAssignments.Dequeue();
           
            var segments = new List<ValidatedSegment>();
            for (var i = 1; i < routeWaypoints.Count; i++)
            {
                
                var (segmentStartCoord, startWaypoint) = routeWaypoints[i-1];
                var (segmentEndCoord, endWaypoint) = routeWaypoints[i];

                if (assignedRule.SegmentStart > startWaypoint)
                {
                    segments.Add(new ValidatedSegment
                    {
                        Start = new PointLatLng(segmentStartCoord.Lat, segmentStartCoord.Lng),
                        End = new PointLatLng(segmentEndCoord.Lat, segmentEndCoord.Lng),
                        Result = ValidationResult.IGNORED
                    });
                    continue;
                }

                var dist = segmentEndCoord.GetDistance(segmentStartCoord);

                var points = (int)(dist / 10) + 1;

                var dLat = segmentEndCoord.Lat - segmentStartCoord.Lat;
                var dLng = segmentEndCoord.Lng - segmentStartCoord.Lng;
                var dAlt = segmentEndCoord.Alt - segmentStartCoord.Alt;

                var latStep = dLat / points;
                var lngStep = dLng / points;
                var altStep = dAlt / points;
                
                var terrainElevation = srtm.getAltitude(segmentStartCoord.Lat, segmentStartCoord.Lng).alt;
                var overlimitDegree = assignedRule.Rule.OverlimitDegree(segmentStartCoord.Alt, terrainElevation);
                var validationResult = ToValidationResult(overlimitDegree);
                var segment = new ValidatedSegment
                {
                    Start = new PointLatLng(segmentStartCoord.Lat, segmentStartCoord.Lng),
                    Result = validationResult
                };
                
                for (var j = 1; j < points; j++)
                {
                    var lat = segmentStartCoord.Lat + latStep * j;
                    var lng = segmentStartCoord.Lng + lngStep * j;
                    var alt = segmentStartCoord.Alt + altStep * j;

                    terrainElevation = srtm.getAltitude(lat, lng).alt;
                    overlimitDegree = assignedRule.Rule.OverlimitDegree(alt, terrainElevation);

                    validationResult = ToValidationResult(overlimitDegree);

                    if (validationResult != segment.Result)
                    {
                        segment.End = new PointLatLng(lat, lng);
                        segments.Add(segment);
                        
                        segment = new ValidatedSegment
                        {
                            Start = new PointLatLng(lat, lng),
                            End = new PointLatLng(lat + latStep, lng + lngStep),
                            Result = validationResult
                        };
                    }
                    else
                    {
                        segment.End = new PointLatLng(lat, lng);
                    }
                }

                segments.Add(segment);

                if (endWaypoint >= assignedRule.SegmentEnd && validationRuleAssignments.Any())
                {
                    assignedRule = validationRuleAssignments.Dequeue();
                }
                else if (endWaypoint >= assignedRule.SegmentEnd)
                {
                    assignedRule = new RuleAssignment
                    {
                        SegmentStart = routeWaypoints.Last().Item2 + 1
                    };
                }
            }

            foreach (var validatedSegment in segments)
            {
                var color = Color.DarkSeaGreen;
                switch (validatedSegment.Result)
                {
                    case ValidationResult.TARGET_MISSED:
                        color = Color.Orange;
                        break;
                    case ValidationResult.MIN_CRITICAL:
                        color = Color.Crimson;
                        break;
                    case ValidationResult.IGNORED:
                        color = Color.DimGray;
                        break;
                    case ValidationResult.MAX_CRITICAL:
                        color = Color.DodgerBlue;
                        break;
                    case ValidationResult.VALID:
                        break;
                }
                
                var routeSegment = new GMapRoute(new[] { validatedSegment.Start, validatedSegment.End }, "");
                var pen = new Pen(new SolidBrush(color), 8f);
                pen.LineJoin = LineJoin.Round;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                routeSegment.Stroke = pen;

                _elevationValidationOverlay.Routes.Add(routeSegment);
            }

            _plugin.Host.MainForm.FlightPlanner.MainMap.Invalidate();
        }

        private static ValidationResult ToValidationResult(double overlimitDegree)
        {
            ValidationResult validationResult = ValidationResult.IGNORED;
            if (overlimitDegree < 0.0 && overlimitDegree >= -0.5)
            {
                validationResult = ValidationResult.TARGET_MISSED;
            }
            else if (overlimitDegree < 0.0)
            {
                validationResult = ValidationResult.MIN_CRITICAL;
            }
            else if (overlimitDegree >= 0.0 && overlimitDegree <= 0.5)
            {
                validationResult = ValidationResult.VALID;
            }
            else if (overlimitDegree > 0.5)
            {
                validationResult = ValidationResult.MAX_CRITICAL;
            }

            return validationResult;
        }

        private List<(int, PointLatLngAlt)> LoadValue()
        {
            return WaypointUtils.FilterMissionWaypoints(_plannerModule.pointlist);
        }
    }

    public struct ValidatedSegment
    {
        public PointLatLng Start { get; set; }
        public PointLatLng End { get; set; }
        public ValidationResult Result { get; set; }

        private bool _notEmpty;

        public ValidatedSegment(PointLatLng start, PointLatLng end, ValidationResult result) : this()
        {
            Start = start;
            End = end;
            Result = result;
            _notEmpty = true;
        }
        
        public bool IsEmpty => !_notEmpty;

        public static ValidatedSegment Empty = new ValidatedSegment();
    }
    
    public enum ValidationResult
    {
        UNKNOWN,
        IGNORED,
        VALID,
        TARGET_MISSED,
        MIN_CRITICAL,
        MAX_CRITICAL
    }
}