using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public GameObject door;
    public GameObject[] laser;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "SwitchRiddleLight")
        {
            if (door.activeSelf)
            {
                door.SetActive(false);
                foreach( GameObject g in laser)
                {
                    g.SetActive(false);
                }
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "SwitchRiddleLight")
    //        if (!door.activeSelf)
    //            door.SetActive(true);
    //}
}
