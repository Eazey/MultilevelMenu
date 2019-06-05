using System.Collections.Generic;
using UnityEngine;

namespace EazeyFramework.UI
{
    public class MenuData
    {
        private const int BufferSize = 16;

        public readonly int DataId;
        public readonly int sortOrder;
        public Dictionary<int, MenuData> ChildsMap;

        public MenuData(int id, Dictionary<int, MenuData> childs = null)
        {
            DataId = id;
            ChildsMap = childs;
        }

        public void AddChild(int dataId)
        {
            if (ChildsMap == null)
                ChildsMap = new Dictionary<int, MenuData>();

            if (!ChildsMap.ContainsKey(dataId))
            {
                var child = new MenuData(dataId);
                ChildsMap.Add(dataId, child);
            }
            else
            {
                Debug.LogWarning("已存在id为" + dataId + "的对象");
            }

        }

        public void AddGrandson(int childID, int dataId)
        {
            if (ChildsMap == null)
                ChildsMap = new Dictionary<int, MenuData>();

            if (ChildsMap.ContainsKey(childID))
            {
                var child = ChildsMap[childID];
                child.AddChild(dataId);
            }
            else
            {
                AddChild(childID);
                AddGrandson(childID, dataId);
            }
        }

        public List<MenuData> GetChildList()
        {
            List<MenuData> list = null;

            if (ChildsMap == null || ChildsMap.Count < 1)
                return list;

            list = new List<MenuData>(BufferSize);
            for (var item = ChildsMap.Values.GetEnumerator(); item.MoveNext();)
            {
                var child = item.Current;
                list.Add(child);
            }

            return list;
        }

        public List<int> GetChildIDList()
        {
            List<int> list = null;

            if (ChildsMap == null || ChildsMap.Count < 1)
                return list;

            list = new List<int>(BufferSize);
            for (var item = ChildsMap.Values.GetEnumerator(); item.MoveNext();)
            {
                var child = item.Current;
                list.Add(child.DataId);
            }

            return list;
        }

        public virtual int GetHashCode()
        {
            return DataId;
        }
    }
}
