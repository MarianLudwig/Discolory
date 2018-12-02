using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGame : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		LevelTransition.Instance.FadeToScene();
	}
}
