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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Dapplo.Addons;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

#endregion

namespace Dapplo.CaliburnMicro.NotifyIconWpf
{
	/// <summary>
	/// This is used to track and make sure the TrayIcon is only instaciated once per ViewModel
	/// </summary>
	[Export(typeof(ITrayIconManager))]
	public class TrayIconManager : ITrayIconManager
	{
		private readonly IDictionary<WeakReference, WeakReference> _icons;

		[Import]
		private IServiceLocator ServiceLocator { get; set; }

		public TrayIconManager()
		{
			_icons = new Dictionary<WeakReference, WeakReference>();
		}

		public ITrayIcon GetOrCreateFor<T>()
		{
			if (!_icons.Any(i => i.Key.IsAlive && i.Key.Target is T))
			{
				return Create<T>();
			}

			var reference = _icons.First(i => i.Key.IsAlive && i.Key.Target is T).Value;
			if (!reference.IsAlive)
			{
				return Create<T>();
			}

			var trayIcon = (TrayIconContainer) reference.Target;
			if (trayIcon.IsDisposed)
			{
				return Create<T>();
			}

			return trayIcon;
		}

		/// <summary>
		///     Create the TaskbarIcon
		/// </summary>
		/// <typeparam name="T">Type to create the ViewModel for </typeparam>
		/// <returns>ITrayIcon</returns>
		private ITrayIcon Create<T>()
		{
			var rootModel = IoC.Get<T>();

			var trayIconView = ViewLocator.LocateForModel(rootModel, null, null) as FrameworkElement;
			var taskbarIcon = trayIconView.Resources.Values.Cast<object>().Where(x => x is TaskbarIcon).Cast<TaskbarIcon>().FirstOrDefault();

			var trayIconHolder = trayIconView as ITrayIconHolder;
			var trayIconContainer = new TrayIconContainer(taskbarIcon);

			trayIconHolder.TrayIcon = trayIconContainer;
			_icons.Add(new WeakReference(rootModel), new WeakReference(trayIconContainer));

			ViewModelBinder.Bind(rootModel, trayIconView, null);

			return trayIconContainer;
		}
	}
}