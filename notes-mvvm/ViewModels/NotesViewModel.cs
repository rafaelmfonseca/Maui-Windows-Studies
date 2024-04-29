
using CommunityToolkit.Mvvm.Input;
using Maui_Windows_Studies.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Maui_Windows_Studies.ViewModels;

internal class NotesViewModel : IQueryAttributable
{
    public ObservableCollection<NoteViewModel> AllNotes { get; set; }
    public ICommand NewCommand { get; }
    public ICommand SelectCommand { get; }

    public NotesViewModel()
    {
        AllNotes = new ObservableCollection<NoteViewModel>(Note.LoadAll().Select(n => new NoteViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewNoteCommand);
        SelectCommand = new AsyncRelayCommand<NoteViewModel>(SelectNoteCommand);
    }

    private async Task NewNoteCommand()
    {
        await Shell.Current.GoToAsync(nameof(Views.NotePage));
    }

    private async Task SelectNoteCommand(NoteViewModel note)
    {
        if (note is not null)
        {
            await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={note.FileName}");
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("deleted", out var deleted) && deleted is string deletedStr)
        {
            var matchedNote = AllNotes.FirstOrDefault(n => n.FileName == deletedStr);

            if (matchedNote is not null)
            {
                AllNotes.Remove(matchedNote);
            }
        }
        else if (query.TryGetValue("saved", out var saved) && saved is string savedStr)
        {
            var matchedNote = AllNotes.FirstOrDefault(n => n.FileName == savedStr);

            if (matchedNote is not null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }
            else
            {
                AllNotes.Insert(0, new NoteViewModel(Note.Load(savedStr)));
            }
        }
    }
}
