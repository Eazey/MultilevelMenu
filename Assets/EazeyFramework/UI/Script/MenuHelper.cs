using System;

namespace EazeyFramework.UI
{
    /// <summary>
    /// 菜单所需要的基础数据结构
    /// </summary>
    public class MenuHelper
    {
        public MenuData Data;

        /// <summary>
        /// 数据的Hash码 用于确定数据是否改变
        /// </summary>
        public int DataHash => Data.GetHashCode();

        /// <summary>
        /// 初始化选择
        /// </summary>
        public MenuData InitSelect;

        /// <summary>
        /// 【无子菜单】的选项 Enable时激活事件
        /// </summary>
        public Delegate OnEnableCallback;

        /// <summary>
        /// 切换子菜单
        /// </summary>
        public Action<MenuControlBase> OnMenuChangeHandler;

        /// <summary>
        /// 菜单选定时 所有菜单的切换类型
        /// </summary>
        public MenuInteractType InteractType;

    }
}