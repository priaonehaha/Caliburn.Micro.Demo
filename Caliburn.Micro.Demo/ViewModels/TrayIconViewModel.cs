﻿//  Dapplo - building blocks for desktop applications
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

using System.ComponentModel.Composition;
using Dapplo.CaliburnMicro.NotifyIconWpf;
using Dapplo.LogFacade;

#endregion

namespace Caliburn.Micro.Demo.ViewModels
{
	[Export]
	public class TrayIconViewModel : TrayIconScreen
	{
		private static readonly LogSource Log = new LogSource();

		public bool CanShowSomething()
		{
			return true;
		}

		public void Configure()
		{
			Log.Debug().WriteLine("Configure");
		}

		public void Exit()
		{
			Log.Debug().WriteLine("Exit");
		}

		public void ShowSomething()
		{
			Log.Debug().WriteLine("ShowSomething");
		}

		public void Update()
		{
			Log.Debug().WriteLine("Update");
		}
	}
}