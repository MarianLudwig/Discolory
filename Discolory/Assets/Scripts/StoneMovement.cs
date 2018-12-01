using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMovement : MonoBehaviour {

    private bool moveable;
    private bool reset = false;
    private float finalSpeed;

    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public float speed;
    public Boundary boundary;
    public Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);

    // Use this for initialization
    void Start () {
        finalSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {

        if (moveable)
        {
            if (reset)
            {
                reset = false;
                speed = finalSpeed;
                GetComponent<Rigidbody>().isKinematic = false;
            }
            if (GetComponent<Rigidbody>().position.x >= boundary.xMin)
                GetComponent<Rigidbody>().velocity = movement * speed;
            else
                speed = 0.0f;

            GetComponent<Rigidbody>().position = new Vector3(
                Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
            );
        }
    }

    public void moveStone()
    {
        moveable = true;
    }

    public void resetPosition(Vector3 initPos)
    {
        moveable = false;
        speed = 0.0f;
        reset = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().position = initPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Clif")
        {
            Debug.Log("collision" + gameObject.name);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
