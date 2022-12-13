using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour
{
    public Light headlights;
    public float duration = 2.0f;
    void Start()
    {
        headlights = this.gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LightBlink(duration));
    }

    IEnumerator LightBlink(float duration)
    {
        headlights.enabled = !headlights.enabled;
        yield return new WaitForSecondsRealtime(duration);
    }
}
