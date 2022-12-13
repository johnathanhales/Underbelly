using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeBehavior : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.Find("Player").transform);
        rb.AddForce(Vector3.forward * 20.0f);
    }
}
