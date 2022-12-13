using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LookatCamera : MonoBehaviour
{

    public Camera TDCamera;
    public RawImage mapIcon;
    public TMP_Text locationText;

    void Start()
    {
        TDCamera = GameObject.Find("TopDownMapCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mapIcon.transform.LookAt(TDCamera.transform);
        locationText.transform.LookAt(TDCamera.transform);
    }
}
