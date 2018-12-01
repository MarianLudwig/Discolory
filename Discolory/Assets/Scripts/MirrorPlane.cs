using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorPlane : MonoBehaviour {

    public GameObject child;
    public GameObject successText;

    private LineRenderer lr;

    private bool updateBeam = false;

	// Use this for initialization
	void Start () {
        lr = child.GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if(updateBeam == true)
        {
            lr.enabled = false;
        }
        if (lr.enabled)
        {
            RaycastHit hit;

            lr.SetPosition(0, this.transform.position);

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider.tag == "Mirrors")
                {
                    hit.collider.SendMessage("activateLaser");
                }

                if(hit.collider.tag == "Goal")
                {
                    Debug.Log("Got it");
                    successText.SetActive(true);
                }

                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                }
            }
            else lr.SetPosition(1, transform.forward * 5000);
            updateBeam = true;
        }
        
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
        Debug.Log("rotate Right: " + angles.y);
        if (angles.y < 45+180 || angles.y > 295-180)
        {
            angles.y += 15;
            transform.rotation = Quaternion.Euler(angles);
        }

    }

    public void rotateLeft()
    {
        var angles = transform.rotation.eulerAngles;
        Debug.Log("rotate Left: " + angles.y);
        if (angles.y > 310-180 || angles.y < 50+180)
        {
            angles.y -= 15;
            transform.rotation = Quaternion.Euler(angles);
        }
    }
}
