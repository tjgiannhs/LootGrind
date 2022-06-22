using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class QuestButton : MonoBehaviour
{
    GameObject gameManager;
    GameObject character;
    GameObject activeQuestCounter;
    GameObject collectionContent;
    GameObject powerLevelCalculator;
    GameObject achievementContainer;
    EquipmentItem newItem;
    int questRewardType;
    int questRewardLevel;
    int questTargetLevel;
    int questRewardQuality;
    string description;
    Sprite questRewardSprite;
    GameFunctions gameFunctions;
    CharacterEquipment characterEquipment;
    AchievementsScript achievementsScript;


    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        character = GameObject.Find("Character");
        activeQuestCounter = GameObject.Find("ActiveQuestCounter");
        collectionContent = GameObject.Find("CollectionContent");
        powerLevelCalculator = GameObject.Find("PowerLevelCalculator");
        achievementContainer = GameObject.FindGameObjectWithTag("Achievements");

        gameFunctions = gameManager.GetComponent<GameFunctions>();
        characterEquipment = character.GetComponent<CharacterEquipment>();
        achievementsScript = achievementContainer.GetComponent<AchievementsScript>();
    }

    public void rollReward() 
    {
        //print("rollara dike mou");

        int currentPowerLevel = powerLevelCalculator.GetComponent<PowerLevelCalculator>().getRoundedPowerLevel();
        int oneStarQualityMaxLevel = gameFunctions.getOneStarQualityMaxLevel();
        questRewardType = transform.parent.GetComponent<ActiveQuestBehavior>().getQuestRewardType();
        questRewardQuality = transform.parent.GetComponent<ActiveQuestBehavior>().getQuestRewardQuality();
        questTargetLevel = transform.parent.GetComponent<ActiveQuestBehavior>().getQuestTargetLevel();
        if (questRewardType == 14) { questRewardType = Random.Range(0, 14); }
        //questRewardLevel = Random.Range(Mathf.Max(currentPowerLevel, characterEquipment.getEquippedItemLevel(questRewardType)) + 1, Mathf.Max(currentPowerLevel, characterEquipment.getEquippedItemLevel(questRewardType)) + 15);
        questRewardLevel = Random.Range(questTargetLevel + questRewardQuality * oneStarQualityMaxLevel/2, questTargetLevel + oneStarQualityMaxLevel + questRewardQuality* oneStarQualityMaxLevel/2 + 1);//plus 1 because it is exclusive

        int currentEquippedItemLevel=0;
        if (questRewardType == 0)
        {
            currentEquippedItemLevel = characterEquipment.getEquippedItemLevel(1);
        }
        else
        {
            currentEquippedItemLevel = characterEquipment.getEquippedItemLevel(questRewardType);
        }
        //we make sure reward level is higher than currentEquippedItem's level if its level is in the range of potential reward levels, this obviously already happens when the quest level is higher thatn the equipped item's level
        //if it is higher than the range then we just get a random reward that's guaranteed to become a material instantly, there is no point in changing anything like always getting the highest possible level in that case
        if(questTargetLevel + questRewardQuality * oneStarQualityMaxLevel/2 <= currentEquippedItemLevel && currentEquippedItemLevel < questTargetLevel + questRewardQuality * oneStarQualityMaxLevel/2 + oneStarQualityMaxLevel)
        {
            while (questRewardLevel <= currentEquippedItemLevel)
            {
                questRewardLevel = Random.Range(questTargetLevel, questTargetLevel + oneStarQualityMaxLevel + questRewardQuality * oneStarQualityMaxLevel/2 + 1);//plus 1 because it is exclusive
            }
            //print("Vzoom");
        }
        print(currentEquippedItemLevel + " - " + questRewardLevel + ", "+(questRewardQuality*oneStarQualityMaxLevel/2+ oneStarQualityMaxLevel) +", "+(questTargetLevel + questRewardQuality * oneStarQualityMaxLevel/2) +" . "+ (questTargetLevel + questRewardQuality * oneStarQualityMaxLevel/2 <= currentEquippedItemLevel && currentEquippedItemLevel < questTargetLevel + questRewardQuality * oneStarQualityMaxLevel/2 + oneStarQualityMaxLevel));
        //print("Type: "+questRewardType+" lvl"+ questRewardLevel);
        newItem = new EquipmentItem();

        //Old way that wasn't working on Android
        //var info = new DirectoryInfo(Application.dataPath + "/Resources/" + gameFunctions.getSlotNameFromIndex(questRewardType));
        //var files = info.GetFiles("*.png");
        //string spritePath = files[Random.Range(0, files.Length)].ToString().Replace("\\", "/");
        //questRewardSprite = Resources.Load<Sprite>(spritePath.Replace(Application.dataPath + "/Resources/", "").Split('.')[0]);
        //print("Q "+ spritePath.Replace(Application.dataPath + "/Resources/", "").Split('.')[0].GetType());
        //spritePath = (gameFunctions.getSlotNameFromIndex(questRewardType) + "/" + itemStrings[Random.Range(0, itemStrings.Count)]).ToString();
        //Sprite itemImage = Resources.Load<Sprite>(itemStrings[itemStringIndex]);
        //Debug.Log(itemImage);

        //New way of loading sprites from resources
        List<string> itemStrings = gameFunctions.getItemsForResourceLoad(questRewardType);
        int itemStringIndex = Random.Range(0, itemStrings.Count);
        questRewardSprite = Resources.Load<Sprite>(itemStrings[itemStringIndex]);

        newItem.setItemLevel(questRewardLevel);
        //newItem.setItemImagePath(spritePath.Replace(Application.dataPath + "/Resources/", "").Split('.')[0]);
        newItem.setItemImagePath(itemStrings[itemStringIndex]);
        newItem.setItemImage(questRewardSprite);

        string rarity = gameFunctions.getRarityByLevel(questRewardLevel);
        description = "lvl " + questRewardLevel + " " + rarity + " " + gameFunctions.getSlotNameFromIndex(questRewardType);
    }

    public void completeQuest() 
    {
        rollReward();


        characterEquipment.equipNewItem(newItem,questRewardType);
        gameManager.GetComponent<SoundFX>().playGainedItem();
        gameFunctions.showPopUp(description,questRewardSprite, gameFunctions.getColorByRarity(questRewardLevel));

        collectionContent.GetComponent<ScrollContent>().highlightNewlyObtainedItem(questRewardType,questRewardSprite);

        activeQuestCounter.transform.parent.parent.GetChild(2).GetComponent<RefreshQuestsButton>().addCompletedQuest();
        PlayerPrefs.SetInt("Quests completed", PlayerPrefs.GetInt("Quests completed", 0) + 1);
        PlayerPrefs.SetInt("Total quests completed", PlayerPrefs.GetInt("Total quests completed", 0) + 1);

        achievementsScript.onQuestsAchievementProgress();

        //we change the number of quests just before we delete the object in case there is an error
        activeQuestCounter.GetComponent<TextMeshProUGUI>().text = int.Parse(activeQuestCounter.GetComponent<TextMeshProUGUI>().text.Split('/')[0]) - 1 + "/10";

        //print("Quests completed: " + PlayerPrefs.GetInt("Quests completed"));

        Destroy(transform.parent.gameObject);
    }
}
