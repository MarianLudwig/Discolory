using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffBehaviour : MonoBehaviour
{

	public float energyConsumption = 3f;

	private bool lightBeamActive, reflected;
	public Transform muzzle;
	private LineRenderer lightBeam;
	private int vertexIndex = 0;
	private Vector3 rayStartPos;
	private Vector3 rayDir;
	// Layer 9-14; R - Y(G) - B - O - P - G, only RYB usable for lightbeam, purple objects can be hit by both
	private int validLayermask;
	private Color currentColor;

	// How long/much can the lightBeam be used, decreases in Update while lightBeamActive
	private float lightPower = 100f;

	public Slider powerSlider;

	// Use this for initialization
	void Awake()
	{
		lightBeam = muzzle.GetComponent<LineRenderer>();
		lightBeam.enabled = false;
	}

	void Start()
	{
		rayStartPos = muzzle.transform.position;
		rayDir = muzzle.transform.forward;

	}

	// Update is called once per frame
	void Update()
	{
		if (lightBeamActive)
		{
			if (lightPower <= 0)
			{
				lightPower = 0;
				DeactivateLightBeam();
				return;
			}

			lightBeam.SetPosition(0, muzzle.transform.position);

			RaycastHit hit;
			if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, 100f, validLayermask))
			{
				lightBeam.SetPosition(1, hit.point);
				// TODO Call function on hit object if it is lightbeam-interactable
				if (hit.collider.tag == "SingleColorLightBlock")
				{
					hit.transform.GetComponent<SingleColorLightBlock>().ActivateLightblock(energyConsumption);
					Debug.Log("Load" + energyConsumption);
				}
				else if (hit.collider.tag == "MultiColorLightBlock")
				{
					hit.transform.GetComponent<MultiColorLightBlock>().ActivateLightblock(energyConsumption, currentColor);
				}
				// Reflect 1 Time only allowed
				if ((hit.collider.gameObject.layer == LayerMask.NameToLayer("Reflection")))
				{
					Vector3 reflectDir = Vector3.Reflect(muzzle.transform.forward, hit.normal);
					lightBeam.positionCount = 3;
					rayStartPos = hit.point;
					rayDir = reflectDir;
					reflected = true;
				}
			}
			else
			{
				lightBeam.positionCount = 2;
				reflected = false;
				lightBeam.SetPosition(1, muzzle.transform.position + muzzle.transform.forward * 100f);
			}
			if (reflected)
			{
				RaycastHit hit2;
				if (Physics.Raycast(rayStartPos, rayDir, out hit2, 100f, validLayermask))
				{
					lightBeam.SetPosition(2, hit2.point);
					// TODO Call function on hit object if it is lightbeam-interactable
					if (hit2.collider.tag == "SingleColorLightBlock")
					{
						hit2.transform.GetComponent<SingleColorLightBlock>().ActivateLightblock(energyConsumption);
					}
					else if (hit2.collider.tag == "MultiColorLightBlock")
					{
						hit2.transform.GetComponent<MultiColorLightBlock>().ActivateLightblock(energyConsumption, currentColor);
					}
				}
				else
				{
					//lightBeam.positionCount = 2;
					lightBeam.SetPosition(2, rayStartPos + rayDir * 100f);
				}
			}


			lightPower -= Time.deltaTime * energyConsumption;
			powerSlider.value = lightPower;
		}
	}



	//void ContinueLightBeam(Vector3 pos, Vector3 dir, int vertexIndex)
	//{
	//	Debug.Log("PosCount: " + lightBeam.positionCount + "; v-Index: " + vertexIndex);

	//	RaycastHit hit;
	//	if (Physics.Raycast(pos, dir, out hit, 100f, validLayermask))
	//	{
	//		lightBeam.SetPosition(vertexIndex + 1, hit.point);
	//		// TODO Call function on hit object if it is lightbeam-interactable
	//		if (hit.collider.tag == "SingleColorLightBlock")
	//		{
	//			hit.transform.GetComponent<SingleColorLightBlock>().ActivateLightblock(energyConsumption);
	//		}
	//		else if (hit.collider.tag == "MultiColorLightBlock")
	//		{
	//			hit.transform.GetComponent<MultiColorLightBlock>().ActivateLightblock(energyConsumption, currentColor);
	//		}
	//		// Recursive Behaviour
	//		if ((hit.collider.gameObject.layer == LayerMask.NameToLayer("Reflection")) && (lightBeam.positionCount <= (vertexIndex+2)))
	//		{
	//			//TODO: Reflect ray, cast forward
	//			Vector3 reflectDir = Vector3.Reflect(muzzle.transform.forward, hit.normal);
	//			Debug.DrawRay(hit.point, reflectDir, Color.black);

	//			vertexIndex++;
	//			lightBeam.positionCount++;
	//			Debug.Log("PosCount2: " + lightBeam.positionCount + "; v-Index: " + vertexIndex);
	//			ContinueLightBeam(hit.point+reflectDir.normalized, reflectDir, vertexIndex);
	//		}
	//	}
	//	else
	//	{
	//		lightBeam.SetPosition(vertexIndex+1, pos + dir * 100f);
	//	}
	//}

	public void ActivateLightBeam()
	{
		if (lightPower <= 0)
			return;
		lightBeamActive = true;
		lightBeam.enabled = true;

	}

	public void DeactivateLightBeam()
	{
		lightBeamActive = false;
		lightBeam.enabled = false;
		lightBeam.positionCount = 2;
		reflected = false;
	}

	public void ReloadNRG()
	{
		lightPower = 100f;
	}

	public void ChangeGem(Color changeToColor)
	{
		lightBeam.startColor = changeToColor;
		currentColor = changeToColor;

		// valid layermask = Lightblocks allowed to hit
		if (changeToColor == Color.red)
			validLayermask = LayerMask.GetMask("Default", "Reflection", "Red", "Orange", "Purple");
		else if (changeToColor == Color.yellow)
			validLayermask = LayerMask.GetMask("Default", "Reflection", "Yellow", "Orange", "Green");
		else
			validLayermask = LayerMask.GetMask("Default", "Reflection", "Blue", "Green", "Purple");
	}
}
