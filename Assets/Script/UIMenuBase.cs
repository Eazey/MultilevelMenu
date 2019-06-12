
using System;
using UnityEngine;
using UnityEngine.UI;

namespace EazeyFramework.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class UIMenuBase : MonoBehaviour
    {
        public UIMenuBase SubMenuPre;
        public Button ClickBtn;

        protected MenuData _data;
        protected Action _onClickHandler;

        private void Awake()
        {
            ClickBtn = GetComponent<Button>();
            ClickBtn.onClick.AddListener(OnBtnClick);
        }

        public void Init(MenuData data, Action onClickHandler)
        {
            _data = data;
            _onClickHandler = onClickHandler;
        }
        
        public void InitUI()
        {
            
        }

        public void Normal()
        {
            
        }

        public void Pressed()
        {
            
        }

        private void OnBtnClick()
        {
            if (_onClickHandler != null)
                _onClickHandler();
        }
    }
}