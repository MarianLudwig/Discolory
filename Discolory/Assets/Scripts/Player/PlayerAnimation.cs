using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    [SerializeField] Animator anim;
    [SerializeField] float animTime;

    // Debugging
    /*
    [Space]
    public float forwardInput;
    public float sidewaysInput;
    public float lookVerticalInput;
    public float lookVerticalSpeed;
    public bool cast;
    public bool switchStaff;
    */

    float toWeightCast;
    float toWeightSwitch;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float currentWeightCast = anim.GetLayerWeight(2);
        if (Mathf.Abs(currentWeightCast - toWeightCast) > .01f)
        {
            print("here");
            currentWeightCast = Mathf.Lerp(currentWeightCast, toWeightCast, animTime * Time.deltaTime);
            anim.SetLayerWeight(2, currentWeightCast);
        }

        float currentWeightSwitch = anim.GetLayerWeight(3);
        if (Mathf.Abs(currentWeightSwitch - toWeightSwitch) > .01f)
        {
            currentWeightSwitch = Mathf.Lerp(currentWeightSwitch, toWeightSwitch, animTime * Time.deltaTime);
            anim.SetLayerWeight(3, currentWeightSwitch);
        }
        else if(currentWeightSwitch > .5f)
        {
            toWeightSwitch = 0;
        }
        else
        {
            // TODO: Set changing gem to false
        }

        // Debugging
        /*
        MoveAnim(forwardInput, sidewaysInput, lookVerticalInput, lookVerticalSpeed);
        Cast(cast);
        if (switchStaff)
        {
            switchStaff = false;
            Switch();
        }
        */
    }

    public void MoveAnim(float forwardInput, float sidewaysInput, float lookVerticalInput, float lookVerticalSpeed)
    {
        float currentSpeed = Mathf.Max(Mathf.Abs(forwardInput), Mathf.Abs(sidewaysInput));
        anim.SetFloat("Speed", currentSpeed);

        if(Mathf.Abs(lookVerticalInput) > .1f)
        {
            float angle = anim.GetFloat("LookAngle");
            angle += lookVerticalInput * lookVerticalSpeed;
            angle = Mathf.Clamp(angle, -1, 1);
            anim.SetFloat("LookAngle", angle);
        }
    }

    public void Cast(bool on)
    {
        toWeightCast = on ? 1 : 0;
    }

    public void Switch()
    {
        toWeightSwitch = 1;
    }

    // Debugging
}
