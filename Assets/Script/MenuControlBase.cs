using System;
using UnityEngine;

namespace EazeyFramework.UI
{ 
    public class MenuControlBase
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
        }

        public void Enable()
        {
            IsSelect = true;
            PressedUI();
            EnableDoSomething();
        }

        public void Disable()
        {
            IsSelect = false;
            NormalUI();
            DisableDoSomething();
        }
        
        protected virtual void NormalUI(){}
		
        protected virtual void PressedUI(){}

        protected virtual void EnableDoSomething()
        {
            OnEnableCallback();
        }

        protected virtual void DisableDoSomething()
        {
            
        }

        protected virtual void OnEnableCallback()
        {
            if (_helper?.OnEnableCallback != null)
            {
                Action<int> callback = _helper.OnEnableCallback as Action<int>;
                if (callback != null)
                    callback(_helper.Data.Id);
            }
        }

        public int GetDataHash()
        {
            return _helper.DataHash;
        }

        public bool Equals(MenuControlBase other)
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
