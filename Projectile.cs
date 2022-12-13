using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosion, projectileOrigin, flare, pointLight;
    Rigidbody rb;
    public float thrust;
    MeshRenderer mr;
    public float lifeDuration = 8.0f;
    bool hasCollided = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mr = this.GetComponent<MeshRenderer>();
        mr.enabled = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(this.gameObject.transform.rotation * Vector3.forward * thrust);
        StartCoroutine(ProjectileLifeSpan());
    }

    IEnumerator ProjectileLifeSpan()
    {
        yield return new WaitForSecondsRealtime(lifeDuration);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter()
    {

            if (!hasCollided)
            {
                explosion.SetActive(true);
                StartCoroutine(DestroyDelay(lifeDuration / 2));
                hasCollided = true;
                mr.enabled = false;
                flare.SetActive(false);
                pointLight.SetActive(false);
            }

    }

    IEnumerator DestroyDelay(float duration)
    {
        rb.useGravity = false;
        yield return new WaitForSecondsRealtime(duration);
        
        Destroy(this.gameObject);
    }

   

    
}
