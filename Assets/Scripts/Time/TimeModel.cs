using System;

public class TimeModel 
{
    //public int simpleNumber = 10;
	public DateTime endTime;
	public TimeSpan remainingTime;
    public Action onFinish;
    public Action onChange;
    private bool pause;

	public TimeModel(DateTime endTime, DateTime now, Action onFinish = null, Action onChange = null)
	{
		this.endTime = endTime;
        this.onFinish = onFinish;
        this.onChange = onChange;

        UpdateRemainingTime(now);
    }

    public void UpdateRemainingTime(DateTime now)
    {
        if (pause)
            return;

        remainingTime = endTime - now;
        CheckToCallOnChange();
        CheckToCallOnFinish();
        //UnityEngine.Debug.Log("UpdateRemainingTime :: " + remainingTime);
    }

    private void CheckToCallOnChange()
    {
        if (onChange.IsNotNull())
            onChange();
    }

    private void CheckToCallOnFinish()
    {
        if (onFinish.IsNotNull() && remainingTime.TotalMilliseconds <= 0)
        {
            onFinish();
            onFinish = null;
            onChange = null;
        }
    }

    public void Pause()
    {
        pause = true;
    }

    public void Unpause()
    {
        pause = false;
    }
}
