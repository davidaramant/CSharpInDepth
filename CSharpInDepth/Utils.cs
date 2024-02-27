using System.Runtime.CompilerServices;

namespace CSharpInDepth;

public static class Utils
{
    public static void Log(string msg = "", [CallerMemberName] string callingMethod = "") =>
        Console.WriteLine($"{DateTime.Now:s}: {callingMethod}{(msg == "" ? "" : " - " + msg)}");
}
