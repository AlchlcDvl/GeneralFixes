using Witchcraft.Utils;

namespace Fixes;

[SalemMod]
[SalemMenuItem]
[DynamicSettings]
public class Fixes
{
    public static Sprite IlluAbility;

    public void Start()
    {
        Logging.InitVoid("GeneralFixes");
        IlluAbility = FromResources.LoadSprite("Fixes.Resources.Illusionist_Ability.png");
        Logging.LogMessage("Fixed!", true);
    }
}