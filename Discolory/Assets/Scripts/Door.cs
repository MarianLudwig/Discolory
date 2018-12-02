using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Vector3 targetAngle = new Vector3(0f, 90.0f, 0f);
    public bool open = false;

    private Vector3 currentAngle;

    // Use this for initialization
    void Start () {
        currentAngle = transform.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        if (open)
        {
            currentAngle = new Vector3(
             Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
             Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
             Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));

            transform.eulerAngles = currentAngle;
        }
	}

    public void openDoor()
    {
        open = true;
    }
}
