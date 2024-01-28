using Home.Shared;
using Witchcraft.Utils;

namespace Fixes;

[HarmonyPatch(typeof(TosAbilityPanelListItem), nameof(TosAbilityPanelListItem.SetKnownRole))]
public static class FixApocNaming
{
    public static void Postfix(TosAbilityPanelListItem __instance, ref Role role)
    {
        Logging.LogMessage("Patching TosAbilityPanelListItem.SetKnownRole");

        if (role != Pepper.GetMyRole() || !role.IsApoc())
            return;

        if (Constants.IsTransformed)
        {
            role = role switch
            {
                Role.BAKER => Role.FAMINE,
                Role.BERSERKER => Role.WAR,
                Role.SOULCOLLECTOR => Role.DEATH,
                Role.PLAGUEBEARER => Role.PESTILENCE,
                _ => role
            };
        }

        ColorUtility.TryParseHtmlString(role.GetFaction().GetFactionColor(), out var color);
        __instance.playerRoleText.color = color;
        __instance.playerRoleText.text = $"({role.ToDisplayString()})";
    }
}

[HarmonyPatch(typeof(RoleCardPanelBackground), nameof(RoleCardPanelBackground.SetRole))]
[HarmonyPriority(Priority.VeryHigh)]
public static class PatchRoleCards
{
    public static void Postfix(RoleCardPanelBackground __instance, ref Role role)
    {
        Logging.LogMessage("Patching RoleCardPanelBackground.SetRole");

        if (Constants.IsTransformed)
        {
            role = role switch
            {
                Role.BAKER => Role.FAMINE,
                Role.BERSERKER => Role.WAR,
                Role.SOULCOLLECTOR => Role.DEATH,
                Role.PLAGUEBEARER => Role.PESTILENCE,
                _ => role
            };
        }

        var panel = __instance.GetComponentInParent<RoleCardPanel>();
        panel.roleNameText.text = role.DisplayString(__instance.currentFaction);

        if (role == Role.JESTER)
            (panel.roleIcon.sprite, panel.roleInfoButtons[0].abilityIcon.sprite) = (panel.roleInfoButtons[0].abilityIcon.sprite, panel.roleIcon.sprite);
    }
}