using Game.Services;
using Home.Shared;

namespace Fixes;

[HarmonyPatch(typeof(ApplicationController), nameof(ApplicationController.QuitGame))]
public static class ExitGamePatch
{
    public static void Prefix()
    {
        Fixes.LogMessage("Patching ApplicationController.QuitGame");
        Utils.SaveLogs();
    }
}

[HarmonyPatch(typeof(RoleService), nameof(RoleService.Init))]
[HarmonyPriority(Priority.VeryHigh)]
public static class PatchRoleService
{
    public static void Postfix(RoleService __instance)
    {
        Fixes.LogMessage("Patching RoleService.Init");
        __instance.roleInfoLookup[Role.VAMPIRE].sprite = Fixes.Vampire;
        __instance.roleInfoLookup[Role.CURSED_SOUL].sprite = Fixes.CursedSoul;
        __instance.roleInfoLookup[Role.GHOST_TOWN].sprite = Fixes.GhostTown;
    }
}

[HarmonyPatch(typeof(TosAbilityPanelListItem), nameof(TosAbilityPanelListItem.SetKnownRole))]
public static class FixApocNaming
{
    public static void Postfix(TosAbilityPanelListItem __instance, ref Role role)
    {
        Fixes.LogMessage("Patching TosAbilityPanelListItem.SetKnownRole");

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
public static class PatchRoleCards
{
    public static void Postfix(RoleCardPanelBackground __instance, ref Role role)
    {
        Fixes.LogMessage("Patching RoleCardPanelBackground.SetRole");

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

        __instance.GetComponentInParent<RoleCardPanel>().roleNameText.text = role.DisplayString(__instance.currentFaction);
    }
}