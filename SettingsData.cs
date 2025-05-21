
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

    public int GetBreakInterval() => 1000 * 10;
        //HoursInterval.HoursToInterval() - BreakMinuts.MinutesToInterval();
}