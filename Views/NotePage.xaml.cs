namespace Maui_Windows_Studies.Views;

public partial class NotePage : ContentPage
{
	string _fileName = Path.Combine(
		Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
		"studies",
		"note.txt");

	public NotePage()
	{
		InitializeComponent();

		if (File.Exists(_fileName))
        {
            TextEditor.Text = File.ReadAllText(_fileName);
        }
	}

	private void Save_Clicked(object send, EventArgs e)
	{
		Directory.CreateDirectory(Path.GetDirectoryName(_fileName)!);
		File.WriteAllText(_fileName, TextEditor.Text);
	}

    private void Delete_Clicked(object send, EventArgs e)
    {
		if (File.Exists(_fileName))
		{
			File.Delete(_fileName);
		}

        TextEditor.Text = string.Empty;
    }
}