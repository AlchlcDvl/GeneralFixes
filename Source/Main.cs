using Witchcraft.Utils;

namespace Fixes;

[SalemMod]
[SalemMenuItem]
[DynamicSettings]
public class Fixes
{
    public static string ModPath => Path.Combine(Path.GetDirectoryName(Application.dataPath), "SalemModLoader", "ModFolders", "Fixes");

    public void Start()
    {
        Logging.InitVoid("Fixes");
        Logging.LogMessage("Fixed!", true);
    }
}