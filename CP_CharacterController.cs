using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CompassNavigatorPro;
using UnityEngine.SceneManagement;


public class CP_CharacterController : MonoBehaviour
{
    public GameObject player;

    public Light flashlight;
    public Camera camera, fullmapCamera;
    public GameObject fullmapGO;
    public GameObject playerAvatar;

    public CP_PlayerInfo playerInfo;
    public CP_ScanInfo scanTarget;
    public GameObject infoCluster, scanContainer;
    public TMP_Text nameText, companyText, descriptionText, vCredsText, hpText;
    public GameObject tacticalMap, compassGO;

    public GameObject projectileOrigin, projectile;
    public bool fireReady = true;
    public float duration = 0.5f;

    public int grenadeSelection = 0;
    public TMP_Text grenadeSelectText;
    public GameObject grenadePrefab, fragGrenade, teleportGrenade;
    public bool isThrowingGrenade = false;

    public float visionCreditsAmount;
    public float ammoCostPerRound = 0.5f;

    // Dialogue System ===================
    public GameObject dialogueSystem;
    public TMP_Text dialogueText0;
    public GameObject inventoryGO;
    public InventorySystem inventory;

    public int hitPoints;

    public Quest quest;
    GameObject[] canvases;

    public Vector3 tempLocation;
    public Quaternion tempRotation;

    // Scan UI Environment variables
    public TypewriterText typeText;
    public bool isTyping = false;
    public string targetNameCurrent;

    private void Awake()
    {
        //characterSheet = this.gameObject.GetComponent<CharacterSheet>();
        typeText = this.gameObject.GetComponent<TypewriterText>();
        hitPoints = 100;
    }
    void Start()
    {
        flashlight.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dialogueSystem = GameObject.Find("Dialogue System");
        dialogueSystem.SetActive(false);
        inventory = inventoryGO.GetComponent<InventorySystem>();
        //canvases = new GameObject[];
        player = this.gameObject;
        grenadeSelectText = GameObject.Find("GrenadeSelectionText").GetComponent<TMP_Text>();
        grenadePrefab = fragGrenade;
    }


    // Update is called once per frame
    void Update()
    {
        CheckForFlashlight();
        CharRaycast();
        CheckForScanToggle();
        CheckForGrenadeSelection();
        CheckForFire();
        vCredsText.text = inventory.VCredits.ToString();
        //hitPoints = characterSheet.hitPoints;
        hpText.text = hitPoints.ToString();
        CheckForFullmap();
    }
   
    void CheckForGrenadeSelection()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if (grenadeSelection == 0)
            {
                grenadeSelection = 1;
                grenadeSelectText.text = "Teleport";
            }
            else
            {
                grenadeSelection = 0;
                grenadeSelectText.text = "Frag";
            }
        }
    }
    void CheckForFire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }
        if(Input.GetMouseButtonDown(1))
        {
            if(grenadeSelection == 0)
            {
                grenadePrefab = fragGrenade;
            }
            else
            {
                grenadePrefab = teleportGrenade;
            }
            ThrowGrenade(grenadePrefab);
        }
    }
    void CheckForFullmap()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            fullmapGO.SetActive(!fullmapGO.activeSelf);
            compassGO.SetActive(false);
        }
    }
    void ThrowGrenade(GameObject grenade)
    {
        if(!isThrowingGrenade)
        {
            isThrowingGrenade = true;
            Instantiate(grenade, projectileOrigin.transform.position, projectileOrigin.transform.rotation);
            StartCoroutine(rethrowGrenadeDelay(1.0f));
        }
    }

    IEnumerator rethrowGrenadeDelay(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        isThrowingGrenade = false;
    }

    void FireProjectile()
    {
        if((fireReady || scanContainer.activeSelf == true && visionCreditsAmount > 0 ))
        {
            Instantiate(projectile, projectileOrigin.transform.position, projectileOrigin.transform.rotation);
            fireReady = false;
            visionCreditsAmount -= ammoCostPerRound;
            StartCoroutine(FireProjectileDelay(duration));
        }
    }

    IEnumerator FireProjectileDelay(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        fireReady = true;
    }

    void CheckForScanToggle()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    void ToggleMap()
    {
        tacticalMap.SetActive(!tacticalMap.activeSelf);
    }
    void CheckForFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
            flashlight.enabled = !flashlight.enabled;
    }

    void ProcessInformation(string objectID = "")
    {
        if(objectID == "")
        {
            typeText.ScanText("", "", "");
        }
        else
        {
            Debug.Log(objectID);
            typeText.ScanText(GameObject.Find(objectID).GetComponent<CP_ScanInfo>().objectName, GameObject.Find(objectID).GetComponent<CP_ScanInfo>().objectCompany, descriptionText.text = GameObject.Find(objectID).GetComponent<CP_ScanInfo>().objectDescription);
        }
    }

    void CharRaycast()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 45.0F))
        {
            if (GameObject.Find(hit.collider.name).GetComponent<CP_ScanInfo>() != null)
            {
                scanTarget = GameObject.Find(hit.collider.name).GetComponent<CP_ScanInfo>();
                
                if(scanTarget.name != targetNameCurrent)
                {
                    infoCluster.SetActive(true);
                    if (!isTyping)
                    {
                        isTyping = true;
                        ProcessInformation(hit.collider.name);
                        targetNameCurrent = hit.collider.name;
                        StartCoroutine(TargetShiftDelay());

                    }
                    else
                    {
                        isTyping = false;
                    }
                }
                    
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(scanTarget.hasDialogue)
                    {
                        dialogueSystem.SetActive(true);
                        dialogueText0.text = dialogueSystem.GetComponent<dialogue>().DialogueShow(GameObject.Find(hit.collider.name).GetComponent<CP_ScanInfo>().objectID, GameObject.Find(hit.collider.name).GetComponent<CP_ScanInfo>().dialogueSwitch);
                        StartCoroutine(DialogueDelay());
                    }
                    if(scanTarget.isTeleport)
                    {
                        tempLocation = player.transform.position;
                        tempRotation = player.transform.rotation;
                        SceneManager.LoadScene(scanTarget.sceneIndexToLoad);
                    }
                    if(scanTarget.isGOToggle)
                    {
                        Debug.Log("Activated.");
                        scanTarget.toggledGO.SetActive(!scanTarget.toggledGO.activeSelf);
                    }
                    if(scanTarget.isQuestGiver)
                    {
                        if(scanTarget.GetComponent<QuestGiver>() != null)
                        {
                            scanTarget.GetComponent<QuestGiver>().AcceptQuest();
                        }
                    }
                    
                }
                
            }
            else
            {
                
            }
        }
    }
    IEnumerator TargetShiftDelay()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        isTyping = false;
        yield return new WaitForSecondsRealtime(3.0f);
        ProcessInformation();
    }
    IEnumerator DialogueDelay()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        dialogueSystem.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            hitPoints -= 10;
            if(hitPoints <= 0)
            {
                hitPoints = 100;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex);
            }

        }
    }
}
