
using System;
using EazeyFramework.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace EazeyFramework.UI
{
    [RequireComponent(typeof(Button))]
    public class UIMenuBase : MonoBehaviour, IMenuView
    {
        public GameObject SubMenuPre;

        public MenuData Data { get; set; }
        public MenuClick Click { get; set; }

        public void Init(MenuData data, Action clickCb)
        {
            Data = data;
            Click = new MenuClick(GetComponent<Button>(), clickCb);

            this.Reset();
        }

        public void NormalView()
        {
            
        }

        public void PressedView()
        {
            
        }
    }
}