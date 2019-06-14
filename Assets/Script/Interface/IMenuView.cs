
using System;
using UnityEngine;

namespace EazeyFramework.UI
{
    public interface IMenuView
    {
        GameObject gameObject { get; }
        Transform transform { get; }
        
        MenuData Data { get; }
        MenuClick Click { get; }

        void Init(MenuData data, Action clickCb);
        void NormalView();
        void PressedView();
    }
}