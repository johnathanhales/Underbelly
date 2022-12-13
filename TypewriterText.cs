using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TypewriterText : MonoBehaviour
{
    public EffectsLibraryScript sound;
    public TMP_Text defaultTextField;
    public TMP_Text showText;
    public string textToBuild;
    public float duration;

    // Scan Text Fields
    public TMP_Text scanName, scanCompany, scanDescription, grenadeText;

    private void Start()
    {
        sound = GameObject.Find("EffectsNode").GetComponent<EffectsLibraryScript>();
        defaultTextField = GameObject.Find("DescriptionTextField").GetComponent<TMP_Text>();
        showText.text = " ";
        
    }

    public void ScanText(string nameMessage, string companyMessage, string descriptionMessage)
    {
        scanName.text = "";
        scanCompany.text = "";
        scanDescription.text = "";

        BuildText(scanName, nameMessage);
        BuildText(scanCompany, companyMessage);
        BuildText(scanDescription, descriptionMessage);
    }

    public void GrenadeText(string message)
    {
        
    }

    public void BuildText(TMP_Text textField, string message = "Sample Text", float duration = 0.05f)
    {
        StartCoroutine(BuildTextDelay(textField, message, duration));
    }
    IEnumerator BuildTextDelay(TMP_Text textField, string message, float duration)
    {
        for(int i = 0; i < message.Length; i++)
        {
            sound.PlayTick();
            textField.text = string.Concat(textField.text, message[i]);
            yield return new WaitForSecondsRealtime(duration);
        }
    }
}
