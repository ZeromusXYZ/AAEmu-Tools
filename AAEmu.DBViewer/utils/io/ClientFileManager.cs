using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AAEmu.DBViewer.utils.io;

public static class ClientFileManager
{
    private static bool _initialized = false;
    private static bool _hasValidSources = false;

    private static readonly List<ClientSource> Sources = [];

    /// <summary>
    /// Adds a Client Source location to the ClientFileManager
    /// </summary>
    /// <param name="pathName">Path to a Directory or game_pak file</param>
    /// <returns></returns>
    public static bool AddSource(string pathName)
    {
        if (string.IsNullOrWhiteSpace(pathName))
            return false;

        try
        {
            var p = Path.GetFullPath(pathName);
            pathName = p;
        }
        catch
        {
            // ignored
        }

        // Source is a directory of unpacked client files ?
        if (Directory.Exists(pathName))
        {
            var newSource = new ClientSource { SourceType = ClientSourceType.Directory, PathName = pathName };
            if (newSource.Open())
            {
                Sources.Add(newSource);
                Debug.WriteLine($"Using Source [{Sources.Count}:{newSource.SourceType}]: {pathName}");
                return true;
            }
        }

        // Source is a packed file ?
        if (File.Exists(pathName))
        {
            var newSource = new ClientSource { SourceType = ClientSourceType.GamePak, PathName = pathName };
            if (newSource.Open())
            {
                Sources.Add(newSource);
                Debug.WriteLine($"Using Source [{Sources.Count}:{newSource.SourceType}]: {pathName}");
                return true;
            }
            else
            {
                Debug.WriteLine($"Source could not be opened, or is invalid: {pathName}");
                return false;
            }
        }

        Debug.WriteLine($"Client Source not found {pathName}");
        return false;
    }

    public static void ClearSources()
    {
        _initialized = false;
        _hasValidSources = false;
        for (var i = Sources.Count - 1; i >= 0; i--)
        {
            var source = Sources[i];
            source.Close();
            Sources.Remove(source);
        }
    }

    /// <summary>
    /// Gets the first ClientSource that is valid for target fileName
    /// </summary>
    /// <param name="fileName">filename to search for</param>
    /// <returns>First found ClientSource, or null</returns>
    public static ClientSource GetFileSource(string fileName)
    {
        foreach (var source in Sources)
        {
            if (source.FileExists(fileName))
                return source;
        }
        return null;
    }

    /// <summary>
    /// Checks if target file exists in any of the Sources
    /// </summary>
    /// <param name="fileName">relative filename to check</param>
    /// <returns></returns>
    public static bool FileExists(string fileName)
    {
        var source = GetFileSource(fileName);
        return source != null;
    }

    /// <summary>
    /// Grabs the target fileName as a Stream
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static Stream GetFileStream(string fileName)
    {
        var source = GetFileSource(fileName);
        if (source == null)
        {
            Debug.WriteLine($"GetFileStream({fileName}) not found");
            return null;
        }
        //Logger.Debug($"[{source.PathName}].GetFileStream({fileName})");
        return source.GetFileStream(fileName);
    }

    public static string GetFileAsString(string fileName)
    {
        var source = GetFileSource(fileName);
        if (source == null)
            return string.Empty;
        return source.GetFileAsString(fileName);
    }

    public static void Initialize(List<string> sourcePaths)
    {
        if (_initialized)
            return;

        ClearSources();
        foreach (var source in sourcePaths)
        {
            if (!AddSource(source))
                Debug.WriteLine($"{source} is not a valid source for client data");
        }
        if (Sources.Count <= 0)
            Debug.WriteLine("No valid client Sources have been defined or found, some features will not work !");

        _initialized = true;
        _hasValidSources = FileExists("game/worlds/main_world/world.xml");
    }

    /// <summary>
    /// Create a list of filenames found in target directory
    /// </summary>
    /// <param name="directory">Directory to search in</param>
    /// <param name="searchPattern">search pattern to use</param>
    /// <param name="includeSubDirectories"></param>
    /// <returns>List of filenames</returns>
    public static List<string> GetFilesInDirectory(string directory, string searchPattern, bool includeSubDirectories)
    {
        List<string> list = [];
        var baseList = new List<string>(); // Helper
        foreach (var source in Sources)
        {
            var filesList = source.GetFilesInDirectory(directory, searchPattern, includeSubDirectories);
            foreach (var fName in filesList)
            {
                var baseName = source.GetBaseName(fName);
                if (!baseList.Contains(baseName))
                {
                    list.Add(fName);
                    baseList.Add(baseName);
                }
            }
        }
        return list;
    }

    public static bool HasValidSources()
    {
        return _hasValidSources;
    }
}
