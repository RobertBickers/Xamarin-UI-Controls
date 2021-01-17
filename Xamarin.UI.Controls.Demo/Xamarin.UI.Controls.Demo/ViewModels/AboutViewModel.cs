using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xamarin.UI.Controls.Demo.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public AboutViewModel()
		{
			Title = "About";
			OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.github.com/robertbickers/xamarin-ui-controls"));
		}

		public ICommand OpenWebCommand { get; }
	}
}