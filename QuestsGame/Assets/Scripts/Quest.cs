using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class Quest
{

    [SerializeField] int questPowerLevelTarget;
    [SerializeField] int questType;
    [SerializeField] int questRewardType;
    [SerializeField] int questRewardQuality;
    [SerializeField] int questSpeed;

     public Quest(int currentRoundedPowerLevel, float sta8era, float bash, float ek8eths, float diaireths) 
    {
        if (currentRoundedPowerLevel <= 10)
        {
            if (currentRoundedPowerLevel >= 5)
            {
                questPowerLevelTarget = UnityEngine.Random.Range(currentRoundedPowerLevel-1, currentRoundedPowerLevel + 2);
            }
            else 
            {
                questPowerLevelTarget = currentRoundedPowerLevel;
            }
        }
        else 
        {
            //questPowerLevelTarget = UnityEngine.Random.Range(currentRoundedPowerLevel - 10, currentRoundedPowerLevel + 10);
            int peri8wrio = Mathf.RoundToInt((sta8era + Mathf.Pow(bash * currentRoundedPowerLevel,ek8eths))/diaireths);
            questPowerLevelTarget = UnityEngine.Random.Range(currentRoundedPowerLevel - peri8wrio, currentRoundedPowerLevel + peri8wrio + 1);
            Debug.Log("currentPowerLevel "+currentRoundedPowerLevel +" -> peri8wrio: "+ peri8wrio);
        }
        questType = UnityEngine.Random.Range(0, 3);
        questSpeed = UnityEngine.Random.Range(0, 3);
        //Debug.Log(questSpeed);
        questRewardQuality = UnityEngine.Random.Range(0, 3);
        questRewardType = UnityEngine.Random.Range(0,15);//14 an tyxaia kathgoria apo tis 14
    }

    public int getquestPowerLevelTarget() { return questPowerLevelTarget; }
    public int getquestType() { return questType; }
    public int getquestRewardType() { return questRewardType; }
    public int getquestRewardQuality() { return questRewardQuality; }
    public int getquestSpeed() { return questSpeed; }

    public void setquestPowerLevelTarget(int target) { questPowerLevelTarget = target; }
    public void setquestType(int type) { questType = type; }
    public void setquestRewardType(int reward) { questRewardType = reward; }
    public void setquestRewardQuality(int quality) { questRewardQuality = quality; }
    public void setquestDifficulty(int speed) { questSpeed= speed; }

}
