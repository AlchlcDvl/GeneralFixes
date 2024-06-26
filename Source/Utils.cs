namespace Fixes;

public static class Utils
{
    public static string DisplayString(this Role role, FactionType factionType, bool includestatus)
    {
        if (role.IsBucket())
        {
            var bucketDisplayString = ClientRoleExtensions.GetBucketDisplayString(role);

            if (!string.IsNullOrEmpty(bucketDisplayString))
                return bucketDisplayString;
        }

        var text = role.ToDisplayString();
        var text2 = "";

        if (includestatus)
        {
            if (role.IsTraitor(factionType))
                text2 = $"\n<color={Constants.TTColor}FF>({Service.Home.LocalizationService.GetLocalizedString("GUI_ROLENAME_202")})</color>";
            else if (Constants.IsLocalVIP)
                text2 = $"\n<color={Constants.VIPColor}FF>({Service.Home.LocalizationService.GetLocalizedString("GUI_ROLENAME_201")})</color>";
        }

        if (text2.Length > 0)
            text2 = $"<size=85%>{text2}</size>";

        return $"<color={factionType.GetFactionColor()}FF>{text}</color>{text2}";
    }

    public static bool IsApoc(this Role role) => role is Role.BERSERKER or Role.WAR or Role.BAKER or Role.FAMINE or Role.SOULCOLLECTOR or Role.DEATH or Role.PLAGUEBEARER or Role.PESTILENCE;
}