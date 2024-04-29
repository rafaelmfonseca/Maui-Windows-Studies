using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Maui_Windows_Studies.ViewModels;

internal class AboutViewModel
{
    public string Title { get; set; } = "Maui Windows Studies";
    public string Description { get; set; } = "A collection of studies on Windows development with .NET MAUI";
    public string Version { get; set; } = "1.0.0";
    public string MoreInfoUrl { get; set; } = "https://example.com";

    public ICommand ShowMoreInfoCommand { get; }

    public AboutViewModel()
    {
        ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
    }

    async Task ShowMoreInfo()
    {
        await Launcher.Default.OpenAsync(MoreInfoUrl);
    }
}
