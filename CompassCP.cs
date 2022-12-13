using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassCP : MonoBehaviour
{
    public GameObject iconPrefab;
    List<QuestMarker> questMarkers = new List<QuestMarker>();

    public RawImage compassImage;
    public Transform player;

    float compassUnit;

    public QuestMarker one;

    public float maxDistance = 200f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        compassImage = this.gameObject.GetComponent<RawImage>();
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        AddQuestMarker(one);

    }

    private void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360.0f, 0f, 1f, 1f);
        foreach(QuestMarker marker in questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            float dist = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), marker.position);
            float scale = 0.0f;
            if(dist < maxDistance)
            {
                scale = 1f - (dist / maxDistance);
            }

            marker.image.rectTransform.localScale = Vector3.one * scale;
        }
    }
    public void AddQuestMarker(QuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        questMarkers.Add(marker);
    }
    Vector2 GetPosOnCompass(QuestMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}
