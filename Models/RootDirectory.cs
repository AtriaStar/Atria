// DO NOT MOVE THIS FILE

using System.Runtime.CompilerServices;

namespace Models;

public static class RootDirectory {
    public static string GetTest()
        => GetSourceFilePathName();

    public static string Get()
        => Directory.GetParent(GetSourceFilePathName())!.Parent!.ToString();

    private static string GetSourceFilePathName([CallerFilePath] string? path = null)
        => path ?? throw new ArgumentNullException(nameof(path));
}
