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

#if SYSTEM_DRAWING
using System.Drawing;
#endif

using System.Windows.Controls.Primitives;
using Hardcodet.Wpf.TaskbarNotification;

#endregion

namespace Dapplo.CaliburnMicro.NotifyIconWpf
{
	/// <summary>
	/// This is the interface to the tray icon
	/// </summary>
	public interface ITrayIcon : IDisposable
	{
		/// <summary>
		/// Close the actual balloon, if there is any
		/// </summary>
		void CloseBalloon();

		/// <summary>
		/// Hide the icon
		/// </summary>
		void Hide();

		/// <summary>
		/// Show the icon
		/// </summary>
		void Show();

		/// <summary>
		/// Show a balloon with title, message and an icon
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		/// <param name="balloonIcon">BalloonIcon</param>
		void ShowBalloonTip(string title, string message, BalloonIcon balloonIcon = BalloonIcon.Info);

#if SYSTEM_DRAWING
		/// <summary>
		/// Show a balloon with title, message and an icon
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		/// <param name="customIcon">Icon</param>
		/// <param name="largeIcon">true to show the icon large</param>
		void ShowBalloonTip(string title, string message, Icon customIcon, bool largeIcon = false);
#endif

		/// <summary>
		/// Show a custom balloon (ViewModel first), using the specified animation.
		/// After the timeout, the balloon is removed.
		/// </summary>
		/// <typeparam name="T">Type for the ViewModel to show</typeparam>
		/// <param name="animation">PopupAnimation</param>
		/// <param name="timeout">TimeSpan</param>
		void ShowBalloonTip<T>(PopupAnimation animation, TimeSpan? timeout = null);
	}
}