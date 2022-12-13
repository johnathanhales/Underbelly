using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public string title, description;
    public int experienceReward;
    public int creditReward;

    public QuestGoal goal;
}
