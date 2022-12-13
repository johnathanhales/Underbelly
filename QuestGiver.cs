using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver :MonoBehaviour
{

    public Quest quest;
    public CP_CharacterController player;

    public GameObject UIContainer;
    public TMP_Text questNameText, questDescriptionText, questXPRewardText, questCreditRewardText;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<CP_CharacterController>();
        UIContainer = GameObject.Find("ScanContainer");
        questNameText = GameObject.Find("QuestTitleText").GetComponent<TMP_Text>();
        questDescriptionText = GameObject.Find("QuestDescriptionText").GetComponent<TMP_Text>();
        questXPRewardText = GameObject.Find("QuestXPAwardText").GetComponent<TMP_Text>();
        questCreditRewardText = GameObject.Find("QuestCreditAwardText").GetComponent<TMP_Text>();
    }
    public void AcceptQuest()
    {
        questNameText.text = quest.title;
        questDescriptionText.text = quest.description;
        questXPRewardText.text = quest.experienceReward.ToString();
        questCreditRewardText.text = quest.creditReward.ToString();
        quest.isActive = true;

        player.quest = quest;
    }
}
