
using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject[] inventorySlot;
    
    public float VCredits = 0;

    void Start()
    {
        inventorySlot = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(GameObject item)
    {

    }

    public void AddCredits(float amount)
    {
        VCredits += amount;

    }
}
