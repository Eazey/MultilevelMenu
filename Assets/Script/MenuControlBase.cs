using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EazeyFramework.UI
{ 
    public class MenuControlBase
    {
        private Transform _uiMenuRoot;
        private GameObject _uiMenuPre;

        protected UIMenuBase _uiMenu;
        protected MenuHelper _helper;
        public MenuHelper Helper => _helper;

        /// <summary>
        /// 当前自身是否是选中状态
        /// </summary>
        public bool IsSelect;


        public MenuControlBase(MenuHelper helper, GameObject pre, Transform root)
        {
            if (helper == null)
                throw new Exception("The object of type 'MenuSeting' is null.");

            if (pre == null)
                throw new Exception("The prefabs is null");

            if (root == null)
                throw new Exception("The root is null");

            Init(helper, pre, root);
        }
        
        /// <summary>
        /// 初始化子列表
        /// </summary>
        /// <param name="helper"></param>
        protected virtual void Init(MenuHelper helper, GameObject pre, Transform root)
        {
            _helper = helper;

            GameObject go = Object.Instantiate(pre, root);
            _uiMenu = go.GetComponent<UIMenuBase>();
            _uiMenu.SetControl(this);
            _uiMenu.InitUI();
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
            _uiMenu.Normal();
        }

        protected void PressedUI()
        {
            _uiMenu.Pressed();
        }

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
