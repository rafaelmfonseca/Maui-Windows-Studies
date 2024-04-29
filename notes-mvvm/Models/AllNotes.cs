using Maui_Windows_Studies.Shared;
using System.Collections.ObjectModel;

namespace Maui_Windows_Studies.Models;

internal class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = [];

    public AllNotes()
    {
        LoadNotes();
    }

    public void LoadNotes()
    {
        Notes.Clear();

        if (!Directory.Exists(Constants.NotesFolder))
        {
            return;
        }

        IEnumerable<Note> notes = Directory.EnumerateFiles(Constants.NotesFolder, "*.notes.txt")
            .Select(fileName => new Note()
            {
                FileName = fileName,
                Text = File.ReadAllText(fileName),
                Date = File.GetCreationTime(fileName)
            })
            .OrderByDescending(note => note.Date);

        foreach (Note note in notes)
        {
            Notes.Add(note);
        }
    }
}
