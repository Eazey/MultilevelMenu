using System;
using EazeyFramework.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EazeyFramework.UI
{ 
    public class MenuControlBase
    {
        protected IMenuView _uiMenu;
        
        protected MenuHelper _helper;
        public MenuHelper Helper => _helper;

        /// <summary>
        /// 当前自身是否是选中状态
        /// </summary>
        public bool IsSelect;

        public MenuControlBase(GameObject pre, Transform root)
        {      
            if (pre == null)
                throw new Exception("The prefabs is null");

            if (root == null)
                throw new Exception("The root is null");

            var go = Object.Instantiate(pre, root);

            _uiMenu = go.GetComponent<IMenuView>();
            _uiMenu?.Init(_helper.Data, OnEnableResponse);
        }
        
        /// <summary>
        /// 初始化子列表
        /// </summary>
        /// <param name="helper"></param>
        public virtual void Init(MenuHelper helper)
        {
            if (helper == null)
                throw new Exception("The object of type 'MenuSeting' is null.");
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

        protected void NormalUI()
        {
            _uiMenu.NormalView();
        }

        protected void PressedUI()
        {
            _uiMenu.PressedView();
        }

        protected virtual void EnableDoSomething()
        {
            ExcuteEnableCb();
        }

        protected virtual void DisableDoSomething()
        {
            
        }

        protected virtual void OnEnableResponse()
        {
            _helper.OnMenuChangeHandler(this);
        }

        protected virtual void ExcuteEnableCb()
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
