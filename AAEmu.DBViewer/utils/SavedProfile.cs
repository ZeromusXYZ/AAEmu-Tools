namespace AAEmu.DBViewer.utils;

public class SavedProfile
{
    public string Name { get; set; }
    public string ServerDdFile { get; set; }
    public string ClientDdFile { get; set; } // currently unused
    public string GamePakFile { get; set; }
    public string Locale { get; set; }

    public override string ToString()
    {
        return Name;
    }
}