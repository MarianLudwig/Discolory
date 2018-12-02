using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorPlane : MonoBehaviour {

    public GameObject child;
    public GameObject[] door;

    private string buttonName;
    private LineRenderer lr;

    private bool updateBeam = false;

	// Use this for initialization
	void Start () {
        lr = child.GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if (lr.enabled)
        {
            RaycastHit hit;

            lr.SetPosition(0, this.transform.position + transform.up*0.5f);

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider.tag == "Mirrors")
                {
                    hit.collider.SendMessage("activateLaser");
                    //GameObject.Find(buttonName).GetComponent("ButtonController").SendMessage("vibrateController", 1.0f);
                }

                if(hit.collider.tag == "Goal")
                {
                    door[0].SendMessage("openDoor");
                    door[1].SendMessage("openDoor");
                }

                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                }
            }
            else lr.SetPosition(1, transform.up * 0.5f + transform.forward * 5000);
            updateBeam = true;
        }
        
	}

    public void updateButtonName(string name)
    {
        buttonName = name;
    }

    public void activateLaser()
    {
        if (!lr.enabled)
        {
            lr.enabled = true;
            updateBeam = false;
        }
    }

    public void rotateRight()
    {
        var angles = transform.rotation.eulerAngles;
        angles.y += 0.3f;
        transform.rotation = Quaternion.Euler(angles);

    }

    public void rotateLeft()
    {
        var angles = transform.rotation.eulerAngles;
        if (angles.y > 310-180 || angles.y < 50+180)
        {
            angles.y -= 15;
            transform.rotation = Quaternion.Euler(angles);
        }
    }
}
