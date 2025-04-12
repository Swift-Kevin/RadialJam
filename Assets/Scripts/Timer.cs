using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
	public static TimeStamp Start()
	{
		return new TimeStamp()
		{
			time = System.DateTime.Now
		};
	}

	public static long Stop(TimeStamp startTime)
	{
		TimeStamp stopTime = new TimeStamp()
		{
			time = System.DateTime.Now
		};

		return stopTime.Milliseconds - startTime.Milliseconds;
	}

	public static IEnumerator Countdown(float seconds, UnityAction<CountdownStatus> overTimeAction)
	{
		float counter = seconds;
		CountdownStatus status = new CountdownStatus()
		{
			isDone = false,
			progress = 0
		};

		overTimeAction(status);

		yield return new WaitForEndOfFrame();

		while(counter > 0)
		{
			counter -= Time.deltaTime;

			status.progress = (seconds - counter) / seconds;

			overTimeAction(status);

			yield return new WaitForEndOfFrame();
		}

		status.progress = 1;
		status.isDone = true;

		overTimeAction(status);



	}
}

public struct TimeStamp
{
	public System.DateTime time;

	public long Milliseconds
	{
		get
		{
			return time.Ticks / 10000;
		}
	}
}

public struct CountdownStatus
{
	public bool isDone;
	public float progress;
}