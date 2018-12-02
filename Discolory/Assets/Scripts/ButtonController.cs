using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class ButtonController : MonoBehaviour {

    public GameObject interactiveObject;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
    }

    public void triggerEvent()
    {
        vibrateController(0.2f);

        if (interactiveObject.name == "Reset")
        {
            interactiveObject.SendMessage("resetStonePositions");
        }

        if (interactiveObject.tag == "Clif")
        {
            interactiveObject.SendMessage("moveStone");
        }

        if(interactiveObject.tag == "Mirrors")
        {
			interactiveObject.SendMessage("updateButtonName", this.gameObject.name);
            interactiveObject.SendMessage("rotateRight");
        }
    }

    public void vibrateController(float intensiveValue)
    {
        //GamePad.SetVibration(playerIndex, intensiveValue, intensiveValue);
    }

    private void OnTriggerExit(Collider other)
    {
        vibrateController(0);
    }
}
