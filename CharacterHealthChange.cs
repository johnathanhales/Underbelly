using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthChange : MonoBehaviour
{
    public CP_CharacterController charController;
    public int hitPoints;

    // Start is called before the first frame update
    void Start()
    {
        charController = GameObject.FindGameObjectWithTag("Player").GetComponent<CP_CharacterController>();
        hitPoints = charController.hitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        charController.hitPoints = hitPoints;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            hitPoints -= 10;
        }
    }
}
