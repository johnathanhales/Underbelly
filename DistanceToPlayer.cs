using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceToPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public float distance;

    private void Update()
    {
        distance = Vector3.Distance(this.gameObject.transform.position, GameObject.Find("PlayerAvatar").transform.position);
        string distanceString = distance.ToString();
        this.gameObject.GetComponent<TMP_Text>().text = distanceString;
    }

}
