using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TokenMetadata : MonoBehaviour
{
    public TMP_Text nameText, pointsText, contactText;
    public string tokenName = "Null";
    public float pointsFloat = 1.0f;
    public bool contactBool = false;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
    private void OnMouseOver()
    {
        nameText = GameObject.Find("NameLabel").GetComponent<TMP_Text>();
        pointsText = GameObject.Find("PointsLabel").GetComponent<TMP_Text>();
        contactText = GameObject.Find("ContactLabel").GetComponent<TMP_Text>();

        nameText.text = tokenName;
        pointsText.text = pointsFloat.ToString();
        contactText.text = contactBool.ToString();
        
    }
}
