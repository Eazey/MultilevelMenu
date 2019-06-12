
using UnityEngine;
using UnityEngine.UI;

namespace EazeyFramework.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class UIMenuBase : MonoBehaviour
    {
        public Button ClickBtn;

        private MenuControlBase _control;

        private void Awake()
        {
            ClickBtn = GetComponent<Button>();
        }
    }
}