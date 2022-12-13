using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehavior : MonoBehaviour
{
    Rigidbody rb;
    public float thrust = 100f;
    public float delay = 3f, countdown;
    public bool hasExploded = false;
    public GameObject explosion;
    public float radius = 10.0f;

    void Start()
    {

        countdown = delay;
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddRelativeTorque(10f, 0f, 0f * thrust);

        rb.AddForce(this.gameObject.transform.rotation * Vector3.forward * thrust * 1.1f);
        rb.AddForce(this.gameObject.transform.rotation * Vector3.up * (thrust / 2));

    }

    void Update()
    {

        countdown -= Time.deltaTime;
        if(countdown <= 0 && !hasExploded)
        {
            hasExploded = true;
            Explode();
        }


    }

    void Explode()
    {
        Debug.Log("Boom!");

        Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Collider[] colliders =  Physics.OverlapSphere(this.gameObject.transform.position, radius);
        foreach (Collider nearbyobject in colliders)
        {
            if(nearbyobject.GetComponent<CP_ScanInfo>() != null)
            {
                nearbyobject.GetComponent<CP_ScanInfo>().hitPoints -= 60;
                Debug.Log(nearbyobject.name + nearbyobject.GetComponent<CP_ScanInfo>().hitPoints.ToString());
            }
        }
        hasExploded = false;
        Destroy(this.gameObject);
        
    }




}
