using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {

    public ControllerSettings conSettings;

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Switch")
        {
            if(Input.GetButtonDown(conSettings.interactionButton))
                other.gameObject.transform.Rotate(new Vector3(0, 1, 0), 90);
        }
    }
}
