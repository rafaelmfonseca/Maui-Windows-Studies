using Maui_Windows_Studies.Shared;

namespace Maui_Windows_Studies.Models;

internal class Note
{
    public string FileName { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }

    public string FullPath
    {
        get
        {
            return Path.Combine(Constants.NotesFolder, FileName);
        }
    }

    public Note()
    {
        FileName = $"{Path.GetRandomFileName()}.notes.txt";
        Date = DateTime.Now;
        Text = string.Empty;
    }

    public void LoadOrThrow(string fileName)
    {
        FileName = fileName;

        if (File.Exists(FullPath))
        {
            Text = File.ReadAllText(FullPath);
            Date = File.GetCreationTime(FullPath);
        }
        else
        {
            throw new FileNotFoundException("Unable to find the file.", FullPath);
        }
    }

    public void Save()
    {
        if (!Directory.Exists(Constants.NotesFolder))
        {
            Directory.CreateDirectory(Constants.NotesFolder);
        }

        File.WriteAllText(FullPath, Text);
    }

    public void Delete()
    {
        if (File.Exists(FullPath))
        {
            File.Delete(FullPath);
        }
    }

    public static IEnumerable<Note> LoadAll()
    {
        if (!Directory.Exists(Constants.NotesFolder))
        {
            return Enumerable.Empty<Note>();
        }

        return Directory.EnumerateFiles(Constants.NotesFolder, "*.notes.txt")
            .Select(fileName => Load(Path.GetFileName(fileName)))
            .OrderByDescending(note => note.Date);
    }

    public static Note Load(string fileName)
    {
        var note = new Note();
        note.LoadOrThrow(fileName);

        return note;
    }
}
