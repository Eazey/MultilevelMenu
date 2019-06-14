
using System;
using System.Collections.Generic;
using EazeyFramework.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EazeyFramework.UI
{
	public class MenuControl : MenuControlBase
	{
		private GameObject _subMenuPre;
		
		protected List<MenuData> _sortData;
		protected List<MenuControlBase> _subMenuCtrls = new List<MenuControlBase>();

		/// <summary>
		/// 当前激活的子菜单
		/// </summary>
		public MenuControlBase CurActiveMenu { get; private set; }

		/// <summary>
		/// 当前激活的子菜单的Hash码
		/// </summary>
		public int? CurActiveMenuHash { get; private set; }

		public MenuControl(GameObject pre, Transform root)
			: base(pre, root)
		{
			var contains = _uiMenu.gameObject.GetComponent<IContainSubMenu>();
			_subMenuPre = contains?.SubMenuPre;
			if (_subMenuPre == null)
				throw new Exception("The subMenu prefab is null.");
		}

		public override void Init(MenuHelper helper)
		{
			base.Init(helper);
			DataParse();
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
			_sortData = _helper.Data.GetChildList();
			MenuUtility.Sort(ref _sortData);
		}

		protected override void EnableDoSomething()
		{
			if (_sortData?.Count > 0)
			{
				ShowSubMenu();
				SetSubMenuData();
				ChoiceLastMenu();
			}
			else
			{
				ExcuteEnableCb();
			}
		}

		
		protected override void DisableDoSomething()
		{
			HideMenuOption();
		}
		
		protected virtual void ShowSubMenu()
		{
			if (_helper.Data.ChildCount - _subMenuCtrls.Count > 0)
			{
				CreateSubMenu(_helper.Data.ChildCount - _subMenuCtrls.Count);
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
			for (int i = 0; i < addNum; i++)
			{
				var ctrls = new MenuControlBase(_subMenuPre, _uiMenu.transform);
				_subMenuCtrls.Add(ctrls);
			}
		}
		
		protected virtual void SetSubMenuData()
		{	
			for (int i = 0; i < _sortData.Count; i++)
			{
				var data = _sortData[i];
				var ctrl = _subMenuCtrls[i];

				MenuHelper helper;
				HelperConstruct(data, out helper);
				ctrl.Init(helper);
			}
		}

		protected virtual void HelperConstruct(MenuData data, out MenuHelper helper)
		{
			helper = new MenuHelper();
			
			helper.Data =  data;
			helper.OnEnableCallback = _helper.OnEnableCallback;
			helper.OnMenuChangeHandler = OnSubMenuChange;
			helper.InteractType = _helper.InteractType;
		}

		protected override void OnEnableResponse()
		{
			if (IsSelect)
			{
				if ((_helper.InteractType & MenuInteractType.Repete) > 0)
				{
					RepeteEnable();
				}
			}
			else
			{
				_helper.OnMenuChangeHandler(this);
			}
		}
		
		protected virtual void RepeteEnable()
		{
			if (_subMenuCtrls.Count <= 0)
				return;
		}

		protected void ChoiceLastMenu()
		{
			bool existCur = false;
			if (CurActiveMenuHash != null)
			{
				using(var item = _subMenuCtrls.GetEnumerator())
				{
					while (item.MoveNext())
					{
						var menu = item.Current;
						if (menu?.Helper == null)
							continue;

						if (CurActiveMenuHash == menu.GetDataHash())
						{
							OnSubMenuChange(menu);
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
			OnSubMenuChange(_subMenuCtrls[index]);
		}

		protected virtual void OnSubMenuChange(MenuControlBase activeMenu)
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