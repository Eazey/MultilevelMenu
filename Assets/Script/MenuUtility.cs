using System.Collections;
using System.Collections.Generic;
using EazeyFramework.UI;
using UnityEngine;

namespace EazeyFramework.Utility
{
    public static class MenuUtility
    {
        private static int _ascendSortWeight;
        private const int ASCEND = 1;
        private const int DESCEND = -1;

        public static void Sort<T>(ref List<T> list, bool ascend = true)
            where T : MenuData
        {
            if (list != null && list.Count > 1)
            {
                _ascendSortWeight = ascend ? ASCEND : DESCEND;
                list.Sort(Comparison);
            }
        }

        public static void Reset(this Transform trans)
        {
            if (trans == null) 
                return;

            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = Vector3.one;
        }

        private static int Comparison<T>(T itemA, T itemB)
            where T : MenuData
        {
            int result = 0;

            if (itemA.SortOrder > itemB.SortOrder)
                result = _ascendSortWeight;
            else if (itemA.SortOrder < itemB.SortOrder)
                result = _ascendSortWeight * -1;

            return result;
        }
    }
}
