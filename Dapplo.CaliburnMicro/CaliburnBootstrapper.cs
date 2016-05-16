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
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Dapplo.Addons;
using System.Windows;

#endregion

namespace Dapplo.CaliburnMicro
{
	/// <summary>
	///     An implementation of the Caliburn Micro Bootstrapper which is strted from the Dapplo ApplicationBootstrapper (MEF)
	/// </summary>
	[StartupAction(StartupOrder = 100)]
	public class CaliburnBootstrapper : BootstrapperBase, IStartupAction
	{
		[Import]
		private IServiceExporter ServiceExporter { get; set; }

		[Import]
		private IServiceLocator ServiceLocator { get; set; }

		[Import]
		private IServiceRepository ServiceRepository { get; set; }

		/// <summary>
		///     Initialize the Caliburn bootstrapper from the Dapplo startup
		/// </summary>
		/// <param name="token">CancellationToken</param>
		public Task StartAsync(CancellationToken token = default(CancellationToken))
		{
			Initialize();
			var windowManagers = ServiceLocator.GetExports<IWindowManager>();
			if (!windowManagers.Any())
			{
				ServiceExporter.Export<IWindowManager>(new WindowManager());
			}
			OnStartup(this, null);
			return Task.FromResult(true);
		}

		protected override void BuildUp(object instance)
		{
			ServiceLocator.FillImports(instance);
		}

		protected override void Configure()
		{
			foreach (var assembly in AssemblySource.Instance)
			{
				ServiceRepository.Add(assembly);
			}
			ServiceExporter.Export<IEventAggregator>(new EventAggregator());
		}

		/// <summary>
		/// Return all assemblies that the Dapplo Bootstrapper knows of
		/// </summary>
		/// <returns></returns>
		protected override IEnumerable<Assembly> SelectAssemblies()
		{
			return ServiceRepository.KnownAssemblies;
		}

		/// <summary>
		/// Return all instances of a certain service type
		/// </summary>
		/// <param name="serviceType"></param>
		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return ServiceLocator.GetExports(serviceType).Select(x => x.Value);
		}

		protected override object GetInstance(Type serviceType, string key)
		{
			var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
			return ServiceLocator.GetExport(serviceType, contract);
		}

		/// <summary>
		///     This is the startup of the Caliburn bootstrapper, here we can display a view
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<IShell>();
		}
	}
}