using Dapplo.LogFacade;
using System.ComponentModel.Composition;

namespace Caliburn.Micro.Demo.ViewModels
{
	[Export]
	public class TrayIconViewModel
	{
		private static readonly LogSource Log = new LogSource();

		public void Update()
		{
			Log.Debug().WriteLine("Update");
		}
		public void Exit()
		{
			Log.Debug().WriteLine("Exit");
		}
		public void Configure()
		{
			Log.Debug().WriteLine("Configure");
		}

		public bool CanShowSomething()
		{
			return true;
		}

		public void ShowSomething()
		{
			Log.Debug().WriteLine("ShowSomething");
		}
	}
}
