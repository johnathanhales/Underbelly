using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;


public class CP_ScanInfo : MonoBehaviour
{
    public FirstPersonController player;
    public CP_CharacterController playerScript;
    public string objectID = "defObj0001";
    public string objectName = "Harvey the Prefab";
    public string objectCompany = "VisionGroup";
    public bool isNPC = false;
    [TextArea(3,18)]
    public string objectDescription = "Harvey is a dick.";
    public bool hasDialogue = false;
    public int dialogueSwitch = 0;

    public bool isQuestItem = false;
    public GameObject scanContainer;
    RawImage itemImage;
    public Texture itemImageTexture;
    TMP_Text itemName, itemDescription;

    public bool isQuestGiver = false;
    public string npcID;
    public int npcDialogueSwitch;

    public bool isCredits = false;
    public int creditAmount = 100;
    public EffectsLibraryScript soundEffects;

    public bool isTeleport = false;
    public int sceneIndexToLoad;

    public bool isGOToggle;
    public GameObject toggledGO;

    public bool isCombatant = false;
    public bool canShoot = false;
    public bool movingTowardsPlayer = false;
    public int hitPoints = 50;
    public GameObject explosion;
    public MeshRenderer mesh;
    public GameObject projectile, projectileOrigin;
    public float shotDelayFloat = 2.0f;
    public bool isFiring = false;
    


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        playerScript = player.GetComponent<CP_CharacterController>();
        soundEffects = GameObject.Find("EffectsNode").GetComponent<EffectsLibraryScript>();
        scanContainer = GameObject.Find("InfoCluster");
        //itemImage = GameObject.Find("QuestItemImage").GetComponent<RawImage>();
        //itemName = GameObject.Find("QuestItemName").GetComponent<TMP_Text>();
        //itemDescription = GameObject.Find("QuestItemDescription").GetComponent<TMP_Text>();
        mesh = this.gameObject.GetComponent<MeshRenderer>();
        if(movingTowardsPlayer)
        {
            this.gameObject.GetComponent<EnemyFollow>().enabled = true;
        }

    }

    private void FixedUpdate()
    {
        if (hitPoints <= 0)
        {
            Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Destroy(this.gameObject);
        }

        CheckForFiring();
    }

    void CheckForFiring()
    {
        if(canShoot && !isFiring)
        {
            Instantiate(projectile, projectileOrigin.transform.position, projectileOrigin.transform.rotation);
            isFiring = true;
            StartCoroutine(ShotDelay(shotDelayFloat));
        }
    }
    IEnumerator ShotDelay (float duration)
    {
        yield return new WaitForSecondsRealtime(shotDelayFloat);
        isFiring = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isCredits)
            {
                // if the object collided with is a bunch of credits. 
                GameObject.Find("Inventory System").GetComponent<InventorySystem>().AddCredits(creditAmount);
                Debug.Log("Credits + " + creditAmount.ToString());
                soundEffects.PlayMoney();
                Destroy(this.gameObject);
            }
            if (isQuestItem)
            {

            }

        }
        else
        {
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile" && isCombatant)
        {
            Debug.Log("Hit");
            hitPoints -= 10;
            Debug.Log(this.gameObject.name + " HP: " + hitPoints.ToString());
        }
    }

}
