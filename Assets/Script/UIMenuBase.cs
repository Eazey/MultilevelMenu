
using System;
using EazeyFramework.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace EazeyFramework.UI
{
    [RequireComponent(typeof(Button))]
    public class UIMenuBase : MonoBehaviour, IMenuView, IContainSubMenu
    {
        [SerializeField] private GameObject _subMenuPre;
        public GameObject SubMenuPre { get; set; }

        public MenuData Data { get; set; }
        public MenuClick Click { get; set; }

        public void Init(MenuData data, Action clickCb)
        {
            Data = data;
            Click = new MenuClick(GetComponent<Button>(), clickCb);

            transform.Reset();
        }

        public void NormalView()
        {

        }

        public void PressedView()
        {

        }
    }
}