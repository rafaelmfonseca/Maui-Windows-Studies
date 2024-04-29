using Maui_Windows_Studies.Views;

namespace Maui_Windows_Studies;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));
		Routing.RegisterRoute(nameof(AllNotesPage), typeof(AllNotesPage));
    }
}
