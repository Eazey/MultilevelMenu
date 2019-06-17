using System;
using EazeyFramework.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EazeyFramework.UI
{ 
    public class MenuControlBase
    {
        protected MenuViewBase _uiMenu;
        
        protected MenuHelper _helper;
        public MenuHelper Helper => _helper;

        /// <summary>
        /// 当前自身是否是选中状态
        /// </summary>
        public bool IsSelect;

        public MenuControlBase(){}

        public virtual void Init(GameObject pre, Transform root, MenuHelper helper)
        {
            if (pre == null)
                throw new Exception("The prefabs is null");

            if (root == null)
                throw new Exception("The root is null");

            if (helper == null)
                throw new Exception("The object of type 'MenuSeting' is null.");
            _helper = helper;

            var go = Object.Instantiate(pre, root);

            _uiMenu = go.GetComponent<MenuViewBase>();
            if (_uiMenu != null)
                _uiMenu.Init(_helper.Data, OnEnableResponse);
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
            IMenuView view = _uiMenu as IMenuView;
            view?.NormalView();
        }

        protected void PressedUI()
        {
            IMenuView view = _uiMenu as IMenuView;
            view?.PressedView();
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

        public void SetVisible(bool value)
        {
            _uiMenu.SetVisible(value);
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
