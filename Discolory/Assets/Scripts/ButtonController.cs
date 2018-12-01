using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public GameObject interactiveObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void triggerEvent()
    {
        if (interactiveObject.tag == "Clif")
        {
            interactiveObject.SendMessage("moveStone");
        }

        if(interactiveObject.tag == "Mirrors")
        {
            interactiveObject.SendMessage("rotateRight");
        }
    }
}
