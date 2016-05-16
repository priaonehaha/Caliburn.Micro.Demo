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
using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls;

#endregion

namespace Dapplo.CaliburnMicro.MahApps
{
	/// <summary>
	///     This comes from https://github.com/ziyasal/Caliburn.Metro/blob/master/Caliburn.Metro.Core/MetroWindowManager.cs
	/// </summary>
	[Export(typeof(IWindowManager))]
	public class MetroWindowManager : WindowManager
	{
		private ResourceDictionary[] _resourceDictionaries;

		private void AddMetroResources(MetroWindow window)
		{
			_resourceDictionaries = LoadResources();
			foreach (var dictionary in _resourceDictionaries)
			{
				window.Resources.MergedDictionaries.Add(dictionary);
			}
		}

		public virtual void ConfigureWindow(MetroWindow window)
		{
		}

		public virtual MetroWindow CreateCustomWindow(object view, bool windowIsView)
		{
			MetroWindow result;
			if (windowIsView)
			{
				result = view as MetroWindow;
			}
			else
			{
				result = new MetroWindow
				{
					Content = view
				};
			}

			AddMetroResources(result);
			return result;
		}

		protected override Window EnsureWindow(object model, object view, bool isDialog)
		{
			MetroWindow window = null;
			Window inferOwnerOf;
			if (view is MetroWindow)
			{
				window = CreateCustomWindow(view, true);
				inferOwnerOf = InferOwnerOf(window);
				if (inferOwnerOf != null && isDialog)
				{
					window.Owner = inferOwnerOf;
				}
			}

			if (window == null)
			{
				window = CreateCustomWindow(view, false);
			}

			ConfigureWindow(window);
			window.SetValue(View.IsGeneratedProperty, true);
			inferOwnerOf = InferOwnerOf(window);
			if (inferOwnerOf != null)
			{
				window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				window.Owner = inferOwnerOf;
			}
			else
			{
				window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			}

			return window;
		}

		private ResourceDictionary[] LoadResources()
		{
			return new[]
			{
				new ResourceDictionary
				{
					Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute)
				},
				new ResourceDictionary
				{
					Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml", UriKind.RelativeOrAbsolute)
				},
				new ResourceDictionary
				{
					Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml", UriKind.RelativeOrAbsolute)
				},
				new ResourceDictionary
				{
					Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml", UriKind.RelativeOrAbsolute)
				},
				new ResourceDictionary
				{
					Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml", UriKind.RelativeOrAbsolute)
				},
				new ResourceDictionary
				{
					Source = new Uri("pack://application:,,,/Resources/Icons.xaml", UriKind.RelativeOrAbsolute)
				}
			};
		}
	}
}