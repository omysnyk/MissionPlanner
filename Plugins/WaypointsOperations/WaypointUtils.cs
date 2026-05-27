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
        
        public static void CeilKey<T>(SortedList<int, T> map, int key, out T value)
        {
            int lo = 0;
            int hi = map.Count - 1;

            while (lo <= hi)
            {
                int mid = lo + ((hi - lo) / 2);

                int midKey = map.Keys[mid];

                if (midKey == key)
                {
                    value = map.Values[mid];
                    return;
                }

                if (midKey < key)
                    lo = mid + 1;
                else
                    hi = mid - 1;
            }

            // lo now points to ceil element
            if (lo < map.Count)
            {
                value = map.Values[lo];
                return;
            }

            value = default;
        }

        public static void FloorKey<T>(SortedList<int, T> map, int key, out T value)
        {
            var lo = 0;
            var hi = map.Count - 1;

            while (lo <= hi)
            {
                var mid = lo + ((hi - lo) / 2);

                var midKey = map.Keys[mid];

                if (midKey == key)
                {
                    value = map.Values[mid];
                    return;
                }

                if (midKey < key)
                    lo = mid + 1;
                else
                    hi = mid - 1;
            }

            // hi now points to floor element
            if (hi >= 0)
            {
                value = map.Values[hi];
                return;
            }

            value = default;
        }
    }
}