using Maui_Windows_Studies.Models;

namespace Maui_Windows_Studies.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		if (this.BindingContext is About about)
		{
			await Launcher.Default.OpenAsync(about.MoreInfoUrl);
		}
    }
}