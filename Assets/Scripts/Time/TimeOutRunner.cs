using System;
using System.Collections;
using UnityEngine;

public class TimeOutRunner : MonoBehaviour, IDisposable
{
	public float remainTime { get; private set; }
	public bool isTimeOut { get; private set; }
	public bool isCount { get; private set; }

	private float startTime;

	public void StartCountTimeOut(float timeOut = 30f)
	{
		remainTime = timeOut;
		startTime = Time.time;

		StopCoroutine("OnCountTimeOut");
		StartCoroutine("OnCountTimeOut");
	}

	public void ResumeCountTimeOut()
	{
		if (!isCount && remainTime > 0f)
			StartCountTimeOut(remainTime);
	}

	public void StopCountTimeOut()
	{
		remainTime -= Time.time - startTime;
		//        Debug.Log("Remain time : " + remainTime);
		isTimeOut = remainTime < 0f;
		isCount = false;
		StopCoroutine("OnCountTimeOut");
	}

	protected IEnumerator OnCountTimeOut()
	{
		isCount = true;
		isTimeOut = false;
		yield return new WaitForSeconds(remainTime);
		remainTime = 0f;
		isTimeOut = true;
		isCount = false;
	}

	public void Dispose()
	{
		StopCoroutine("OnCountTimeOut");
	}
}
