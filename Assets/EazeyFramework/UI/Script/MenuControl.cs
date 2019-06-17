
using System;
using System.Collections.Generic;
using EazeyFramework.Utility;
using UnityEngine;
using UnityEngine.UI;

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

		public MenuControl():base(){}

		public override void Init(GameObject pre, Transform root, MenuHelper helper)
		{
			base.Init(pre, root, helper);
			
			var drawer = _uiMenu.gameObject.GetComponent<MenuDrawerViewBase>();
			if (drawer != null)
				_subMenuPre = drawer.SubMenuPre;

			if (_subMenuPre == null)
				throw new Exception("The subMenu prefab is null.");

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
			//TODO 后续版本尝试做同级Item的回收机制
			HideSubMenu();
		}
		
		protected virtual void ShowSubMenu()
		{
			if (_helper.Data.ChildCount - _subMenuCtrls.Count > 0)
			{
				CreateSubMenuCtrl(_helper.Data.ChildCount - _subMenuCtrls.Count);
			}
			ShowAllSubMenu();
		}

		protected virtual void HideSubMenu()
		{
			if (_subMenuCtrls.Count > 0)
			{
				HideAllSubMenu();
			}
		}

		protected virtual void ShowAllSubMenu()
		{
			//根据数据的数量来决定显示的菜单
			for (int i = 0; i < _sortData.Count; i++)
			{
				var subMenu = _subMenuCtrls[i];
				subMenu.SetVisible(true);
			}
			RefreshMenuRect();
		}

		protected virtual void HideAllSubMenu()
		{
			for (int i = 0; i < _subMenuCtrls.Count; i++)
			{
				var subMenu = _subMenuCtrls[i];
				subMenu.SetVisible(false);
			}
			RefreshMenuRect();
		}

		protected void CreateSubMenuCtrl(int addNum)
		{
			for (int i = 0; i < addNum; i++)
			{
				//TODO 这里有问题   想办法处理子菜单类型
				var ctrls = new MenuControlBase();
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

				ctrl.Init(_subMenuPre, _uiMenu.transform, helper);
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
					RepeatEnable();
				}
			}
			else
			{
				_helper.OnMenuChangeHandler(this);
			}
		}
		
		protected virtual void RepeatEnable()
		{
			if (_subMenuCtrls.Count <= 0)
				return;
			
			IRepeatMenu iRepeatMenu = _uiMenu as IRepeatMenu;
			iRepeatMenu?.Fold(!iRepeatMenu.isFold);
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
			LayoutRebuilder.ForceRebuildLayoutImmediate(_uiMenu.LayoutRoot);
		}
	}
}