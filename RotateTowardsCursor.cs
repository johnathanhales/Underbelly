using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCursor : MonoBehaviour
{
    public GameObject rotateTarget, targetCursor;
    
    // Start is called before the first frame update
    void Start()
    {
        //rotateTarget = GameObject.Find("RotationalCursor");
        //targetCursor = GameObject.Find("TargetCursor");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.parent == targetCursor)
        {
            Debug.Log("Parent is targetcursor");
            this.gameObject.transform.transform.LookAt(rotateTarget.gameObject.transform);
        }
        
    }
}
