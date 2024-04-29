using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui_Windows_Studies.Models;
using Maui_Windows_Studies.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace Maui_Windows_Studies.ViewModels;

internal class NoteViewModel : ObservableObject, IQueryAttributable
{
    private Note _note;

    public string Text
    {
        get => _note.Text;
        set
        {
            if (_note.Text != value)
            {
                _note.Text = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime Date => _note.Date;
    public string FileName => _note.FileName;

    public ICommand SaveCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }

    public NoteViewModel(Note note)
    {
        _note = note;

        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    public NoteViewModel(): this(new Note()) { }

    private async Task Save()
    {
        _note.Date = DateTime.Now;
        _note.Save();

        await Shell.Current.GoToAsync($"..?saved={_note.FileName}");
    }

    private async Task Delete()
    {
        _note.Delete();

        await Shell.Current.GoToAsync($"..?deleted={_note.FileName}");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("load", out var note) && note is string noteStr)
        {
            _note = Note.Load(noteStr);
            RefreshProperties();
        }
    }

    public void Reload()
    {
        _note = Note.Load(_note.FileName);
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Text));
        OnPropertyChanged(nameof(Date));
        OnPropertyChanged(nameof(FileName));
    }
}
