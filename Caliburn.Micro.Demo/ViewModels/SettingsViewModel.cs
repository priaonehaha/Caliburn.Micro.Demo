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
using Dapplo.CaliburnMicro;

#endregion

namespace Caliburn.Micro.Demo.ViewModels
{
	/// <summary>
	/// The settings view model is, well... for the settings :)
	/// It is a conductor where one item is active.
	/// </summary>
	[Export(typeof(IShell))]
	public class SettingsViewModel : Conductor<ISettingsControl>.Collection.OneActive, IShell
	{
		[Import]
		private IDemoConfiguration DemoConfiguration { get; set; }

		/// <summary>
		/// Get all settings controls, these are the items that are displayed.
		/// </summary>
		[ImportMany]
		private IEnumerable<ISettingsControl> SettingsControls { get; set; }

		/// <summary>
		/// This is called when an item from the itemssource is selected
		/// And will make sure that the selected item is made visible.
		/// </summary>
		/// <param name="view"></param>
		public void ActivateChildView(ISettingsControl view)
		{
			ActivateItem(view);
		}

		/// <summary>
		/// As soon as it's activated, the items that are imported are add to the Observable Items collection.
		/// </summary>
		protected override void OnActivate()
		{
			base.OnActivate();
			var lang = DemoConfiguration.Language;

			// Add all the imported settings controls
			// TODO: Sort them for a tree view, somehow...
			Items.AddRange(SettingsControls);

			UiContext.RunOn(async () => await LanguageLoader.Current.ChangeLanguageAsync(lang)).Wait();
		}
	}
}