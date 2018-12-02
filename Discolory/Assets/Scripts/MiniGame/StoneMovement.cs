using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMovement : MonoBehaviour {

    private bool moveable;
    private bool reset = false;
    private float finalSpeed;
    private Vector3 dirNorm;

    /*
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }*/

    public float speed;
    //public Boundary boundary;
    public Vector3 goalPos = new Vector3(0.0f, -1.0f, 0.0f);

    private GameObject test;

    // Use this for initialization
    void Start () {
        goalPos += this.transform.parent.parent.gameObject.transform.position;
        dirNorm = (goalPos - transform.position).normalized;
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

            Vector3 move = new Vector3(0,0,0);
            //if (GetComponent<Rigidbody>().position.x >= boundary.xMin)
            //{
            if (Vector3.Distance(goalPos, transform.position) <= 1)
            {
                enabled = false;
            }
            else
            {
                GetComponent<Rigidbody>().position = transform.position + dirNorm * speed * Time.deltaTime;
            }
            //GetComponent<Rigidbody>().position = Vector3.MoveTowards(transform.position, goalPos, speed);
            //GetComponent<Rigidbody>().position += this.transform.position - goalPos;
            //GetComponent<Rigidbody>().velocity = movement * speed;
            //move = goalPosition - GetComponent<Rigidbody>().position;

            //}
            //GetComponent<Rigidbody>().velocity = movement * speed;
            //else
            //  speed = 0.0f;
            /*
            GetComponent<Rigidbody>().position = new Vector3(
                0.0f,
                Mathf.Clamp(transform.position.y, boundary.xMin, boundary.xMax),
                Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
            );*/


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
