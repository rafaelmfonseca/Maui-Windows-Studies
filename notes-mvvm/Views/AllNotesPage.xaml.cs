using Maui_Windows_Studies.Models;

namespace Maui_Windows_Studies.Views;

public partial class AllNotesPage : ContentPage
{
	public AllNotesPage()
	{
		InitializeComponent();

		BindingContext = new AllNotes();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AllNotes allNotes)
        {
            allNotes.LoadNotes();
        }
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NotePage));
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Any())
        {
            var note = (Note)e.CurrentSelection.First();

            await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.FileName)}={note.FileName}");

            if (sender is CollectionView collectionView)
            {
                collectionView.SelectedItem = null;
            }
        }
    }
}