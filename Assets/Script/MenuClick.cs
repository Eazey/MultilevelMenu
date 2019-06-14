using System;
using UnityEngine.UI;

namespace EazeyFramework.UI
{
    public class MenuClick
    {
        private Button _btn;
        private Action _clickHandelr;

        public MenuClick(Button btn, Action callback)
        {
            _btn = btn;
            _clickHandelr = callback;

            _btn.onClick.AddListener(OnBtnClick);
        }

        private void OnBtnClick()
        {
            _clickHandelr?.Invoke();
        }
    }
}