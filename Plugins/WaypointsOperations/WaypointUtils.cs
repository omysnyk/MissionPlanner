using System;
using System.Collections.Generic;
using System.Linq;
using MissionPlanner.Utilities;

namespace MissionActionsPlugin
{
    public class WaypointUtils
    {
        public static List<(int, PointLatLngAlt)> FilterMissionWaypoints(List<PointLatLngAlt> mission)
        {
            var routeWaypointsPoints = (mission.AsEnumerable() ?? Array.Empty<PointLatLngAlt>())
                .Select((point, index) => (index, point))
                .Where(tuple => tuple.point != null && tuple.point.Tag != "H")
                .ToList();

            return routeWaypointsPoints;
        }
    }
}