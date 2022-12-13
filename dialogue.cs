using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{

    GameObject dialogueUI;
    TMP_Text dialogueText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string DialogueShow(string speakerID, int dialogueSwitch)
    {
        string dialogueString;
        switch (speakerID)
        {
            case "npcDannyMcKagen0":
                switch (dialogueSwitch)
                {
                    case 3:
                        dialogueString = "Great. See how easy that was?";
                        break;
                    case 1:
                        dialogueString = "Hey, go to Cleaver Circle and grab a file from Bob. Bring it back and we'll get to work.";
                        break;
                    case 0:
                        dialogueString = "Hey, this shit's working.";
                        
                        break;
                    default:
                        dialogueString = "I got nothing to say to you, friendo.";
                        break;
                        
                }
                break;
            case "npcBobTutorial0":
                switch (dialogueSwitch)
                {
                    case 1:
                        dialogueString = "Here you go. Now take this and get away from me.";
                        break;
                    case 0:
                        dialogueString = "What?";
                        break;
                    default:
                        dialogueString = "What?";
                        break;
                }
                break;
            default:
                dialogueString = "";
                Debug.Log("Error in dialogue system. No selected speaker.");
                break;
                return dialogueString;

        }
        return dialogueString;
    }

    
}
