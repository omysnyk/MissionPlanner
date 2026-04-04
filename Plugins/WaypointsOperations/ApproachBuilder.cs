using BruTile.Wms;
using GeographicLib;
using MissionPlanner;
using MissionPlanner.Controls;
using MissionPlanner.Plugin;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaypointsOperations
{
    internal class ApproachBuilder
    {
        public Plugin WaypointsPlugin;

        public ApproachBuilder(Plugin waypointsPlugin)
        {
            this.WaypointsPlugin = waypointsPlugin;
        }

        static double ToRadians(double degrees) {
            return degrees * Math.PI / 180.0;
        }

        public void AddApproachPoints() {
            var approachAlt = 60;
            if (InputBox.Show("Approach Parameters", "Select approach altitude above target", ref approachAlt) !=
                DialogResult.OK) {
                return;
            }

            var flightPlanner = MainV2.instance.FlightPlanner;
            var commands = flightPlanner.Commands;
            
            var routePoints = flightPlanner.pointlist;
            var routeLastPoints = flightPlanner.pointlist.Skip(routePoints.Count - 2);
            var finalApproachVector = routeLastPoints as PointLatLngAlt[] ?? routeLastPoints.ToArray();
            if (finalApproachVector.Length < 2 || !finalApproachVector.All(point => point != null && point.Tag != "H")) {
                CustomMessageBox.Show("At least two waypoints needed at the end of a mission!",
                    "Approach calculate error", CustomMessageBox.MessageBoxButtons.OK, CustomMessageBox.MessageBoxIcon.Error);
                return;
            }

            var endIndex = routePoints.Count(alt => alt.Tag !="H") - 1; // exclude home if present
            var lastPoint = routePoints[routePoints.Count - 1];
            var nextToLastPoint = routePoints[routePoints.Count - 2];

            /*var allowedCommands = ImmutableHashSet.Create(
                (ushort)MAVLink.MAV_CMD.LAND,
                (ushort)MAVLink.MAV_CMD.WAYPOINT);

            var isLastCommandsValid = new[] { ExtractCmd(lastCommand), ExtractCmd(nextToLastCommand) }.All(val => allowedCommands.Contains(val));/*/
            if (lastPoint == null || nextToLastPoint == null) {
                CustomMessageBox.Show("Require WAYPOINT/LAND as two last mission command items!",
                    "Error", CustomMessageBox.MessageBoxButtons.OK,
                    CustomMessageBox.MessageBoxIcon.Error);
                return;
            }

            // var endCoords = ExtractCoords(lastPoint);
            // var startCoords = ExtractCoords(nextToLastPoint);

            var lastCommandRow = commands.Rows[commands.Rows.Count - 1];
            lastCommandRow.Cells[0].Value = nameof(MAVLink.MAV_CMD.LAND);
            lastCommandRow.Cells[7].Value = lastPoint.Alt;

            var line = Geod.InverseLine(nextToLastPoint.Lat, nextToLastPoint.Lng, lastPoint.Lat,  lastPoint.Lng);
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

            var approachDescentAngle = ApproachDescentAngles[approachAlt];
            var aimPointDistance = approachAlt * 1.0 / Math.Tan(ToRadians(approachDescentAngle));
            distFromEnd = Math.Max(0, line.Distance - aimPointDistance);
            newPoint = line.Position(distFromEnd);
            WaypointsPlugin.Host.InsertWP(endIndex + 3, MAVLink.MAV_CMD.WAYPOINT, 0, 0, 0, 0,
                newPoint.Longitude, newPoint.Latitude,
                aimAlt);


            // var targetApproachAltAsl = (int)Math.Round(srtm.getAltitude(newPoint.Latitude, newPoint.Longitude).alt);
            flightPlanner.CHK_verifyheight.Checked = verifyCheckerState;

            // var intervalsCount = (int)Math.Ceiling(line.Distance / 10.0);
            // for (int i = 0; i < intervalsCount; i++) {
            //     var probeCoord = line.Position(Math.Min(line.Distance, 10 * intervalsCount));
            // }
        }

        private static readonly Geodesic Geod = Geodesic.WGS84;
        private static readonly Dictionary<int, int> ApproachDescentAngles = new Dictionary<int, int> {
            { 40, 18 },
            { 60, 23 },
            { 80, 26 },
            { 100, 30 }
        };

        private static (double lat, double lon, double alt) ExtractCoords(DataGridViewRow lastCommand) {
            var lat = double.Parse(lastCommand.Cells[5].Value.ToString());
            var lon = double.Parse(lastCommand.Cells[6].Value.ToString());
            var alt = double.Parse(lastCommand.Cells[7].Value.ToString()) / CurrentState.multiplieralt;

            return (lat: lat, lon: lon, alt: alt);
        }


        private ushort ExtractCmd(DataGridViewRow row) {
            return MainV2.instance.FlightPlanner.getCmdID(row.Cells[0].Value.ToString());
        }
    }
}
