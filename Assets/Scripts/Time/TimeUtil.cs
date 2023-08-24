using System;

public static class TimeUtil
{
	public static string GetTimeSpan(DateTime currentTime, DateTime endTime)
    {
        var timeSpan = endTime - currentTime;

		string result = GetTimeSpan(timeSpan, true);
        return result;
    }

    public static string GetTimeSpan(int seconds)
    {
        var timeSpan = TimeSpan.FromSeconds(seconds);

		string result = GetTimeSpan(timeSpan, true);
        return result;
    }

	public static string GetTimeSpan(TimeSpan timeSpan, bool pattern)
    {
		return (pattern) ? SetTimeSpanDefault (timeSpan) : SetTimeSpanFormat (timeSpan);
    }

	public static string GetTimeSpan(float secF)
    {
		DateTime tmpDate = TimeManager.Instance.Now;
		tmpDate = tmpDate.AddSeconds(secF);
		TimeSpan timeSpan = tmpDate - TimeManager.Instance.Now;
		return GetTimeSpan(timeSpan, true);
	}

	private static string SetTimeSpanDefault(TimeSpan timeSpan)
	{
		string result = "";
		if (timeSpan.TotalSeconds <= 0) {
			result = "0M 0S";
		} else if (timeSpan.Days > 0) {
			result = timeSpan.Days + "D " + timeSpan.Hours + "H";
		} else if (timeSpan.Hours > 0) {
			result = timeSpan.Hours + "H " + timeSpan.Minutes + "M";
		} else {
			result = timeSpan.Minutes + "M " + timeSpan.Seconds + "S";
		}
		return result;
	}

	private static string SetTimeSpanFormat(TimeSpan timeSpan)
	{
		string result = "";
		if (timeSpan.TotalSeconds <= 0) {
			result = "0M : 0S";
		} else if (timeSpan.Days > 0) {
			result = string.Format("{0:00} Day {1:00} Hour",timeSpan.Days, timeSpan.Hours);
		} else if (timeSpan.Hours > 0) {
			result = string.Format("{0:00} : {1:00} : {2:00}",timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		} else {
			result = string.Format("{0:00} : {1:00}",timeSpan.Minutes, timeSpan.Seconds);
		}
		return result;
	}
}
