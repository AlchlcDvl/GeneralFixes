namespace Fixes;

public static class Constants
{
    public static bool IsTransformed
    {
        get
        {
            try
            {
                return Service.Game?.Sim?.info?.roleCardObservation?.Data?.powerUp == POWER_UP_TYPE.HORSEMAN;
            }
            catch
            {
                return false;
            }
        }
    }
    public static bool IsLocalVIP
    {
        get
        {
            try
            {
                return Service.Game?.Sim?.info?.roleCardObservation?.Data?.modifier == ROLE_MODIFIER.VIP;
            }
            catch
            {
                return false;
            }
        }
    }
    public static bool IsLocalTT
    {
        get
        {
            try
            {
                return Service.Game?.Sim?.info?.roleCardObservation?.Data?.modifier == ROLE_MODIFIER.TRAITOR;
            }
            catch
            {
                return false;
            }
        }
    }
    public static string TTColor
    {
        get
        {
            if (ModStates.IsLoaded("curtis.colour.swapper"))
                return ModSettings.GetString("Coven", "curtis.colour.swapper");
            else
                return "#B545FF";
        }
    }
    public static string VIPColor
    {
        get
        {
            if (ModStates.IsLoaded("curtis.colour.swapper"))
                return ModSettings.GetString("Town", "curtis.colour.swapper");
            else
                return "#06E00C";
        }
    }
    public static bool BTOS2Exists => ModStates.IsLoaded("curtis.tuba.better.tos2");
    public static bool IsBTOS2
    {
        get
        {
            try
            {
                return IsBTOS2Bypass();
            }
            catch
            {
                return false;
            }
        }
    }
    private static bool IsBTOS2Bypass() => BTOS2Exists && BetterTOS2.BTOSInfo.IS_MODDED;
}