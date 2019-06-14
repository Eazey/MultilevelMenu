
using System;
using UnityEngine;

namespace EazeyFramework.UI
{
    public interface IMenuView
    {
        MenuData Data { get; set; }
        MenuClick Click { get; set; }

        void Init(MenuData data, Action clickCb);
        void NormalView();
        void PressedView();
    }
}