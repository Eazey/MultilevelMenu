
using System;
using System.Collections.Generic;
using EazeyFramework.Utility;
using UnityEngine;

namespace EazeyFramework.UI
{
	public class MenuControl : MenuControlBase
	{
		protected List<MenuData> _data;
		protected List<MenuControlBase> _subMenus = new List<MenuControlBase>();

		/// <summary>
		/// 当前激活的子菜单
		/// </summary>
		public MenuControlBase CurActiveMenu { get; private set; }

		/// <summary>
		/// 当前激活的子菜单的Hash码
		/// </summary>
		public int? CurActiveMenuHash { get; private set; }

		public override void Init(MenuHelper helper)
		{
			base.Init(helper);
			InitUI();
			DataParse();
		}
		
		protected virtual void InitUI()
		{
			NormalUI();
		}
		
		protected void DataParse()
		{
			if (_helper.Data == null)
			{
				Debug.LogError("Data do not parse because that's null.");
				return;
			}
			
			if (_helper.InitSelect != null)
				CurActiveMenuHash = _helper.InitSelect.Id;

			DoDataParse();
		}

		protected virtual void DoDataParse()
		{
			_data = _helper.Data.GetChildList();
			MenuUtility.Sort(ref _data);
		}

		protected override void EnableDoSomething()
		{
			if (_data?.Count > 0)
			{
				ShowSubMenu();
				SetSubMenuData();
				ChoiceLastMenu();
			}
			else
			{
				OnEnableCallback();
			}
		}

		
		protected override void DisableDoSomething()
		{
			HideMenuOption();
		}
		
		protected virtual void ShowSubMenu()
		{
			if (_helper.Data.ChildCount - _subMenus.Count > 0)
			{
				CreateSubMenu(_helper.Data.ChildCount - _subMenus.Count);
			}
			// Show all menu
			RefreshMenuRect();
		}
		
		protected virtual void HideMenuOption()
		{
			// Hide all menu
			RefreshMenuRect();
		}


		protected void CreateSubMenu(int addNum)
		{
			
		}
		
		protected virtual void SetSubMenuData()
		{
		}
		
		protected void ChoiceLastMenu()
		{
			bool existCur = false;
			if (CurActiveMenuHash != null)
			{
				using(var item = _subMenus.GetEnumerator())
				{
					while (item.MoveNext())
					{
						var menu = item.Current;
						if (menu?.Helper == null)
							continue;

						if (CurActiveMenuHash == menu.GetDataHash())
						{
							MenuChange(menu);
							existCur = true;
							break;
						}
					}
				}
			}

			if (!existCur)
				ChoiceMenu(0);
		}

		protected void ChoiceMenu(int index)
		{
			MenuChange(_subMenus[index]);
		}

		protected virtual void MenuChange(MenuControlBase activeMenu)
		{
			if (CurActiveMenu != null 
			    && (CurActiveMenu.Helper.InteractType & MenuInteractType.Exclusive) > 0)
			{
				CurActiveMenu.Disable();
				CurActiveMenuHash = null;
			}

			CurActiveMenu = activeMenu;

			if (CurActiveMenu != null)
			{
				CurActiveMenu.Enable();
				CurActiveMenuHash = CurActiveMenu.GetDataHash();
			}
		}

		/// <summary>
		/// 刷新界面绘制
		/// </summary>
		protected void RefreshMenuRect()
		{
			//LayoutRebuilder.ForceRebuildLayoutImmediate();
		}
	}
}