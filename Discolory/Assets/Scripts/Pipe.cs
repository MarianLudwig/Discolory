using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

    public GameObject laserOut;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("TRIGGERED");
        if (other.tag == "SwitchRiddleLight")
            if(!laserOut.activeSelf)
                laserOut.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SwitchRiddleLight")
            if (laserOut.activeSelf)
                laserOut.SetActive(false);
    }
}
