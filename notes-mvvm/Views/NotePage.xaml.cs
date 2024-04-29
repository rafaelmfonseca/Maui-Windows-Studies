using Maui_Windows_Studies.Models;
using Maui_Windows_Studies.Shared;

namespace Maui_Windows_Studies.Views;

[QueryProperty(nameof(FileName), nameof(FileName))]
public partial class NotePage : ContentPage
{
	public string FileName
	{
		set => LoadNote(value);
	}

	public NotePage()
	{
		InitializeComponent();

		string fileName = Path.Combine(Constants.NotesFolder, $"{Path.GetRandomFileName()}.notes.txt");

		LoadNote(fileName);
    }

	private void LoadNote(string fileName)
    {
		var note = new Note()
		{
			FileName = fileName
		};

		if (File.Exists(fileName))
		{
			note.Text = File.ReadAllText(fileName);
			note.Date = File.GetCreationTime(fileName);
		}

		BindingContext = note;
    }

	private async void Save_Clicked(object send, EventArgs e)
	{
		if (BindingContext is Note note)
		{
			Directory.CreateDirectory(Constants.NotesFolder);
			File.WriteAllText(note.FileName, note.Text);
		}

		await Shell.Current.GoToAsync("..");
	}

    private async void Delete_Clicked(object send, EventArgs e)
    {
		if (BindingContext is Note note && File.Exists(note.FileName))
		{
			File.Delete(note.FileName);
		}

		await Shell.Current.GoToAsync("..");
    }
}