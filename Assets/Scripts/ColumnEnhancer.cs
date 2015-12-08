using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColumnEnhancer : MonoBehaviour {

	public Color color;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < transform.childCount; i+=2)
		{
			transform.GetChild(i).GetComponent<Image>().color = color;
		}
	}
}
