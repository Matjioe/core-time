using UnityEngine;
using System.Collections;

public class GeneralTimeControllers : MonoBehaviour
{
	public GameObject captureParent;

	public void Quit()                { Application.Quit(); }
	public void SetTimeScale(float t) { Time.timeScale = t; }
	public void ReloadLevel()         { Application.LoadLevel(0); }

	void Start()
	{
	}

	public void CaptureAll()
	{
		for (int i = 0; i < captureParent.transform.childCount; ++i)
		{
			captureParent.transform.GetChild(i).GetComponent<TimeToUIText>().StartCapture();
		}
	}
}
