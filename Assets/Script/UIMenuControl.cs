
using System;
using System.Collections.Generic;
using EazeyFramework.Utility;
using UnityEngine;

namespace EazeyFramework.UI
{
	public class UIMenuControl : UIMenuBase
	{
		protected List<UIMenuBase> _subMenus = new List<UIMenuBase>();

		/// <summary>
		/// 当前激活的子菜单
		/// </summary>
		public UIMenuBase curActiveMenu { get; private set; }

		/// <summary>
		/// 当前激活的子菜单的Hash码
		/// </summary>
		public int? curActiveMenuHash { get; private set; }


		public override void Init(MenuHelper helper)
		{
			base.Init(helper);
			DataParse();
			InitUI();
		}
		
		protected void DataParse()
		{
			if (_helper.Data == null)
			{
				Debug.LogError("Data do not parse because that's null.");
				return;
			}
			
			if (_helper.InitSelect != null)
				curActiveMenuHash = _helper.InitSelect.DataId;

			DoDataParse();
		}

		protected virtual void DoDataParse(){}
	
		protected virtual void InitUI(){}
		
		

		public override void Enable()
		{
			base.Enable();
			
			PressedUI();
			ShowSubMenu();
			SetSubMenuData();
			EnableDoSomthing();
		}

		public override void Disable()
		{
			base.Disable();

			NormalUI();
			HideMenuOption();
		}	
		
		protected virtual void NormalUI(){}
		
		protected virtual void PressedUI(){}
		
		protected virtual void ShowSubMenu()
		{
			if (_helper.Data.ChildCount - _subMenus.Count > 0)
			{
				CreateSubMenu(_helper.Data.ChildCount - _subMenus.Count);
			}
			// Show all menu
			RefreshMenuRect();
		}

		protected void CreateSubMenu(int addNum)
		{
			
		}

		protected virtual void HideMenuOption()
		{
			// Hide all menu
			RefreshMenuRect();
		}

		/// <summary>
		/// 填充数据
		/// </summary>
		protected virtual void SetSubMenuData()
		{
			if (_helper.Data.ChildCount == 0)
				return;

			DoSetSubMenuData();
		}

		protected virtual void DoSetSubMenuData()
		{

		}

		protected virtual void EnableDoSomthing()
		{
			if (_helper.Data.ChildsMap != null && _helper.Data.ChildsMap.Count > 0)
			{
				ChoiceLastMenu();
			}
			else
			{
				OptionClickCallBack();
			}
		}

		protected void ChoiceLastMenu()
		{
			bool existCur = false;
			if (curActiveMenuHash != null)
			{
				using(var item = _subMenus.GetEnumerator())
				{
					while (item.MoveNext())
					{
						var menu = item.Current;
						if (menu?.Helper == null)
							continue;

						if (curActiveMenuHash == menu.GetDataHash())
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
		
		protected virtual void OptionClickCallBack()
		{
			if (_helper?.OptionClickCallback != null)
			{
				Action<int> callback = _helper.OptionClickCallback as Action<int>;
				if (callback != null)
					callback(_helper.Data.DataId);
			}
		}

		protected virtual void MenuChange(UIMenuBase activeMenu)
		{
			if (curActiveMenu != null 
			    && (curActiveMenu.Helper.InteractType & MenuInteractType.Exclusive) > 0)
			{
				curActiveMenu.Disable();
				curActiveMenuHash = null;
			}

			curActiveMenu = activeMenu;

			if (curActiveMenu != null)
			{
				curActiveMenu.Enable();
				curActiveMenuHash = curActiveMenu.GetDataHash();
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