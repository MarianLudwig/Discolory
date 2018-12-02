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

    private LightBlockDissolve childBlock;



    void Start()
    {
        childBlock = transform.GetChild(0).GetComponent<LightBlockDissolve>();
        Color col = new Color();
        string layer = LayerMask.LayerToName(gameObject.layer);
        print(layer);
        switch (layer)
        {
            case "Red":
                col = Color.red;
                break;
            case "Yellow":
                col = Color.yellow;
                break;
            case "Blue":
                col = Color.blue;
                break;
            case "Orange":
                col = new Color(1.000f, 0.522f, 0.106f);
                break;
            case "Purple":
                col = new Color(0.694f, 0.051f, 0.788f);
                break;
            case "Green":
                col = Color.green;
                break;
            default:
                col = Color.black;
                break;
        }
        childBlock.SetColor(col);

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
			childBlock.Dissolve(true);
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
		childBlock.Dissolve(true);
		childBlock.GetComponent<Renderer>().enabled = childBlock.GetComponent<Collider>().enabled = true;
	}

	void DeactivateChildBlock()
	{
		childBlock.Dissolve(false);
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
