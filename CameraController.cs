using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraController : MonoBehaviour
{

    public Camera cam;
    public string currentTarget;
    public TMP_Text targetText;
    public GameObject targetCursor;
    public Vector3 minimumCursorPosition = new Vector3(0.0f, 0.0f, 0.5f);
    public float cursorPosValueChange = 0.5f;

    public float speed = 2.0f;
    public Vector3 movement;
    public float rotSpeed = 0.05f;

    
    public Camera warehouseCamera;
    public GameObject currentWarehouseObjectSelected, warehouseUI, informationUI;

    public GameObject objectContainer, worldObjectContainer;
    public Vector3 scaleFactor;

    public Transform terrainTransform;
    public TMP_Text rotationText;

    public int objectCount;

    void Start()
    {
        scaleFactor = new Vector3(10.0f, 10.0f, 10.0f);
        //cursorPosValueChange = new Vector3(0.0f, 0.0f, 0.5f);
    }

    void Update()
    {
        CheckForCursorRangeChange();
        UIToggle();
        ChangeObject();
        ChangeObjectOrientation();
        PlaceObject();
        CheckForFileOps();
    }

    void FixedUpdate()
    {
        CameraRaycast();
    }

    void ChangeObject()
    {
        //sint 
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {

            Destroy(currentWarehouseObjectSelected);
            //Instantiate();
        }
    }

    void ChangeObjectOrientation()
    {
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            Quaternion zero = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            targetCursor.transform.GetChild(0).gameObject.transform.localRotation = zero;
        }
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if(targetCursor.transform.childCount > 0)
            {
                targetCursor.transform.GetChild(0).gameObject.transform.Rotate(0.0f, rotSpeed / 5, 0.0f);
            }
        }
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
        {
            if (targetCursor.transform.childCount > 0)
            {
                targetCursor.transform.GetChild(0).gameObject.transform.Rotate(0.0f, -rotSpeed / 5, 0.0f);
            }
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            targetCursor.transform.GetChild(0).gameObject.transform.Rotate(Vector3.left * 1.0f, Space.Self);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            Debug.Log("Scroll Down");
            targetCursor.transform.GetChild(0).gameObject.transform.Rotate(Vector3.right * 1.0f, Space.Self);
        }
        if(Input.GetMouseButtonDown(3) || Input.GetMouseButton(3))
        {
            targetCursor.transform.GetChild(0).gameObject.transform.Rotate(Vector3.right * 1.0f, Space.Self);
        }
        if(rotationText.enabled)
        {
            rotationText.text = targetCursor.transform.GetChild(0).gameObject.transform.rotation.eulerAngles.ToString();
        }
    }
        

    void PlaceObject()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject objectToBePlaced = targetCursor.transform.GetChild(0).gameObject;
            objectToBePlaced.transform.parent = worldObjectContainer.transform;
            LoadShape();
        }
        
    }

    void CheckForFileOps()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            GameObject[] userObjects = GameObject.FindGameObjectsWithTag("UserObject");

            string destination = Application.persistentDataPath + "/save.dat";
            Debug.Log("SaveTo: " + destination);
            if(!File.Exists(destination))
            {
                using (StreamWriter sw = File.CreateText(destination))
                {
                    foreach(GameObject go in userObjects)
                    {
                        sw.WriteLine("GO_ " + go.name + "_ pos_" + go.transform.position.ToString() + "_ rot_" + go.transform.rotation.ToString() + "_ scl_" + go.transform.localScale.ToString()+"_");
                    }
                }
            }
            

        }
    }
    void LoadShape()
    {
        GameObject child = objectContainer.transform.GetChild(0).gameObject;
        child.transform.localScale = scaleFactor;
        Instantiate(child, targetCursor.transform.position, terrainTransform.rotation, targetCursor.transform);
    }
    void UIToggle()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!warehouseCamera.enabled)
            {
                LoadShape();
                rotationText.enabled = true;
            }
            else
            {
                Destroy(targetCursor.transform.GetChild(0).gameObject);
                rotationText.enabled = false;
            }
            warehouseUI.SetActive(!warehouseCamera.enabled);
            warehouseCamera.enabled = !warehouseCamera.enabled;
            
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            informationUI.SetActive(!informationUI.activeSelf);
        }
    }

    void CheckForCursorRangeChange()
    {

        if(Input.GetKeyDown(KeyCode.Keypad9))
        {
            Debug.Log("Keypad 9");
            targetCursor.transform.position += cam.transform.forward * cursorPosValueChange;
            //targetCursor.transform.position = targetCursor.transform.position + cam.transform.forward * cursorPosValueChange * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("Keypad 3");
            //targetCursor.transform.position = targetCursor.transform.position + -cam.transform.forward * cursorPosValueChange * Time.deltaTime;
            targetCursor.transform.position -= cam.transform.forward * cursorPosValueChange;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Debug.Log("Keypad 7");
            targetCursor.transform.position += cam.transform.forward * 0.1f;
            //targetCursor.transform.position = targetCursor.transform.position + cam.transform.forward * cursorPosValueChange * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("Keypad 1");
            //targetCursor.transform.position = targetCursor.transform.position + -cam.transform.forward * cursorPosValueChange * Time.deltaTime;
            targetCursor.transform.position -= cam.transform.forward * 0.1f;
        }
    }

    void CameraRaycast()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50.0F))
        {
            if (GameObject.Find(hit.collider.name).GetComponent<TargetInfo>() != null)
            {
                currentTarget = hit.collider.name;
                targetText.text = currentTarget;
                if (Input.GetKeyDown(KeyCode.E))
                {

                }
                if(Input.GetKeyDown(KeyCode.Keypad8))
                {
                    movement = new Vector3(1.0f, 0, 0);
                    GameObject targetObjectGO = GameObject.Find(hit.collider.name);
                    targetObjectGO.transform.Translate(movement * speed);
                }
            }
            else
            {
                targetText.text = "--NULL--";
            }
        }
    }
}
