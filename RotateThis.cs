using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThis : MonoBehaviour
{
    public float x = 0.0f, y = 0.0f, z = 0.0f, speed = 0.0f;
    

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(x * speed, y * speed, z * speed);
    }
}
