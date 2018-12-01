using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClifMovement : MonoBehaviour {

    public Vector3 movement;
    public Vector2 boundaryMin;
    public Vector2 boundaryMax;
    public bool playable = false;

    private bool moveHorizontal, moveVertical;
    private bool negative = false;
    private float coolDown = 0.25f;
    private float timeStamp;
    private bool freze = false;
    private Collision currentCol;

    // Use this for initialization
    void Start () {
        playable = false;
        moveHorizontal = false;
        moveVertical = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (playable)
        {
            float horizontalValue = Input.GetAxis("Horizontal");
            float verticalValue = Input.GetAxis("Vertical");

            if (horizontalValue > 0 && timeStamp <= Time.time)
            {
                timeStamp = Time.time + coolDown;
                moveHorizontal = true;
                negative = false;
            }

            if (horizontalValue < 0 && timeStamp <= Time.time)
            {
                timeStamp = Time.time + coolDown;
                moveHorizontal = true;
                negative = true;
            }

            if (verticalValue > 0 && timeStamp <= Time.time)
            {
                timeStamp = Time.time + coolDown;
                moveVertical = true;
                negative = false;
            }

            if (verticalValue < 0 && timeStamp <= Time.time)
            {
                timeStamp = Time.time + coolDown;
                moveVertical = true;
                negative = true;
            }

            moveStone();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Clif")
        {
            currentCol = col;
            Debug.Log("collision!" + col.transform.name);
        }
        freze = true;
    }

    public void activateClif()
    {
        this.playable = true;
    }

    public void deactivateClif()
    {
        this.playable = false;
    }

    void moveStone()
    {
        if (moveHorizontal)
        {
            moveHorizontal = false;
            if (!negative)
            {
                if (gameObject.transform.position.x + movement.x <= boundaryMax.x)
                {
                    gameObject.transform.position += new Vector3(movement.x, 0.0f, 0.0f);
                }
            }
            else
            {
                if (gameObject.transform.position.x - movement.x >= boundaryMin.x)
                {
                    //gameObject.GetComponent<Rigidbody>().position -= new Vector3(movement.x, 0.0f, 0.0f);#
                    gameObject.transform.position -= new Vector3(movement.x, 0.0f, 0.0f);
                }
            }
        }

        else if (moveVertical)
        {
            if (!negative)
            {
                moveVertical = false;
                if (gameObject.transform.position.z + movement.z <= boundaryMax.y)
                {
                    gameObject.transform.position += new Vector3(0.0f, 0.0f, movement.z);
                }
            }
            else
            {
                if (gameObject.transform.position.z - movement.z >= boundaryMin.y)
                {
                    gameObject.transform.position -= new Vector3(0.0f, 0.0f, movement.z);
                }
            }
        }
    }
}
