using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace Xamarin.UI.Controls.Demo.Views.Pages
{
	public partial class ControlsPage : ContentPage
	{
		public ControlsPage()
		{
			InitializeComponent();

			BindingContext = new ControlsPageViewModel();
		}
	}

	public class ControlViewModel : BaseViewModel { }

	public class RadialProgressIndicatorViewModel : ControlViewModel
	{
		public RadialProgressIndicatorViewModel()
		{
			ChangePercentageCommand = new Command(ExecuteChangePercentageCommand);
		}

		private async void ExecuteChangePercentageCommand(object obj)
		{
			var newTarget = double.Parse(obj.ToString());


			while (Progress != newTarget)
			{
				if (newTarget > Progress)
					Progress++;
				else
					Progress--;

				await Task.Delay(3);
			}
		}

		public ICommand ChangePercentageCommand { get; set; }

		int _progress;

		public int Progress
		{
			get { return _progress; }
			set
			{
				_progress = value;
				OnPropertyChanged();
			}
		}
	}

	public class ControlsPageViewModel : BaseViewModel
	{
		public ControlsPageViewModel()
		{
			ViewModels = new Dictionary<string, ControlViewModel>();

			ViewModels.Add("RadialProgressIndicator", new RadialProgressIndicatorViewModel());
		}

		public Dictionary<string, ControlViewModel> ViewModels { get; set; }
	}
}
