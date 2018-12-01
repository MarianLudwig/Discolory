using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClifController : MonoBehaviour {

    public GameObject[] clifs;

    int count = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Clif")
                {
                    for (int i = 0; i < clifs.Length; i++)
                    {
                        clifs[i].SendMessage("deactivateClif");
                    }
                    hitInfo.transform.SendMessage("activateClif");
                    Debug.Log("It's working!");
                }
                else
                {
                    for(int i = 0; i < clifs.Length; i++)
                    {
                        clifs[i].SendMessage("deactivateClif");
                    }
                    Debug.Log("nopz");
                }
            }
            else
            {
                Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");
        }
    }
}
