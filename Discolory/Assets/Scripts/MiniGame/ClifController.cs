using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClifController : MonoBehaviour {

    public GameObject[] stones = new GameObject[4];
    public Vector3[] initStonePosition = new Vector3[4];

    private void Start()
    {
        for(int i = 0; i < stones.Length; i++)
        {
            initStonePosition[i] = stones[i].transform.position;
        }
    }

    public void resetStonePositions()
    {
        for(int i = 0; i < stones.Length; i++)
        {
            stones[i].SendMessage("resetPosition", initStonePosition[i]);
        }
    }
}
