using System.CodeDom;

namespace TimeBreak;

public static class TimeExtensions
{
    public static int MinutesToSeconds(this int minutes) =>
        Convert.ToInt32(TimeSpan.FromMinutes(minutes).TotalSeconds);

    public static int ToInterval(this TimeSpan time) =>
        Convert.ToInt32(time.TotalMilliseconds);
}