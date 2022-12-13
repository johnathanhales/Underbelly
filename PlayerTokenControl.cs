using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTokenControl : MonoBehaviour
{

    public float movementRate = 1.0f;

    void Update()
    {
        CheckForMovement();
    }

    void CheckForMovement()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            this.gameObject.transform.position += Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.gameObject.transform.position += -Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.gameObject.transform.position += -Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.gameObject.transform.position += Vector3.left;
        }
    }
}
