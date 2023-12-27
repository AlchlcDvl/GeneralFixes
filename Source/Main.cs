using BepInEx.Logging;

namespace Fixes;

[SalemMod]
[SalemMenuItem]
[DynamicSettings]
public class Fixes
{
    private static readonly ManualLogSource Log = BepInEx.Logging.Logger.CreateLogSource("GeneralFixes");
    public static string SavedLogs = "";

    private static Assembly Core => typeof(Fixes).Assembly;

    public static string ModPath => Path.Combine(Path.GetDirectoryName(Application.dataPath), "SalemModLoader", "ModFolders", "Fixes");

    public static Sprite CursedSoul;
    public static Sprite GhostTown;
    public static Sprite Vampire;

    public void Start()
    {
        Core.GetManifestResourceNames().ForEach(x =>
        {
            if (x.EndsWith(".png"))
            {
                var sprite = FromResources.LoadSprite(x);
                UObject.DontDestroyOnLoad(sprite);

                if (x.Contains("CursedSoul"))
                    CursedSoul = sprite;
                else if (x.Contains("Vampire"))
                    Vampire = sprite;
                else if (x.Contains("GhostTown"))
                    GhostTown = sprite;
            }
        });

        LogMessage("Fixed!", true);
    }

    private static void LogSomething(object message, LogLevel type, bool logIt)
    {
        message = $"[{DateTime.UtcNow}] {message}";
        Log?.Log(type, message);
        SavedLogs += $"[{type, -7}] {message}\n";
    }

    public static void LogError(object message) => LogSomething(message, LogLevel.Error, true);

    public static void LogMessage(object message, bool logIt = false) => LogSomething(message, LogLevel.Message, logIt);

    public static void LogFatal(object message) => LogSomething(message, LogLevel.Fatal, true);

    public static void LogInfo(object message, bool logIt = false) => LogSomething(message, LogLevel.Info, logIt);

    public static void LogWarning(object message, bool logIt = false) => LogSomething(message, LogLevel.Warning, logIt);

    public static void LogDebug(object message, bool logIt = false) => LogSomething(message, LogLevel.Debug, logIt);

    public static void LogNone(object message, bool logIt = false) => LogSomething(message, LogLevel.None, logIt);

    public static void LogAll(object message, bool logIt = false) => LogSomething(message, LogLevel.All, logIt);
}