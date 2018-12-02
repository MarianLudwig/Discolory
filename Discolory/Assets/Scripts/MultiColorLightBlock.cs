using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MultiColors
{
	Orange,
	Green,
	Purple,
}
public class MultiColorLightBlock : MonoBehaviour
{
	private MultiColors mColor;
	private bool colorOneHit, colorTwoHit, loadingMulti, waitingForSecondColor;
	private float multiLoad;


	public float disappearingSpeed = 1.5f;

	private float lightTime = 1f;
	private float maxLightTime = 25f;

	private GameObject childBlock;



	void Start()
	{
		childBlock = transform.GetChild(0).gameObject;
		AssignColor();
		DeactivateChildBlock();
		waitingForSecondColor = true;
	}

	void Update()
	{

	}

	IEnumerator UpdateLightTime()
	{
		while (lightTime > 0)
		{
			lightTime -= disappearingSpeed * Time.deltaTime;
			yield return null;
		}
		DeactivateLightblock();
		waitingForSecondColor = true;
	}

	void ChargeMultiColor(float energyToLoad, Color incomingColor)
	{
		switch (mColor)
		{
			case MultiColors.Orange:
				if (incomingColor == Color.red)
					colorOneHit = true;
				if (incomingColor == Color.yellow)
					colorTwoHit = true;
				break;
			case MultiColors.Green:
				if (incomingColor == Color.blue)
					colorOneHit = true;
				if (incomingColor == Color.yellow)
					colorTwoHit = true;
				break;
			case MultiColors.Purple:
				if (incomingColor == Color.red)
					colorOneHit = true;
				if (incomingColor == Color.blue)
					colorTwoHit = true;
				break;
		}
		// Actual energy loading starts here
		if (colorOneHit && colorTwoHit)
		{
			lightTime += energyToLoad * Time.deltaTime * energyToLoad;
			lightTime = Mathf.Clamp(lightTime, 0, maxLightTime);
			if (waitingForSecondColor)
			{
				ActivateChildBlock();
				Debug.Log("Activated Child Block in bothColorsHit");
				StartCoroutine(UpdateLightTime());
			}
			waitingForSecondColor = false;
			colorOneHit = colorTwoHit = false;
		}
	}

	public void ActivateLightblock(float energyToLoad, Color incomingColor)
	{
		if (waitingForSecondColor)
			childBlock.SetActive(true);
		ChargeMultiColor(energyToLoad, incomingColor);
	}

	public void DeactivateLightblock()
	{
		colorOneHit = colorTwoHit = false;
		waitingForSecondColor = true;
		lightTime = 1f;
		DeactivateChildBlock();
		StopAllCoroutines();
	}

	void ActivateChildBlock()
	{
		childBlock.SetActive(true);
		childBlock.GetComponent<Renderer>().enabled = childBlock.GetComponent<Collider>().enabled = true;
	}

	void DeactivateChildBlock()
	{
		childBlock.SetActive(false);
		childBlock.GetComponent<Renderer>().enabled = childBlock.GetComponent<Collider>().enabled = false;
	}

	void AssignColor()
	{
		if (gameObject.layer == LayerMask.NameToLayer("Orange"))
			mColor = MultiColors.Orange;
		else if (gameObject.layer == LayerMask.NameToLayer("Green"))
			mColor = MultiColors.Green;
		else if (gameObject.layer == LayerMask.NameToLayer("Purple"))
			mColor = MultiColors.Purple;
	}
}
