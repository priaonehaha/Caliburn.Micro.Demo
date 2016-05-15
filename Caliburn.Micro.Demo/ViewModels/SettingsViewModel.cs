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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro.Demo.Interfaces;
using Caliburn.Micro.Demo.Models;
using Dapplo.Config.Language;
using Dapplo.Utils;

#endregion

namespace Caliburn.Micro.Demo.ViewModels
{
	/// <summary>
	///     A view model for credentials (username / password)
	/// </summary>
	[Export(typeof(IShell))]
	public class SettingsViewModel : Conductor<ISettingsControl>.Collection.OneActive, IShell
	{
		[Import]
		private IDemoConfiguration DemoConfiguration { get; set; }

		[ImportMany]
		private IEnumerable<ISettingsControl> SettingsControls { get; set; }

		public void ActivateChildView(ISettingsControl view)
		{
			ActivateItem(view);
		}

		protected override void OnActivate()
		{
			base.OnActivate();
			var lang = DemoConfiguration.Language;

			// Add all the imported settings controls
			Items.AddRange(SettingsControls);

			UiContext.RunOn(async () => await LanguageLoader.Current.ChangeLanguageAsync(lang)).Wait();
		}
	}
}