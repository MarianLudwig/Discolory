using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLaser : MonoBehaviour {

    private LineRenderer lr;
    public GameObject colli;

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        lr.SetPosition(0, transform.position);


        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.tag == "Switch")
            {
                //Debug.Log(hit.collider.tag);
                colli.transform.position = hit.collider.gameObject.transform.position;
            }
            else
            {
                if (colli.transform.position != this.transform.position)
                    colli.transform.position = this.transform.position;
            }

            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward * 5000);
    }
}
