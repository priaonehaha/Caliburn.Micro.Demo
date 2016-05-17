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

#endregion

namespace Dapplo.CaliburnMicro.NotifyIconWpf
{
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

			var wrapper = (TrayIconWrapper) reference.Target;
			if (wrapper.IsDisposed)
			{
				return Create<T>();
			}

			return wrapper;
		}

		/// <summary>
		///     Create the TaskbarIcon
		/// </summary>
		/// <typeparam name="T">Type to create the ViewModel for </typeparam>
		/// <returns>ITrayIcon</returns>
		private ITrayIcon Create<T>()
		{
			var rootModel = IoC.Get<T>();
			var view = ViewLocator.LocateForModel(rootModel, null, null);
			var taskbarIcon = view as TaskbarIcon;
			var icon = taskbarIcon ?? new TaskbarIcon();
			var wrapper = new TrayIconWrapper(icon);

			ViewModelBinder.Bind(rootModel, view, null);
			SetIconInstance(rootModel, wrapper);
			_icons.Add(new WeakReference(rootModel), new WeakReference(wrapper));

			return wrapper;
		}

		private void SetIconInstance(object rootModel, ITrayIcon icon)
		{
			var instance = rootModel as ISetTrayIconInstance;
			if (instance != null)
			{
				instance.Icon = icon;
			}
		}
	}
}