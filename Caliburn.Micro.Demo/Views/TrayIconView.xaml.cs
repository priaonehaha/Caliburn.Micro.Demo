using Dapplo.CaliburnMicro.NotifyIconWpf;
using System.Windows.Controls;

namespace Caliburn.Micro.Demo.Views
{
	/// <summary>
	/// Interaction logic for TrayIconView.xaml
	/// </summary>
	public partial class TrayIconView : UserControl, ITrayIconHolder
	{
		public TrayIconView()
		{
			InitializeComponent();
		}

		public ITrayIcon TrayIcon
		{
			get;
			set;
		}
	}
}
