using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayActivator : MonoBehaviour {

	void Start()
	{
		if (Display.displays.Length > 1)
		{
			Display.displays[1].Activate();
			Debug.Log("Display 2 activated");
		}
	}
}
