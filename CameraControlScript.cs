using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class CameraControlScript : MonoBehaviour
{
    public GameObject groundCamGO, flyCamGO;
    public FreeFlyCamera flyCameraController;
    public FirstPersonController groundCameraController;
    public Camera groundCamera, flyCamera;
    public AudioListener flyCamAudioListen, groundCamAudioListen;

    public GameObject activeCamera;
    private Camera camera;

    public TMP_Text xScaleText, yScaleText, zScaleText, rangeText, targetNameText, positionText, bearingText;

    //'bearingText' is simply a translated rotational value converted into a 2 decimal place double value.


    // Start is called before the first frame update
    void Start()
    {
        // Get rid of cursor. O_O
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        activeCamera = groundCamGO;

        flyCameraController.enabled = false;
        groundCameraController.enabled = true;

        flyCamera.enabled = false;
        groundCamera.enabled = true;

        groundCamAudioListen.enabled = true;
        flyCamAudioListen.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Camera state toggle.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            flyCameraController.enabled = !flyCameraController.enabled;
            groundCameraController.enabled = !groundCameraController.enabled;
            flyCamera.enabled = !flyCamera.enabled;
            groundCamera.enabled = !groundCamera.enabled;

            groundCamAudioListen.enabled = !groundCamAudioListen.enabled;
            flyCamAudioListen.enabled = !flyCamAudioListen.enabled;
        }

        //Check for active camera.
        if(groundCamera.enabled)
        {
            activeCamera = groundCamGO;
        }
        if(flyCamera.enabled)
        {
            activeCamera = flyCamGO;
        }
    }

    private void FixedUpdate()
    {
        CameraRaycast();
    }

    private void CameraRaycast()
    {
        CheckForActiveCamera();

        Ray ray = CheckForActiveCamera().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 45.0F))
        {
            if (GameObject.Find(hit.collider.name).GetComponent<BuildingStats>() != null)
            {
                // volume === length * width * height
                float volume = hit.collider.transform.localScale.x * hit.collider.transform.localScale.y * hit.collider.transform.localScale.z;

            }
            Debug.Log(hit.collider.name);
            Debug.Log(CheckForActiveCamera());
        }
    }

    private Camera CheckForActiveCamera()
    {
        if(flyCamera.enabled)
        {
            return flyCamera;
        }
        else if(groundCamera.enabled)
        {
            return groundCamera;
        }
        else
        {
            return groundCamera;
        }
        
    }
}

