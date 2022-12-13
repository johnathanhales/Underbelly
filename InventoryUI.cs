using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public RawImage[] slot = new RawImage[5];
    public int cursorIndex;

    // Start is called before the first frame update
    void Start()
    {
        cursorIndex = 0;

    }

    private void Awake()
    {
        cursorIndex = 4;
        CheckInventoryCursor();
    }
    // Update is called once per frame
    void Update()
    {
        CheckInventoryCursor();
    }

    void CheckInventoryCursor()
    {
        if(Input.GetMouseButtonDown(1))
        {
            cursorIndex += 1;
            if (cursorIndex == 5)
                cursorIndex = 0;

            slot[0].enabled = false;
            slot[1].enabled = false;
            slot[2].enabled = false;
            slot[3].enabled = false;
            slot[4].enabled = false;

            slot[cursorIndex].enabled = true;
            Debug.Log(cursorIndex);
        }
            
    }
}
