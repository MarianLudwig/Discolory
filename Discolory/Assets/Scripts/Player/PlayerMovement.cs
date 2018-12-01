using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ControllerSettings
{
	public string leftHorizontal = "leftHorizontal_P1";
	public string leftVertical = "leftVertical_P1";
	[Tooltip("Right Stick x-Axis (4th axis")]
	public string rightHorizontal = "rightHorizontal_P1";
	[Tooltip("Right Stick y-Axis (5th axis)")]
	public string rightVertical = "rightVertical_P1";
	public string interactionButton = "Interaction_P1";
	public string changeButton = "Change_P1";
	public string rightTrigger = "rightTrigger_P1";
}

public enum PrimaryColors
{
	Red,
	Blue,
}
public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	ControllerSettings conSettings;

	public float walkingSpeed;
	public float bodyRotSpeed;
	public float headRotSpeed;

	// for managing lightbeam stuff
	public GameObject staff;

	public PlayerAnimation playerAnim;

	private float minAngle = 45f;
	private float maxAngle = -45f;

	private float cameraAngle;

	[SerializeField]
	public PrimaryColors primaryColor;
	private Color primColor;
	private bool primaryColorActive; //primary color or yellow active?

	private bool changingGem;
	private bool isRightTriggerInUse;

	private Vector3 spawnPos;
	// Use this for initialization
	void Start()
	{
		spawnPos = transform.position;
		//start with primaryColor
		primaryColorActive = true;  
		if (primaryColor == PrimaryColors.Red)
			primColor = Color.red;
		else
			primColor = Color.blue;
		staff.GetComponent<StaffBehaviour>().ChangeGem(primColor);
	}

	void Reset()
	{
		transform.position = spawnPos;
		changingGem = isRightTriggerInUse = false;
	}

	// Update is called once per frame
	void Update()
	{
		#region INPUT
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
			//transform.GetChild(0).Rotate(Vector3.right, Input.GetAxis(conSettings.rightVertical) * headRotSpeed * Time.deltaTime);
		}
		if (Input.GetButtonDown(conSettings.interactionButton))
		{

		}

		playerAnim.MoveAnim(Input.GetAxis(conSettings.leftVertical), Input.GetAxis(conSettings.leftHorizontal), -Input.GetAxis(conSettings.rightVertical), headRotSpeed*Time.deltaTime);

		// Return if animation currently played
		if (changingGem)
			return;

		if (Input.GetButtonDown(conSettings.changeButton))
		{
			Debug.Log("Changing gem");
			playerAnim.Cast(false);
			staff.GetComponent<StaffBehaviour>().DeactivateLightBeam();
			//changingGem = true;
			if (primaryColorActive)
			{
				staff.GetComponent<StaffBehaviour>().ChangeGem(Color.yellow);
			}
			else
			{
				staff.GetComponent<StaffBehaviour>().ChangeGem(primColor);
			}

			primaryColorActive = !primaryColorActive;
		}
		Debug.Log(Input.GetAxis(conSettings.rightTrigger));
		if (Input.GetAxisRaw(conSettings.rightTrigger) == 1)
		{
			//Debug.Log("Press right trigger");
			if (isRightTriggerInUse == false)
			{
				// Activate light beam		
				staff.GetComponent<StaffBehaviour>().ActivateLightBeam();
				playerAnim.Cast(true);
				isRightTriggerInUse = true;
			}
		}
		if (Input.GetAxisRaw(conSettings.rightTrigger) != 1)
		{
			//Debug.Log("Release right trigger");
			// Activate light beam		
			staff.GetComponent<StaffBehaviour>().DeactivateLightBeam();
			playerAnim.Cast(false);
			isRightTriggerInUse = false;
		}
		#endregion
	}

	void Die()
	{
		transform.position = spawnPos;
	}

	// Setter
	public void SetChangingGem(bool isChanging)
	{
		changingGem = isChanging;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Checkpoint")
		{
			spawnPos = other.transform.position;
			Destroy(other.gameObject);
		}
		if (other.tag == "Deathzone")
		{
			Die();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "EnergySource")
		{
			if (Input.GetButtonDown(conSettings.interactionButton))
			{
				// Reloading energy, TODO: Sound, PSystem, Anim
				staff.GetComponent<StaffBehaviour>().ReloadNRG();
			}
		}
	}
}
