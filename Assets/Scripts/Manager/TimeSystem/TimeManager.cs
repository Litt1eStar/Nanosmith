using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TimeManager : MonoSingleton<TimeManager>
{
	protected const float oneSecond = 1f;

	public static GameManagerBase GameManager { get; set; }

	public DateTime Now { get; set; }
	private TimeSpan deltaNowTimeSpan;

	private List<TimeModel> timeModels;

    private float currentTime;
    private float startTime;
    private bool isStopWatchStart;

    [HideInInspector]
	public bool onReadyToCounter = true;

	public float deltaTime
	{
		get { return GameManager.IsGamePaused ? 0f : Time.deltaTime; }
	}

	public virtual void SetSafetyTime(DateTime now)
	{
		Now = now;
		deltaNowTimeSpan = DateTime.Now - Now;
	}

	public double GetUnixTimeStamp()
	{
		TimeSpan safeNow = DateTime.Now - Now;
		DateTime now = DateTime.Now.AddMilliseconds(safeNow.TotalMilliseconds);
		double unixTimestamp = (now.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
		return unixTimestamp;
	}

	void Start()
	{
		timeModels = new List<TimeModel>();
		StartCoroutine("AllUpdateTime");
	}

	void Update()
	{
		Now = DateTime.Now - deltaNowTimeSpan;

        if (isStopWatchStart)
        {
            currentTime = Time.time - startTime;
        }
    }

	private IEnumerator AllUpdateTime()
	{
		for (;;)
		{
			if (onReadyToCounter)
				timeModels.ForEach(timeModel => timeModel.UpdateRemainingTime(Now));

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void AddTimeModel(TimeModel timeModel)
	{
		timeModel.onFinish += () => RemoveTimeModel(timeModel);
		timeModels.Add(timeModel);
	}

	public void AddTimeModelList(List<TimeModel> timeModelList)
	{
		timeModelList.ForEach((timeModel) => AddTimeModel(timeModel));
	}

	public void RemoveTimeModel(TimeModel timeModel)
	{
		timeModels.Remove(timeModel);
	}

	public void ClearAllTimeModel()
	{
		timeModels.Clear();
	}

    public void StartTimer()
    {
        isStopWatchStart = true;
        startTime = Time.time;
    }

    public void StopTimer()
    {
        isStopWatchStart = false;
    }

    public void ResetTimer()
    {
        currentTime = 0;
    }

    public string GetCurrentTime()
    {
        TimeSpan t = TimeSpan.FromSeconds(currentTime);
        return string.Format("{0:D2}:{1:D2}",
                            t.Minutes,
                            t.Seconds);
    }
}
