using System.CodeDom;

namespace TimeBreak;

public static class TimeExtensions
{
    public static int MinutesToSeconds(this int minutes) =>
        Convert.ToInt32(TimeSpan.FromMinutes(minutes).TotalSeconds);

    public static int MinutesToInterval(this int minutes) =>
        Convert.ToInt32(TimeSpan.FromMinutes(minutes).TotalMilliseconds);

    public static int HoursToInterval(this int hours) =>
        Convert.ToInt32(TimeSpan.FromHours(hours).TotalMilliseconds);
}