using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCursorBehaviors : MonoBehaviour
{
    public MeshRenderer meshRend;
    public Material greenMat, redMat, blueMat;

    public GameObject warehouse;
    
    void Start()
    {
        meshRend = this.gameObject.GetComponent<MeshRenderer>();
        meshRend.material = blueMat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter");
        meshRend.material = redMat;
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision Exit");
        meshRend.material = blueMat;
    }
}
