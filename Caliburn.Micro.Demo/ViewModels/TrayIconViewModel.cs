using System.ComponentModel.Composition;
using System.Windows;
using Dapplo.CaliburnMicro.NotifyIconWpf;

namespace Caliburn.Micro.Demo.ViewModels
{
	[Export]
	public class TrayIconViewModel : ISetTrayIconInstance
	{
		public ITrayIcon Icon { get; set; }

		public void DoSomething()
		{
			MessageBox.Show("Hello");
		}
	}
}
