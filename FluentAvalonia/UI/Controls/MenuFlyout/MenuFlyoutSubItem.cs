﻿using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace FluentAvalonia.UI.Controls
{
	/// <summary>
	/// Represents a menu item that displays a sub-menu in a <see cref="MenuFlyout"/> control.
	/// </summary>
	public partial class MenuFlyoutSubItem : MenuFlyoutItemBase, IMenuItem
	{
		public MenuFlyoutSubItem()
		{
			_items = new AvaloniaList<object>();
		}

		protected override void OnPointerEntered(PointerEventArgs e)
		{
			base.OnPointerEntered(e);

			var point = e.GetCurrentPoint(null);
			RaiseEvent(new PointerEventArgs(MenuItem.PointerEnteredItemEvent, this, e.Pointer, this.VisualRoot, point.Position,
				e.Timestamp, point.Properties, e.KeyModifiers));
		}

		protected override void OnPointerExited(PointerEventArgs e)
		{
			base.OnPointerExited(e);

			var point = e.GetCurrentPoint(null);
			RaiseEvent(new PointerEventArgs(MenuItem.PointerExitedItemEvent, this, e.Pointer, this.VisualRoot, point.Position,
				e.Timestamp, point.Properties, e.KeyModifiers));
		}

		/// <summary>
		/// Opens the SubMenu
		/// </summary>
		public void Open()
		{
			InitPopup();
			_subMenu.IsOpen = true;
            _presenter.RaiseMenuOpened();
		}

		/// <summary>
		/// Closes the SubMenu
		/// </summary>
		public void Close()
		{
            if (_subMenu != null)
			    _subMenu.IsOpen = false;

            if (_presenter != null)
            {
                _presenter.SelectedIndex = -1;
                _presenter.RaiseMenuClosed();
            }            
		}
				
		private void InitPopup()
		{
			if (_subMenu == null)
			{
				_presenter = new MenuFlyoutPresenter()
				{
					[!ItemsControl.ItemsProperty] = this[!ItemsProperty],
					[!ItemsControl.ItemTemplateProperty] = this[!ItemTemplateProperty]
				};

				_subMenu = new Popup
				{
					Child = _presenter,
					HorizontalOffset = -4,
					WindowManagerAddShadowHint = false,
					PlacementMode = PlacementMode.AnchorAndGravity,
					PlacementAnchor = Avalonia.Controls.Primitives.PopupPositioning.PopupAnchor.TopRight,
					PlacementGravity = Avalonia.Controls.Primitives.PopupPositioning.PopupGravity.BottomRight,
					PlacementTarget = this
				};

				LogicalChildren.Add(_subMenu);

				_subMenu.Opened += OnPopupOpen;
				_subMenu.Closed += OnPopupClose;
			}
		}

		private void OnPopupOpen(object sender, EventArgs e)
		{
			PseudoClasses.Set(":submenuopen", true);
		}

		private void OnPopupClose(object sender, EventArgs e)
		{
			PseudoClasses.Set(":submenuopen", false);
		}

        bool IMenuElement.MoveSelection(NavigationDirection direction, bool wrap) => false;
		
		private Popup _subMenu;
		private MenuFlyoutPresenter _presenter;

        public bool StaysOpenOnClick { get; set; }
    }
}
