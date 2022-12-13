using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingConditions : MonoBehaviour
{
    bool isReady = false;
    public Vector3 startingPosition;
    public Quaternion startingRotation;
    // public GameObject testActors;

    
    void FixedUpdate()
    {
        if(!isReady)   
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                // testActors.SetActive(false);
                GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
                playerGO.transform.position = startingPosition;
                playerGO.transform.rotation = startingRotation;
                isReady = true; 
            }
        }
    }
}
