using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GeographicLib;
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

        public Plugin WaypointsPlugin;

        public ApproachBuilder(Plugin waypointsPlugin)
        {
            WaypointsPlugin = waypointsPlugin;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public void AddApproachPoints()
        {
            var approachAlt = 60;
            if (InputBox.Show("Approach Parameters", "Select approach altitude above target", ref approachAlt) !=
                DialogResult.OK)
                return;

            if (!ApproachDescentAngles.TryGetValue(approachAlt, out var approachDescentAngle))
            {
                CustomMessageBox.Show("Unsupported approach altitude.",
                    "Approach calculate error", CustomMessageBox.MessageBoxButtons.OK,
                    CustomMessageBox.MessageBoxIcon.Error);
                return;
            }

            var flightPlanner = MainV2.instance.FlightPlanner;
            var commands = flightPlanner.Commands;

            var routePoints = flightPlanner.pointlist;
            var routeLastPoints = (routePoints.AsEnumerable() ?? Array.Empty<PointLatLngAlt>())
                .Reverse()
                .Where(point => point != null && point.Tag != "H")
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

            var endIndex = routePoints.Count(alt => alt?.Tag != "H") - 1; // exclude home if present
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
            var distFromEnd = Math.Max(0, line.Distance - 2000);
            var newPoint = line.Position(distFromEnd);
            var aimAlt = lastPoint.Alt + approachAlt;

            flightPlanner.CHK_verifyheight.CheckState = CheckState.Unchecked;
            flightPlanner.CHK_verifyheight.Checked = false;
            WaypointsPlugin.Host.InsertWP(endIndex, MAVLink.MAV_CMD.WAYPOINT, 0, 0, 0, 0,
                newPoint.Longitude, newPoint.Latitude,
                aimAlt);

            distFromEnd = Math.Max(0, line.Distance - 1750);
            newPoint = line.Position(distFromEnd);
            WaypointsPlugin.Host.InsertWP(endIndex + 1, MAVLink.MAV_CMD.DO_CHANGE_SPEED, 0, 42, 0, 0,
                newPoint.Longitude, newPoint.Latitude,
                aimAlt);

            distFromEnd = Math.Max(0, line.Distance - 1500);
            newPoint = line.Position(distFromEnd);
            WaypointsPlugin.Host.InsertWP(endIndex + 2, MAVLink.MAV_CMD.WAYPOINT, 0, 0, 0, 0,
                newPoint.Longitude, newPoint.Latitude,
                aimAlt);

            var aimPointDistance = approachAlt * 1.0 / Math.Tan(ToRadians(approachDescentAngle));
            distFromEnd = Math.Max(0, line.Distance - aimPointDistance);
            newPoint = line.Position(distFromEnd);
            WaypointsPlugin.Host.InsertWP(endIndex + 3, MAVLink.MAV_CMD.WAYPOINT, 0, 0, 0, 0,
                newPoint.Longitude, newPoint.Latitude,
                aimAlt);

            flightPlanner.CHK_verifyheight.Checked = verifyCheckerState;
        }
    }
}