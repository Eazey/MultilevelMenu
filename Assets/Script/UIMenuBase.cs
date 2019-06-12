
using UnityEngine;
using UnityEngine.UI;

namespace EazeyFramework.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class UIMenuBase : MonoBehaviour
    {
        public UIMenuBase SubMenuPre;
        public Button ClickBtn;

        protected MenuControlBase _control;

        private void Awake()
        {
            ClickBtn = GetComponent<Button>();
        }

        public void SetControl(MenuControlBase control)
        {
            _control = control;
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
    }
}