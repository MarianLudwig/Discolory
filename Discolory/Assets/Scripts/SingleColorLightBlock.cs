using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleColorLightBlock : MonoBehaviour
{
	public float disappearingSpeed = 1.5f;

	private float lightTime = 1f;
	private float maxLightTime = 25f;

	private GameObject childBlock;

	void Start()
	{
		childBlock = transform.GetChild(0).gameObject;
		childBlock.SetActive(false);
	}

	void Update()
	{
		if (childBlock.activeInHierarchy)
		{
			lightTime -= disappearingSpeed * Time.deltaTime;
			if (lightTime <= 0)
				DeactivateLightblock();
		}
	}

	public void ActivateLightblock(float energyToLoad)
	{
		lightTime += energyToLoad * Time.deltaTime * energyToLoad;
		lightTime = Mathf.Clamp(lightTime, 0, maxLightTime);
		ActivateChildBlock();
	}

	public void DeactivateLightblock()
	{
		childBlock.SetActive(false);
		lightTime = 1f;
	}

	void ActivateChildBlock()
	{
		childBlock.SetActive(true);
	}


}
