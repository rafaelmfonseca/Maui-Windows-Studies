using System.Reflection;

namespace Maui_Windows_Studies.Shared;

internal class Constants
{
    public static string NotesFolder => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "notes");
}
