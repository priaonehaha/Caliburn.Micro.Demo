//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Caliburn.Micro.Demo
// 
//  Caliburn.Micro.Demo is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Caliburn.Micro.Demo is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Caliburn.Micro.Demo. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls.Primitives;
using Caliburn.Micro;
using Hardcodet.Wpf.TaskbarNotification;

#endregion

namespace Dapplo.CaliburnMicro.NotifyIconWpf
{
	/// <summary>
	/// Wrap the TrayIcon and manage it
	/// </summary>
	public class TrayIconContainer : ITrayIcon
	{
		private readonly TaskbarIcon _icon;

		public TrayIconContainer(TaskbarIcon icon)
		{
			_icon = icon;
		}

		public bool IsDisposed { get; private set; }

		public void Dispose()
		{
			_icon.Dispose();
			IsDisposed = true;
		}

		/// <summary>
		/// Show the icon
		/// </summary>
		public void Show()
		{
			_icon.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Hide the icon
		/// </summary>
		public void Hide()
		{
			_icon.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Show a balloon with title, message and an icon.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		/// <param name="customIcon">Icon</param>
		/// <param name="largeIcon"></param>
		public void ShowBalloonTip(string title, string message, Icon customIcon, bool largeIcon = false)
		{
			_icon.ShowBalloonTip(title, message, customIcon, largeIcon);
		}

		/// <summary>
		/// Show a balloon with title, message and an icon.
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		/// <param name="balloonIcon">BalloonIcon</param>
		public void ShowBalloonTip(string title, string message, BalloonIcon balloonIcon = BalloonIcon.Info)
		{
			_icon.ShowBalloonTip(title, message, balloonIcon);
		}

		/// <summary>
		/// Show a custom balloon (ViewModel first), using the specified animation.
		/// After the timeout, the balloon is removed.
		/// </summary>
		/// <typeparam name="T">Type for the ViewModel to show</typeparam>
		/// <param name="animation">PopupAnimation</param>
		/// <param name="timeout">TimeSpan</param>
		public void ShowBalloonTip<T>(PopupAnimation animation, TimeSpan? timeout = null)
		{
			var rootModel = IoC.Get<T>();

			var view = ViewLocator.LocateForModel(rootModel, null, null);
			ViewModelBinder.Bind(rootModel, view, null);

			_icon.ShowCustomBalloon(view, animation, timeout.HasValue ? (int) timeout.Value.TotalMilliseconds : (int?) null);
		}

		public void CloseBalloon()
		{
			_icon.CloseBalloon();
		}
	}
}