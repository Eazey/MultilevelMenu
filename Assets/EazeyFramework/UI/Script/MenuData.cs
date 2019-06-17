using System.Collections.Generic;
using UnityEngine;

namespace EazeyFramework.UI
{
    public class MenuData
    {
        private const int BufferSize = 16;

        public readonly int Id;
        public readonly int SortOrder;
        public Dictionary<int, MenuData> ChildsMap;

        public int ChildCount
        {
            get
            {
                if (ChildsMap != null)
                    return ChildsMap.Count;
                return 0;
            }
        }

        public MenuData(int id, int sortOrder = -1, Dictionary<int, MenuData> childs = null)
        {
            Id = id;
            SortOrder = sortOrder;
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
        }

        public void AddGrandson(int childId, int dataId)
        {
            if (ChildsMap == null)
                ChildsMap = new Dictionary<int, MenuData>();

            if (ChildsMap.ContainsKey(childId))
            {
                var child = ChildsMap[childId];
                child.AddChild(dataId);
            }
            else
            {
                AddChild(childId);
                AddGrandson(childId, dataId);
            }
        }

        public List<MenuData> GetChildList()
        {
            if (ChildsMap == null || ChildsMap.Count < 1)
                return null;

            List<MenuData> list = new List<MenuData>(BufferSize);
            
            using (var item = ChildsMap.Values.GetEnumerator())
            {
                while (item.MoveNext())
                {
                    var child = item.Current;
                    list.Add(child);
                }
            }

            return list;
        }

        public List<int> GetChildIDList()
        {
            if (ChildsMap == null || ChildsMap.Count < 1)
                return null;

            List<int> list = new List<int>(BufferSize);
            using (var item = ChildsMap.Values.GetEnumerator())
            {
                while (item.MoveNext())
                {
                    var child = item.Current;
                    if (child != null)
                        list.Add(child.Id);
                }   
            }

            return list;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
