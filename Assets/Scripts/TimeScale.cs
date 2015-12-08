using UnityEngine;
using System.Collections;

public class TimeScale : MonoBehaviour {
	
	void Update ()
	{
		GetComponent<UnityEngine.UI.Text>().text = Time.timeScale.ToString("#0.0");
	}
}
