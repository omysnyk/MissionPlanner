using System.Collections.Generic;

namespace MissionActionsPlugin
{
    public static class ListExtensions
    {
        public static int GetOrFloor(this List<int> sortedList, int key)
        {
            var index = sortedList.BinarySearch(key);

            if (index >= 0)
                return sortedList[index];

            var floorIndex = ~index - 1;

            if (floorIndex < 0)
                return -1;

            return sortedList[floorIndex];
        }
    }
}