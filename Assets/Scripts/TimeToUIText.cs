using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeToUIText : MonoBehaviour {

	public enum TimeMode
	{
		Time_time,
		Time_unscaledTime,
		Time_realtimeSinceStartup,
		Time_smoothDeltaTime,
		Time_timeSinceLevelLoad,
		Time_unscaledDeltaTime,
		Time_deltaTime,
		DateTime_ticks,   
		DateTime_date_long,
     	DateTime_time_long,
		DateTime_date_short,
     	DateTime_time_short
	};

	public TimeMode mode;
	public GameObject nameText;
	public GameObject captureOutput; // Requires UI.Text component
	public GameObject timeOutput; // Requires UI.Text component
	public GameObject deltaTimeOutput; // Requires UI.Text component

	private float startTime = 0f;

	private int maxCaptureCount = 10;
	private bool capturing = false;
	private int captureCount = 0;
	private float[] captures;

	void Start()
	{
		if (nameText != null)
			nameText.GetComponent<Text>().text = GetName();

		startTime = 0f;

		captures = new float[maxCaptureCount];
		captureCount = 0; // Prevent hot reload to not mess up
		capturing = false;

		StartCapture();
	}

	public void ResetStart()
	{
		float t = 0f;
		string timeString = "";
		GetTime(out t, out timeString);
		startTime = t;
	}

	public void StartCapture()
	{
		capturing = true;
		captureCount = 0;
	}

	void Update()
	{
		float t = 0f;
		string timeString = "";
		GetTime(out t, out timeString);

		if (capturing)
		{
			captures[captureCount] = t;
			captureCount++;
			if (captureCount >= maxCaptureCount)
			{
				capturing = false;
				OnCaptureDone();
			}
		}

		if (timeOutput != null)
			timeOutput.GetComponent<Text>().text = timeString;

		if (deltaTimeOutput != null)
			deltaTimeOutput.GetComponent<Text>().text = (t - startTime).ToString();
	}

	void OnCaptureDone()
	{
		if (captureOutput == null)
			return;

		Text t = captureOutput.GetComponent<Text>();
		t.text = "";
		for (int i = 0; i < captureCount; ++i)
		{
			t.text += (captures[i].ToString() + "\n");
		}
	}

	public string GetName()
	{
		switch (mode)
		{
		case TimeMode.Time_time:                 return "Time.time";
		case TimeMode.Time_unscaledTime:         return "Time.unscaledTime";
		case TimeMode.Time_realtimeSinceStartup: return "Time.realtimeSinceStartup";
		case TimeMode.Time_smoothDeltaTime:      return "Time.smoothDeltaTime";
		case TimeMode.Time_timeSinceLevelLoad:   return "Time.timeSinceLevelLoad";
		case TimeMode.Time_unscaledDeltaTime:    return "Time.unscaledDeltaTime";
		case TimeMode.Time_deltaTime:            return "Time.deltaTime";
		}
		
		switch (mode)
		{
		case TimeMode.DateTime_ticks:      return "DateTime.ticks";
		case TimeMode.DateTime_date_long:  return "DateTime.date_long";
		case TimeMode.DateTime_time_long:  return "DateTime.time_long";
		case TimeMode.DateTime_date_short: return "DateTime.date_short";
		case TimeMode.DateTime_time_short: return "DateTime.time_short";
		}

		return "Unknown"; 
	}

	void GetTime(out float t, out string timeString)
	{
		t = 0f;

		switch (mode)
		{
		case TimeMode.Time_time:                 t = Time.time;                 break;
		case TimeMode.Time_unscaledTime:         t = Time.unscaledTime;         break;
		case TimeMode.Time_realtimeSinceStartup: t = Time.realtimeSinceStartup; break;
		case TimeMode.Time_smoothDeltaTime:      t = Time.smoothDeltaTime;      break;
		case TimeMode.Time_timeSinceLevelLoad:   t = Time.timeSinceLevelLoad;   break;
		case TimeMode.Time_unscaledDeltaTime:    t = Time.unscaledDeltaTime;    break;
		case TimeMode.Time_deltaTime:            t = Time.deltaTime;            break;
		}
		timeString = t.ToString();
		
		switch (mode)
		{
		case TimeMode.DateTime_ticks:      timeString = System.DateTime.Now.Ticks.ToString(); break;
		case TimeMode.DateTime_date_long:  timeString = System.DateTime.Now.ToLongDateString(); break;
		case TimeMode.DateTime_time_long:  timeString = System.DateTime.Now.ToLongTimeString(); break;
		case TimeMode.DateTime_date_short: timeString = System.DateTime.Now.ToShortDateString(); break;
		case TimeMode.DateTime_time_short: timeString = System.DateTime.Now.ToShortTimeString(); break;
		}
	}
}
