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
using Caliburn.Micro.Demo.Interfaces;
using Caliburn.Micro.DemoAddon.Models;
using Dapplo.CaliburnMicro.Extensions;

#endregion

namespace Caliburn.Micro.DemoAddon.ViewModels
{
	[Export(typeof(ISettingsControl))]
	public class DemoAddonSettingsViewModel : Screen, ISettingsControl, IPartImportsSatisfiedNotification
	{
		[Import]
		public IDemoAddonConfiguration DemoAddonConfiguration { get; set; }

		[Import]
		public IDemoAddonTranslations DemoAddonTranslations { get; set; }

		/// <summary>
		///     Implement the IHaveDisplayName
		/// </summary>
		public override string DisplayName
		{
			get
			{
				return DemoAddonTranslations.SomeText;
			}
			set { throw new NotImplementedException($"Set {nameof(DisplayName)}"); }
		}


		public void OnImportsSatisfied()
		{
			DemoAddonTranslations.BindChanges(nameof(DemoAddonTranslations.SomeText), OnPropertyChanged, nameof(DisplayName));
		}
	}
}