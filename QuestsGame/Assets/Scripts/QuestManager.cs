using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameObject activeQuestPrefab;
    [SerializeField] GameObject powerLevelCalculator;
    [SerializeField] GameObject activeQuestsContainer;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject activeQuestCounter;

    GameFunctions gameFunctions;
    Quest nextQuest;
    SoundFX soundFX;
    PowerLevelCalculator powerLevelCalculatorComponent;

    int maxActiveQuests = 10;

    private void Start()
    {
        //powerLevelCalculator = GameObject.Find("PowerLevelCalculator").gameObject;
        //activeQuestsContainer = GameObject.Find("ActiveQuests").gameObject;
        //gameManager = GameObject.Find("GameManager").gameObject;

        gameFunctions = gameManager.GetComponent<GameFunctions>();
        soundFX = gameManager.GetComponent<SoundFX>();
        powerLevelCalculatorComponent = powerLevelCalculator.GetComponent<PowerLevelCalculator>();

        createQuest();
    }

    public void createQuest()
    {
        int currentRoundedPowerLevel = powerLevelCalculatorComponent.getRoundedPowerLevel();
        nextQuest = new Quest(currentRoundedPowerLevel, gameFunctions.getSta8era(), gameFunctions.getBash(), gameFunctions.getEk8eths(), gameFunctions.getDiaireths());
        /*string questDifficulty;
        if (currentRoundedPowerLevel > nextQuest.getquestPowerLevelTarget() + 4.5)
        {
            questDifficulty = "Hard";
        } else if (currentRoundedPowerLevel < nextQuest.getquestPowerLevelTarget() - 4.5)
        {
            questDifficulty = "Easy";
        }
        else 
        {
            questDifficulty = "Normal";
        }
        print("Powerlevel target: "+newQuest.getquestPowerLevelTarget() +",Type: "+ newQuest.getquestType() +", Reward type:"+ newQuest.getquestRewardType());
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gameFunctions.getSlotNameFromIndex (nextQuest.getquestRewardType()) + "\nlvl"+ nextQuest.getquestPowerLevelTarget();
        */

        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = nextQuest.getquestPowerLevelTarget()+"";

        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameFunctions.getSlotNameFromIndex(nextQuest.getquestRewardType());

        if (nextQuest.getquestRewardQuality() == 0)
        {
            transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (nextQuest.getquestRewardQuality() == 1)
        {
            transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
        }
        else 
        {
            transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);
        }

        string questSpeed = "Medium";
        if (nextQuest.getquestSpeed() == 0) { questSpeed = "Fast"; } else if (nextQuest.getquestSpeed() == 2) { questSpeed = "Slow"; }
        string quality = "";
        transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = questSpeed;
    }

    public void activateQuest() 
    {
        if (activeQuestsContainer.transform.childCount >= maxActiveQuests) return;
        activeQuestCounter.GetComponent<TextMeshProUGUI>().text = int.Parse(activeQuestCounter.GetComponent<TextMeshProUGUI>().text.Split('/')[0])+1+"/10";
        GameObject newActiveQuest = Instantiate(activeQuestPrefab, activeQuestsContainer.transform).gameObject;
        newActiveQuest.GetComponent<ActiveQuestBehavior>().setQuest(nextQuest);
        soundFX.playQuestAdded();
        for (int i = 3; i < transform.parent.GetChildCount(); i++) transform.parent.GetChild(i).GetComponent<QuestManager>().createQuest();
        //createQuest();
    }
}
