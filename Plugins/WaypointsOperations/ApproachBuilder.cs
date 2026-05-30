using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GeographicLib;
using MissionActionsPlugin;
using MissionPlanner;
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
                    waypoints.Add(i);
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

            var routeNavigablePoints = flightPlanner.pointlist
                .Select((point, i) => (waypoint: point, cmdIndex: i - 1))
                .Where(t => t.waypoint != null && t.waypoint.Tag != "H")
                .ToList();

            if (routeNavigablePoints.Count < 2)
            {
                CustomMessageBox.Show("At least two waypoints needed at the end of a mission!",
                    "Approach calculate error", CustomMessageBox.MessageBoxButtons.OK,
                    CustomMessageBox.MessageBoxIcon.Error);
                return;
            }

            var lastSegmentEndCmdIndex = routeNavigablePoints[routeNavigablePoints.Count - 1].cmdIndex;
            var lastPoint = routeNavigablePoints[routeNavigablePoints.Count - 1].waypoint;
            var lastSegmentStartCmdIndex = routeNavigablePoints[routeNavigablePoints.Count - 2].cmdIndex;
            var nextToLastPoint = routeNavigablePoints[routeNavigablePoints.Count - 2].waypoint;

            var commands = flightPlanner.Commands;

            var line = Geod.InverseLine(nextToLastPoint.Lat, nextToLastPoint.Lng, lastPoint.Lat, lastPoint.Lng);
            if (line.Distance < 2000)
            {
                CustomMessageBox.Show("Unable to build approach on route leg less then 2000 meters.",
                    "Approach calculate error", CustomMessageBox.MessageBoxButtons.OK,
                    CustomMessageBox.MessageBoxIcon.Warning);
                return;
            }

            var lastCommandRow = commands.Rows[lastSegmentEndCmdIndex];
            lastCommandRow.Cells[0].Value = nameof(MAVLink.MAV_CMD.LAND);
            lastCommandRow.Cells[7].Value = lastPoint.Alt;

            var verifyCheckerState = flightPlanner.CHK_verifyheight.Checked;
            var aimAlt = lastPoint.Alt + approachAlt;

            flightPlanner.CHK_verifyheight.CheckState = CheckState.Unchecked;
            flightPlanner.CHK_verifyheight.Checked = false;

            int insertIndex;
            if (approachParametersForm.ChangeSpeedBeforeApproach)
            {
                insertIndex = TryInsertCommandAtDistance(line, routeNavigablePoints,
                    lastSegmentStartCmdIndex, aimAlt,
                    approachParametersForm.ChangeSpeedActivationDistance * 1000, MAVLink.MAV_CMD.DO_CHANGE_SPEED, 42,
                    0);
                if (lastSegmentEndCmdIndex >= insertIndex)
                {
                    lastSegmentEndCmdIndex += 2;
                }
                
                if (lastSegmentStartCmdIndex >= insertIndex)
                {
                    lastSegmentStartCmdIndex += 2;
                }

                routeNavigablePoints = flightPlanner.pointlist
                    .Select((point, i) => (waypoint: point, cmdIndex: i - 1))
                    .Where(t => t.waypoint != null && t.waypoint.Tag != "H")
                    .ToList();
            }

            var trackerApproachActivationDistance = approachParametersForm.TrackerApproachActivationDistance * 1000;
            insertIndex = TryInsertCommandAtDistance(line, routeNavigablePoints, lastSegmentStartCmdIndex, aimAlt, 
                trackerApproachActivationDistance, MAVLink.MAV_CMD.DO_SET_SERVO, 2000, 16);
            
            if (lastSegmentEndCmdIndex >= insertIndex)
            {
                lastSegmentEndCmdIndex += 2;
            }
            
            if (lastSegmentStartCmdIndex >= insertIndex)
            {
                lastSegmentStartCmdIndex += 2;
            }

            InsertWaypointAtLine(lastSegmentEndCmdIndex, 2000, aimAlt, line);
            lastSegmentEndCmdIndex++;
            if (!approachParametersForm.ChangeSpeedBeforeApproach)
            {
                InsertCmdAtLine(index: lastSegmentEndCmdIndex, aimAlt: aimAlt, distance: 1750, line: line,
                    mavCmd: MAVLink.MAV_CMD.DO_CHANGE_SPEED, p2: 42);
                lastSegmentEndCmdIndex++;
            }
            
            InsertWaypointAtLine(lastSegmentEndCmdIndex, 1500, aimAlt, line);
            lastSegmentEndCmdIndex++;
            var aimPointDistance = approachAlt * 1.0 / Math.Tan(ToRadians(approachDescentAngle));
            InsertWaypointAtLine(lastSegmentEndCmdIndex, aimPointDistance, aimAlt, line);

            flightPlanner.CHK_verifyheight.Checked = verifyCheckerState;
        }

        private int TryInsertCommandAtDistance(IGeodesicLine lastLeg,
            List<(PointLatLngAlt waypoint, int cmdIndex)> routeNavigablePoints,
            int lastSegmentStartCmdIndex,
            double aimAlt,
            float dist, MAVLink.MAV_CMD cmd, int p2, int p1)
        {
            var totalDistFromEnd = lastLeg.Distance;
            var index = routeNavigablePoints.Count - 3;
            var routeSegmentLine = lastLeg;

            while (totalDistFromEnd < dist && index >= 0)
            {
                var waypoint = routeNavigablePoints[index].waypoint;
                var nextWaypoint = routeNavigablePoints[index + 1].waypoint;
                routeSegmentLine = Geod.InverseLine(waypoint.Lat, waypoint.Lng, nextWaypoint.Lat, nextWaypoint.Lng);
                totalDistFromEnd += routeSegmentLine.Distance;
                index--;
            }

            if (index < 0 && totalDistFromEnd < dist)
            {
                CustomMessageBox.Show("Unable to place tracker approach activation command.",
                    "Approach build warning", CustomMessageBox.MessageBoxButtons.OK,
                    CustomMessageBox.MessageBoxIcon.Warning);
                return -1;
            }

            var insertPosition = routeNavigablePoints[index + 1];
            var nextPosition = routeNavigablePoints[index + 2];
            var deltaAlt = (nextPosition.waypoint.Alt - insertPosition.waypoint.Alt) / routeSegmentLine.Distance;
            if (insertPosition.cmdIndex == lastSegmentStartCmdIndex)
            {
                deltaAlt = (aimAlt - insertPosition.waypoint.Alt) / (routeSegmentLine.Distance - 2000);
            }

            var distFromEnd = dist - (totalDistFromEnd - routeSegmentLine.Distance);
            var waypointAlt = insertPosition.waypoint.Alt + deltaAlt * (routeSegmentLine.Distance - distFromEnd);

            InsertWaypointAtLine(insertPosition.cmdIndex + 1, distFromEnd, waypointAlt, routeSegmentLine);
            InsertCmdAtLine(index: insertPosition.cmdIndex + 2,
                aimAlt: waypointAlt,
                distance: routeSegmentLine.Distance / 2,
                line: routeSegmentLine,
                mavCmd: cmd, p2: p2, p1: p1);

            return insertPosition.cmdIndex;
        }

        private void InsertWaypointAtLine(int index, double distance, double aimAlt, IGeodesicLine line)
        {
            var distFromEnd = Math.Max(0, line.Distance - distance);
            var newPoint = line.Position(distFromEnd);
            _plugin.Host.InsertWP(index, MAVLink.MAV_CMD.WAYPOINT, 0, 0, 0, 0,
                newPoint.Longitude, newPoint.Latitude,
                aimAlt);
        }

        private void InsertCmdAtLine(int index, double distance, double aimAlt, IGeodesicLine line,
            MAVLink.MAV_CMD mavCmd, int p1 = 0, int p2 = 0)
        {
            var distFromEnd = Math.Max(0, line.Distance - distance);
            var newPoint = line.Position(distFromEnd);
            _plugin.Host.InsertWP(index, mavCmd, p1, p2, 0, 0,
                newPoint.Longitude, newPoint.Latitude,
                aimAlt);
        }
    }
}