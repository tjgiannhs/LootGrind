using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveQuestBehavior : MonoBehaviour
{
    Quest quest;
    GameObject powerLevelCalculator;
    GameObject gameManager;
    PowerLevelCalculator powerLevelCalculatorComponent;
    GameFunctions gameFunctions;
    float stathera;
    int noOfTargets;
    int noOfTargetsCompleted=0;
    bool coroutineRunning = false;
    bool questCompleted = false;
    float pauseTime;
    int secondsToComplete;
    int currentPowerLevel;
    int questLevelTarget;

    float sta8era;
    float bash;
    float ek8eths;
    float diaireths;
    float speedFactorBasedOnLevelDifference;
    float speedFactor;
    float questTimePercentLevel1;
    float questTimeMaxLevel;

    private void Awake()
    {
        powerLevelCalculator = GameObject.Find("PowerLevelCalculator").gameObject;
        gameManager = GameObject.Find("GameManager").gameObject;
        powerLevelCalculatorComponent = powerLevelCalculator.GetComponent<PowerLevelCalculator>();
        gameFunctions = gameManager.GetComponent<GameFunctions>();

        sta8era = gameFunctions.getSta8era();
        bash = gameFunctions.getBash();
        ek8eths = gameFunctions.getEk8eths();
        diaireths = gameFunctions.getDiaireths();
        speedFactor = gameFunctions.getSpeedFactor();
        questTimePercentLevel1 = gameFunctions.getQuestTimePercentLevel1();
        questTimeMaxLevel = gameFunctions.getQuestTimeMaxLevel();
    }

    public void setQuest(Quest generatedQuest) { quest = generatedQuest; setDetails(); }

    void setDetails()
    {
        currentPowerLevel = powerLevelCalculatorComponent.getRoundedPowerLevel();
        questLevelTarget = quest.getquestPowerLevelTarget();
        setQuestTypeText(); 
        if (quest.getquestType() == 2) 
        {
            //stathera = 1.0f* currentPowerLevel / questLevelTarget;
            stathera = 1 + (speedFactor - 1) * (currentPowerLevel - questLevelTarget) / Mathf.RoundToInt((sta8era + Mathf.Pow(bash * currentPowerLevel, ek8eths)) / diaireths);
            noOfTargets = UnityEngine.Random.Range(1, 11);
            //print(noOfTargets);
        }
        secondsToComplete = 30 + quest.getquestSpeed() * 15;//dhladh 30 an kathgoria 0-fast, 45 an 1-normal, 60 an 2-slow
        //print("Secs: " + secondsToComplete);
        //gia ta prwta 60 epipeda o xronos gia ta quests einai ligoteros analoga kai me to epipedo tou paikth me syntelesth apo 0.3 ws 1
        secondsToComplete = (int)(secondsToComplete *1.0f* Mathf.Min(questTimePercentLevel1*1.0f + (currentPowerLevel - 1)*1.0f / (questTimeMaxLevel-1) * (1- questTimePercentLevel1) * 1.0f, 1));//*1.0f gia na mhn einai int alla float
        print("Secs: "+ secondsToComplete+", "+ Mathf.Min(questTimePercentLevel1 * 1.0f + (currentPowerLevel - 1) * 1.0f / (questTimeMaxLevel - 1) * (1 - questTimePercentLevel1) * 1.0f, 1));
    }

    void setQuestTypeText() 
    {
        //string questSpeed = "Medium";
        //if (quest.getquestRewardType() == 0) { questSpeed = "Slow"; } else if (quest.getquestRewardType() == 2) { questSpeed = "Fast"; }
        if (quest.getquestRewardQuality() == 0)
        {
            transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (quest.getquestRewardQuality() == 1)
        {
            transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(false);
        }
        else 
        {
            transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(true);
        }
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(gameFunctions.getSlotNameFromIndex(quest.getquestRewardType())+" "+quest.getquestPowerLevelTarget()+"lvl");
    }

    public void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            pauseTime = Time.realtimeSinceStartup;
            // Game is paused, remember the time
        }
        else
        {
            pauseTime = Time.realtimeSinceStartup - pauseTime;

  
            if (quest.getquestType() != 2)//Not Countdown 
            {
                transform.GetChild(2).GetComponent<Image>().fillAmount += 1.0f / secondsToComplete * speedFactorBasedOnLevelDifference /*currentPowerLevel / questLevelTarget*/ * pauseTime;
                if (quest.getquestType() == 0)
                {
                    float secondsRemaining = (1 - transform.GetChild(2).GetComponent<Image>().fillAmount) * secondsToComplete/ speedFactorBasedOnLevelDifference /* * questLevelTarget / currentPowerLevel*/;
                    string minutesRemaining;
                    string secsRemaining;
                    if ((int)(secondsRemaining / 60) <= 9) { minutesRemaining = "0" + (int)(secondsRemaining / 60); } else { minutesRemaining = (int)(secondsRemaining / 60) + ""; }
                    if ((int)(secondsRemaining % 60) <= 9) { secsRemaining = "0" + (int)(secondsRemaining % 60); } else { secsRemaining = (int)(secondsRemaining % 60) + ""; }

                    transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(minutesRemaining + ":" + secsRemaining);
                }
                else
                {
                    transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText((transform.GetChild(2).GetComponent<Image>().fillAmount * 100).ToString("0") + "%");
                }
            }
            else 
            {
                for (int i = 0; i < 2 * pauseTime; i++) 
                {
                    int result = (int)(UnityEngine.Random.Range(1, 121) * stathera/ speedFactorBasedOnLevelDifference/* * questLevelTarget / currentPowerLevel*/);
                    if (result <= noOfTargets)
                    {
                        noOfTargetsCompleted++;
                    }
                }
                
                noOfTargetsCompleted = Mathf.Min(noOfTargetsCompleted,noOfTargets);//so that number of completed isn't higher than target number

                transform.GetChild(2).GetComponent<Image>().fillAmount = 1.0f * noOfTargetsCompleted / noOfTargets;
                transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(noOfTargetsCompleted + "/" + noOfTargets);
                if (!coroutineRunning) { return; }
                StopCoroutine(roll());
            }
            // Game is unpaused, calculate the time passed since the game was paused and use this time to calculate build times of your buildings or how much money the player has gained in the meantime.
        }
    }


    void Update()
    {
        if (quest == null) return;
        if (questCompleted) return;
        if (transform.GetChild(2).GetComponent<Image>().fillAmount == 1 && !questCompleted) 
        {
            //for (int i = 1; i < 5; i++) { transform.GetChild(i).gameObject.SetActive(false); }
            transform.SetAsFirstSibling();
            transform.GetChild(6).gameObject.SetActive(true);
            //transform.GetChild(6).GetComponent<QuestButton>().rollReward();
            gameManager.GetComponent<SoundFX>().playQuestComplete();
            questCompleted = true;
            return;
        }
        if (quest.getquestType() != 2)//Not Countdown 
        {
            currentPowerLevel = powerLevelCalculatorComponent.getRoundedPowerLevel();
            speedFactorBasedOnLevelDifference = 1+ (speedFactor-1) * (currentPowerLevel-questLevelTarget) / Mathf.RoundToInt((sta8era + Mathf.Pow(bash * currentPowerLevel, ek8eths)) / diaireths);
            //if (speedFactorBasedOnLevelDifference == 0) { speedFactorBasedOnLevelDifference = 1; } else if (speedFactorBasedOnLevelDifference < 0) { speedFactorBasedOnLevelDifference = 1 / -speedFactorBasedOnLevelDifference; }
            //print("S: " + speedFactorBasedOnLevelDifference);

            transform.GetChild(2).GetComponent<Image>().fillAmount += 1.0f / secondsToComplete * speedFactorBasedOnLevelDifference /* currentPowerLevel / questLevelTarget*/ * Time.deltaTime;
            if (quest.getquestType() == 0)
            {
                float secondsRemaining = (1 - transform.GetChild(2).GetComponent<Image>().fillAmount) * secondsToComplete/ speedFactorBasedOnLevelDifference /* * questLevelTarget / currentPowerLevel*/;
                string minutesRemaining;
                string secsRemaining;
                if ((int)(secondsRemaining / 60) <= 9) { minutesRemaining = "0" + (int)(secondsRemaining / 60); } else { minutesRemaining = (int)(secondsRemaining / 60) + ""; }
                if ((int)(secondsRemaining % 60) <= 9) { secsRemaining = "0" + (int)(secondsRemaining % 60); } else { secsRemaining = (int)(secondsRemaining % 60) + ""; }

                transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(minutesRemaining + ":" + secsRemaining);
            }
            else
            {
                transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText((transform.GetChild(2).GetComponent<Image>().fillAmount * 100).ToString("0") + "%");
            }   
        }
        else 
        {
            speedFactorBasedOnLevelDifference = 1 + (speedFactor - 1) * (currentPowerLevel - questLevelTarget) / Mathf.RoundToInt((sta8era + Mathf.Pow(bash * currentPowerLevel, ek8eths)) / diaireths);
            //print("S: " + speedFactorBasedOnLevelDifference);

            transform.GetChild(2).GetComponent<Image>().fillAmount = 1.0f * noOfTargetsCompleted / noOfTargets;
            transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(noOfTargetsCompleted + "/" + noOfTargets);
            if (coroutineRunning) return;
            StartCoroutine(roll());
        }
    }

    IEnumerator roll() 
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(0.5f);
        int result = (int)(UnityEngine.Random.Range(1, 2* secondsToComplete + 1) *stathera / speedFactorBasedOnLevelDifference /* * questLevelTarget / currentPowerLevel*/);
        //print("Roll: "+result);
        if (result <= noOfTargets) 
        {
            noOfTargetsCompleted++;
        }
        coroutineRunning = false;
    }

    public int getQuestRewardType() 
    {
        return quest.getquestRewardType();
    }
    
    public int getQuestRewardQuality() 
    {
        return quest.getquestRewardQuality();
    }    
    public int getQuestTargetLevel() 
    {
        return quest.getquestPowerLevelTarget();
    }
}
