using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScript : MonoBehaviour
{

	public Transform[] checkpointsP1;
	public Transform[] checkpointsP2;
	public GameObject player1;
	public GameObject player2;

	private int index = 0;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			++index;
			player1.transform.position = checkpointsP1[index].position;
			player2.transform.position = checkpointsP2[index].position;
		}
	}
}