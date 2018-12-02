using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleColorLightBlock : MonoBehaviour
{
	public float disappearingSpeed = 1.5f;

	private float lightTime = 25f;
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
	}

	void Update()
	{
		if (childBlock.IsThere())
		{
			lightTime -= disappearingSpeed * Time.deltaTime;
            childBlock.UpdateIntensity(lightTime);
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
		childBlock.Dissolve(false);
		lightTime = 25f;
	}

	void ActivateChildBlock()
	{
		childBlock.Dissolve(true);
	}


}
