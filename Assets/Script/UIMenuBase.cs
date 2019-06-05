using UnityEngine;

namespace EazeyFramework.UI
{ 
    public abstract class UIMenuBase
    {
        protected MenuHelper _helper;
        public MenuHelper Helper => _helper;

        /// <summary>
        /// 当前自身是否是选中状态
        /// </summary>
        public bool IsSelect;


        /// <summary>
        /// 初始化子列表
        /// </summary>
        /// <param name="helper"></param>
        public virtual void Init(MenuHelper helper)
        {
            if (helper == null)
            {
                Debug.LogError("The object of type 'MenuSeting' is null.");
                return;
            }

            _helper = helper;

            DataParse();
        }


        /// <summary>
        /// 数据解析
        /// </summary>
        protected abstract void DataParse();

        #region Menu Switch  菜单自身状态改变时处理

        public virtual void Enable()
        {
            IsSelect = true;
        }

        public virtual void Disable()
        {
            IsSelect = false;
        }

        #endregion

        public int GetDataHash()
        {
            return _helper.DataHash;
        }

        public bool Equals(UIMenuBase other)
        {
            bool result = false;
            if (_helper != null && other._helper != null)
            {
                result = GetDataHash() == other.GetDataHash();
            }

            return result;
        }
    }
}
