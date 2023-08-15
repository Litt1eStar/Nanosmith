using System;

public static class TimeSpanExtension
{
    public static string ToShortDateTime(this TimeSpan span)
    {
        return string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);
    }

    public static string ToSuperShortDateTime(this TimeSpan span)
    {
        return string.Format("{0:D2}:{1:D2}", span.Hours, span.Minutes);
    }
}
