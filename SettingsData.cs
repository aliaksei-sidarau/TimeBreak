
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

    public TimeSpan GetTimeToBreak() => TimeSpan.FromHours(HoursInterval) - TimeSpan.FromMinutes(BreakMinuts);
    
    public int GetBreakInterval() => GetTimeToBreak().ToInterval();
}