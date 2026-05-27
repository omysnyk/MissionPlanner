using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GeographicLib;
using MissionActionsPlugin;
using MissionPlanner;
using MissionPlanner.Controls;
using MissionPlanner.Plugin;
using MissionPlanner.Utilities;

namespace WaypointsOperations
{
    internal class ApproachBuilder
    {
        private static readonly Geodesic Geod = Geodesic.WGS84;

        private static readonly Dictionary<int, int> ApproachDescentAngles = new Dictionary<int, int>
        {
            { 40, 18 },
            { 60, 23 },
            { 80, 26 },
            { 100, 30 }
        };

        private readonly Plugin _plugin;

        public ApproachBuilder(Plugin waypointsPlugin)
        {
            _plugin = waypointsPlugin;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public void AddApproachPoints()
        {
            var flightPlanner = MainV2.instance.FlightPlanner;
            var waypoints = new List<int>();

            if (flightPlanner.pointlist == null)
            {
                return;
            }

            for (var i = 0; i < flightPlanner.pointlist.Count; i++)
            {
                if (flightPlanner.pointlist[i] != null && flightPlanner.pointlist[i].Tag != "H")
                {
                    waypoints.Add(int.Parse(flightPlanner.pointlist[i].Tag));
                }
            }
            var approachParametersForm = new ApproachParametersForm(waypoints);
            if (approachParametersForm.ShowDialog() != DialogResult.OK)
                return;

            var approachAlt = approachParametersForm.ApproachAltitude;
            if (!ApproachDescentAngles.TryGetValue(approachAlt, out var approachDescentAngle))
            {
                CustomMessageBox.Show("Unsupported approach altitude.",
                    "Approach calculate error", CustomMessageBox.MessageBoxButtons.OK,
                    CustomMessageBox.MessageBoxIcon.Error);
                return;
            }

            
            var commands = flightPlanner.Commands;

            var routePoints = flightPlanner.pointlist.Where(point => point != null && point.Tag != "H").ToList();
            var routeLastPoints = routePoints?.Select(point => point)
                .Reverse()
                .Take(2)
                .Reverse();

            var finalApproachVector = routeLastPoints as PointLatLngAlt[] ?? routeLastPoints.ToArray();
            if (finalApproachVector.Length < 2)
            {
                CustomMessageBox.Show("At least two waypoints needed at the end of a mission!",
                    "Approach calculate error", CustomMessageBox.MessageBoxButtons.OK,
                    CustomMessageBox.MessageBoxIcon.Error);
                return;
            }

            var endIndex = flightPlanner.pointlist.Count(alt => alt?.Tag != "H") - 1; // exclude home if present
            var lastPoint = finalApproachVector[1];
            var nextToLastPoint = finalApproachVector[0];

            if (lastPoint == null || nextToLastPoint == null)
            {
                CustomMessageBox.Show("Require WAYPOINT/LAND as two last mission command items!",
                    "Error", CustomMessageBox.MessageBoxButtons.OK,
                    CustomMessageBox.MessageBoxIcon.Error);
                return;
            }

            var lastCommandRow = commands.Rows[commands.Rows.Count - 1];
            lastCommandRow.Cells[0].Value = nameof(MAVLink.MAV_CMD.LAND);
            lastCommandRow.Cells[7].Value = lastPoint.Alt;

            var line = Geod.InverseLine(nextToLastPoint.Lat, nextToLastPoint.Lng, lastPoint.Lat, lastPoint.Lng);
            if (line.Distance < 2000)
            {
                CustomMessageBox.Show("Unable to build approach on route leg less then 2000 meters.",
                    "Approach calculate error", CustomMessageBox.MessageBoxButtons.OK,
                    CustomMessageBox.MessageBoxIcon.Warning);
                return;
            }

            var verifyCheckerState = flightPlanner.CHK_verifyheight.Checked;
            var aimAlt = lastPoint.Alt + approachAlt;

            flightPlanner.CHK_verifyheight.CheckState = CheckState.Unchecked;
            flightPlanner.CHK_verifyheight.Checked = false;

            // var totalDistFromEnd = line.Distance;
            // int index = routePoints.Count - 2;
            // var routeSegmentLine = line;
            // var activationDist = approachParametersForm.TrackerApproachActivationDistance * 1000;
            // while (totalDistFromEnd < activationDist && index >= 0)
            // {
            //     routeSegmentLine = Geod.InverseLine(routePoints[index].Lat, routePoints[index].Lng, routePoints[index + 1].Lat, routePoints[index + 1].Lng);
            //     totalDistFromEnd +=  routeSegmentLine.Distance;
            //     index--;
            // }
            // InsertWaypointAtLine(index + 1, activationDist - (totalDistFromEnd - routeSegmentLine.Distance), routePoints[index].Alt, routeSegmentLine);
            // InsertCmdAtLine(index: index + 2, aimAlt: routePoints[index].Alt, distance: totalDistFromEnd - activationDist + 100, line: routeSegmentLine, mavCmd: MAVLink.MAV_CMD.DO_SET_SERVO, p2:2000, p1:16);
            //
            InsertWaypointAtLine(endIndex, 2000, aimAlt, line);
            InsertCmdAtLine(index: endIndex + 2, aimAlt: aimAlt, distance: 1750, line: line, mavCmd: MAVLink.MAV_CMD.DO_CHANGE_SPEED, p2:42);
            InsertWaypointAtLine(endIndex + 3, 1500, aimAlt, line);
            var aimPointDistance = approachAlt * 1.0 / Math.Tan(ToRadians(approachDescentAngle));
            InsertWaypointAtLine(endIndex + 4, aimPointDistance, aimAlt, line);

            flightPlanner.CHK_verifyheight.Checked = verifyCheckerState;
        }

        private void InsertWaypointAtLine(int index, double distance, double aimAlt, IGeodesicLine line)
        {
            var distFromEnd = Math.Max(0, line.Distance - distance);
            var newPoint = line.Position(distFromEnd);
            _plugin.Host.InsertWP(index, MAVLink.MAV_CMD.WAYPOINT, 0, 0, 0, 0,
                newPoint.Longitude, newPoint.Latitude,
                aimAlt);
        }
        
        private void InsertCmdAtLine(int index, double distance, double aimAlt, IGeodesicLine line, MAVLink.MAV_CMD mavCmd, int p1 = 0, int p2 = 0)
        {
            var distFromEnd = Math.Max(0, line.Distance - distance);
            var newPoint = line.Position(distFromEnd);
            _plugin.Host.InsertWP(index, mavCmd, p1, p2, 0, 0,
                newPoint.Longitude, newPoint.Latitude,
                aimAlt);
        }
    }
}