
namespace TimeBreak;

public class SettingsData
{
    public static SettingsData Default => new SettingsData
    {
        HoursInterval = 1,
        BreakMinuts = 5,
    };

    public int HoursInterval { get; private set; }

    public int BreakMinuts { get; private set; }

    public int GetBreakInterval() =>
        HoursInterval.HoursToInterval() -
        BreakMinuts.MinutesToInterval();
}