namespace Fixes;

[HarmonyPatch(typeof(TosAbilityPanelListItem), nameof(TosAbilityPanelListItem.SetKnownRole))]
public static class FixApocNaming
{
    public static void Postfix(TosAbilityPanelListItem __instance, ref Role role)
    {
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

        if (!Constants.IsBTOS2)
            panel.roleNameText.text = role.DisplayString(__instance.currentFaction, !Constants.IsBTOS2);

        if (role == Role.JESTER)
            (panel.roleIcon.sprite, panel.roleInfoButtons[0].abilityIcon.sprite) = (panel.roleInfoButtons[0].abilityIcon.sprite, panel.roleIcon.sprite);
    }
}

[HarmonyPatch(typeof(TosAbilityPanelListItem), nameof(TosAbilityPanelListItem.OverrideIconAndText))]
[HarmonyPriority(Priority.VeryHigh)]
public static class TosAbilityPanelListItem_OverrideIconAndTextFix
{
    public static void Postfix(TosAbilityPanelListItem __instance, ref TosAbilityPanelListItem.OverrideAbilityType overrideType)
    {
        if (overrideType != TosAbilityPanelListItem.OverrideAbilityType.NECRO_ATTACK || Pepper.GetMyRole() != Role.ILLUSIONIST)
            return;

        __instance.choice1Sprite.sprite = Fixes.IlluAbility;
    }
}