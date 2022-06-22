using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CraftingButton : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] GameObject craftingMaterials;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject craftingPopUpButtons;
    [SerializeField] GameObject powerLevelCalculator;
    [SerializeField] GameObject craftingPopup;
    [SerializeField] GameObject collectionContent;
    [SerializeField] GameObject achievementsContainer;

    PowerLevelCalculator powerLevelCalculatorComponent;
    CharacterEquipment characterEquipment;
    ScrollContent scrollContent;
    GameFunctions gameFunctions;
    FileAccessor fileAccessor;
    SoundFX soundFX;
    AchievementsScript achievementsScript;

    int rarityIndex;

    private void Start()
    {
        powerLevelCalculatorComponent = powerLevelCalculator.GetComponent<PowerLevelCalculator>();
        characterEquipment = character.GetComponent<CharacterEquipment>();
        scrollContent = collectionContent.GetComponent<ScrollContent>();
        gameFunctions = gameManager.GetComponent<GameFunctions>();
        fileAccessor = character.GetComponent<FileAccessor>();
        soundFX = gameManager.GetComponent<SoundFX>();
        achievementsScript = achievementsContainer.GetComponent<AchievementsScript>();
        //character = GameObject.Find("Character");
        //craftingMaterials = GameObject.Find("CraftingMaterials");
        //gameManager = GameObject.Find("GameManager");
    }

    public void onCraftingButtonClick()
    {
        if (int.Parse(craftingMaterials.transform.GetChild(transform.GetSiblingIndex()).GetChild(0).GetComponent<TextMeshProUGUI>().text) < 10) return;
        gameFunctions.showCraftingPopUp();
        soundFX.playMenuButtonDown();
        //GameObject craftingPopUpButtons = GameObject.Find("CraftingPopUpButtons");
        for (int i = 0; i < craftingPopUpButtons.transform.childCount; i++)
        {
            craftingPopUpButtons.transform.GetChild(i).GetComponent<CraftingButton>().setRarityIndex(transform.GetSiblingIndex());
        }
    }

    public void onCraftingTypeButtonClick()
    {

        PlayerPrefs.SetInt("Items Crafted", PlayerPrefs.GetInt("Items Crafted", 0) + 1);
        PlayerPrefs.SetInt("Total items crafted", PlayerPrefs.GetInt("Total items crafted", 0) + 1);
        achievementsScript.onCraftingAchievementProgress();

        rollItem();

        fileAccessor.saveCraftingItems(rarityIndex, -10);//teleutaia h afairesh materials se periptwsh bug kai crash sto roll item na mhn timwrh8oun oi paiktes xanontas ta
        //print("Items Crafted: "+PlayerPrefs.GetInt("Items Crafted"));
    }

    public void setRarityIndex(int index)
    {
        rarityIndex = index;
    }

    void rollItem()
    {
        int currentPowerLevel = powerLevelCalculatorComponent.getRoundedPowerLevel(); //GameObject.Find("PowerLevelCalculator").gameObject.GetComponent<PowerLevelCalculator>().getRoundedPowerLevel();
        int rewardType = transform.GetSiblingIndex() + 1;
        int rewardLevel;
        int minLevel;
        int nextRarityMinLevel = 0;
        int oneStarQualityMaxLevel = gameFunctions.getOneStarQualityMaxLevel();
        if (rarityIndex < 14)
        {
            nextRarityMinLevel = gameFunctions.getMinLevelByRarity(gameFunctions.getRarityByIndex(rarityIndex + 2));
            //print(nextRarityMinLevel);
        }

        if (rarityIndex < 15)
        {
            minLevel = gameFunctions.getMinLevelByRarity(gameFunctions.getRarityByIndex(rarityIndex + 1));
        }
        else
        {
            minLevel = gameFunctions.getMinLevelByRarity(gameFunctions.getRarityByIndex(rarityIndex));
        }
        //print(minLevel);

        /*
        if (currentPowerLevel < minLevel)
        {
            rewardLevel = minLevel;
        }
        else 
        {*/

        // to crafting dinei antikeimeno mesa sta oria twn epipedwn tou rarity an den einai unique
        if (rarityIndex < 14)
        {
            //to epipedo den mporei na einai mikrotero tou currentlyEquippedItemLevel kai tou currentPowerLevel efoson afta einai megalytera tou minlevel opote pairnoume san bash to mikrotero apo ta dyo + 1
            int bash = minLevel;
            if (currentPowerLevel > minLevel && currentPowerLevel <= characterEquipment.getEquippedItemLevel(rewardType)) { bash = currentPowerLevel + 1; }
            if (characterEquipment.getEquippedItemLevel(rewardType) > minLevel && characterEquipment.getEquippedItemLevel(rewardType) <= currentPowerLevel) { bash = characterEquipment.getEquippedItemLevel(rewardType) + 1; }

            if (bash > nextRarityMinLevel - 1) { bash = nextRarityMinLevel - 1; }

            rewardLevel = Random.Range(bash, nextRarityMinLevel);//mexri maxLevel - 1
            //rewardLevel = Mathf.Clamp(rewardLevel, minLevel, maxLevel - 1);
        }
        else if (rarityIndex == 14) //an einai apo godlike na ftiaxtei unique
        {
            int bash = minLevel;
            if (currentPowerLevel > minLevel && currentPowerLevel <= characterEquipment.getEquippedItemLevel(rewardType)) { bash = currentPowerLevel + 1; }
            if (characterEquipment.getEquippedItemLevel(rewardType) > minLevel && characterEquipment.getEquippedItemLevel(rewardType) <= currentPowerLevel) { bash = characterEquipment.getEquippedItemLevel(rewardType) + 1; }

            rewardLevel = Random.Range(bash, Mathf.Max(currentPowerLevel, characterEquipment.getEquippedItemLevel(rewardType), minLevel) + 2 * oneStarQualityMaxLevel + 1);
        }
        else // an einai unique tote ginetai roll me bash 1 + th mesh tou currentlyEquippedItemLevel kai tou powerLevel ki ews kai 20(2*oneStarQualityMaxLevel) epipeda parapanw pou einai kai to roll gia 3 asteria quality
        {
            rewardLevel = (int)(Mathf.Max(0.5f * (currentPowerLevel + characterEquipment.getEquippedItemLevel(rewardType)), minLevel) + 1 + Random.Range(0, 2 * oneStarQualityMaxLevel));//einai exclusive to 20(2*oneStarQualityMaxLevel) alla pros8etoume 1 etsi ki alliws opote gi auto einai k to 0 anti gia 1

        }
        //}
        //print("Type: " + rewardType + " lvl" + rewardLevel);
        EquipmentItem newItem = new EquipmentItem();

        //var info = new DirectoryInfo(Application.dataPath + "/Resources/" + gameFunctions.getSlotNameFromIndex(rewardType));
        //var files = info.GetFiles("*.png");

        string spritePath = "";



        List<string> itemStrings = gameFunctions.getItemsForResourceLoad(rewardType);

        //crafting always gives a not seen before item
        int missingItemIndex = gameFunctions.getMissingItemIndex(transform.GetSiblingIndex());
        //print("Missing item index: "+missingItemIndex);
        if (missingItemIndex != -1)
        {
            //spritePath = files[missingItemIndex].ToString().Replace("\\", "/");

            spritePath = (itemStrings[missingItemIndex]).ToString();
            //print("Sprite: "+spritePath);
        }
        else
        {
            //spritePath = files[Random.Range(0, files.Length)].ToString().Replace("\\", "/");

            spritePath = (itemStrings[Random.Range(0, itemStrings.Count)]).ToString();
        }

        /*//crafting used to give unique item only when crafting unique items And being max level
        if (rarityIndex == 15 && currentPowerLevel>=1000)
        {
            int missingItemIndex = gameFunctions.getMissingItemIndex(transform.GetSiblingIndex());
            print("Missing item index: "+missingItemIndex);
            if (missingItemIndex!=-1)
            {
                spritePath = files[missingItemIndex].ToString().Replace("\\", "/");
                print("Sprite: "+spritePath);
            }
            else 
            {
                spritePath = files[Random.Range(0, files.Length)].ToString().Replace("\\", "/");
            }
        }
        else 
        {
            spritePath = files[Random.Range(0, files.Length)].ToString().Replace("\\", "/");
        }
        */

        //Sprite questRewardSprite = Resources.Load<Sprite>(spritePath.Replace(Application.dataPath + "/Resources/", "").Split('.')[0]);
        Sprite questRewardSprite = Resources.Load<Sprite>(spritePath);

        newItem.setItemLevel(rewardLevel);
        //newItem.setItemImagePath(spritePath.Replace(Application.dataPath + "/Resources/", "").Split('.')[0]);
        newItem.setItemImagePath(spritePath);
        newItem.setItemImage(questRewardSprite);
        //GameObject.Find("Character").GetComponent<CharacterEquipment>().equipNewItem(newItem, rewardType);
        characterEquipment.equipNewItem(newItem, rewardType);
        soundFX.playGainedItem();
        string rarity = gameFunctions.getRarityByLevel(rewardLevel);
        string description = "lvl " + rewardLevel + " " + rarity + " " + gameFunctions.getSlotNameFromIndex(rewardType);
        gameFunctions.showPopUp(description, questRewardSprite, gameFunctions.getColorByRarity(rewardLevel));
        //GameObject.Find("CollectionContent").GetComponent<ScrollContent>().highlightNewlyObtainedItem(rewardType, questRewardSprite);
        //GameObject.Find("CraftingPopUp").gameObject.SetActive(false);
        scrollContent.highlightNewlyObtainedItem(rewardType, questRewardSprite);
        craftingPopup.gameObject.SetActive(false);
    }
}
