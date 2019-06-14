
using System;
using EazeyFramework.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace EazeyFramework.UI
{ 
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class MenuViewBase : MonoBehaviour
    {
        private Button _menuBtn;
        
        private Action _onClickHandler;
        private MenuData _menuData;

        private RectTransform _layoutRoot;
        public RectTransform LayoutRoot
        {
            get
            {
                if (_layoutRoot == null)
                    _layoutRoot = transform.parent.GetComponent<RectTransform>();
                return _layoutRoot;
            }
        }
        
        private void Awake()
        {
            _menuBtn = GetComponent<Button>();
            _menuBtn.onClick.AddListener(OnMenuClick);
        }
        
        public void Init(MenuData data, Action clickCb)
        {
            _menuData = data;
            _onClickHandler = clickCb;
            
            transform.Reset();
        }

        private void OnMenuClick()
        {
            _onClickHandler?.Invoke();
        }
    }
}