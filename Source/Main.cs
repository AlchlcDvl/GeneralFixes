using Witchcraft.Utils;

namespace Fixes;

[SalemMod]
[SalemMenuItem]
[DynamicSettings]
public class Fixes
{
    public void Start()
    {
        Logging.InitVoid("GeneralFixes");
        Logging.LogMessage("Fixed!", true);
    }
}