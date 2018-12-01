using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ControllerSettings
{
	public string leftHorizontal;
	public string leftVertical;
	[Tooltip("Right Stick x-Axis (4th axis")]
	public string rightHorizontal;
	[Tooltip("Right Stick y-Axis (5th axis)")]
	public string rightVertical;
	public string interactionButton;
	public string changeButton;
}

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	ControllerSettings conSettings;

	public float walkingSpeed;
	public float bodyRotSpeed;
	public float headRotSpeed;

	private float minAngle = 45f;
	private float maxAngle = -45f;

	private float cameraAngle;

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetAxis(conSettings.leftHorizontal) != 0)
		{
			transform.Translate(Input.GetAxis(conSettings.leftHorizontal) * walkingSpeed * Time.deltaTime, 0, 0);
		}
		if (Input.GetAxis(conSettings.leftVertical) != 0)
		{
			transform.Translate(0, 0, Input.GetAxis(conSettings.leftVertical) * walkingSpeed * Time.deltaTime);
		}
		if (Input.GetAxis(conSettings.rightHorizontal) != 0)
		{
			transform.Rotate(transform.up, Input.GetAxis(conSettings.rightHorizontal) * bodyRotSpeed * Time.deltaTime);
		}
		if (Input.GetAxis(conSettings.rightVertical) != 0)
		{
			// Delete dis; will be done by animation
			transform.GetChild(0).Rotate(Vector3.right, Input.GetAxis(conSettings.rightVertical) * headRotSpeed * Time.deltaTime);
		}
		if (Input.GetButtonDown(conSettings.interactionButton))
		{

		}
		if (Input.GetButtonDown(conSettings.changeButton))
		{

		}
	}
}
