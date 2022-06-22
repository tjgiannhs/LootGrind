using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] EquipmentItem[] allEquippedItems;

    [SerializeField] GameObject powerLevelCalculator;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject achievementContainer;
    [SerializeField] GameObject powerLevel;

    PowerLevelCalculator powerLevelCalculatorComponent;
    GameFunctions gameFunctions;
    FileAccessor fileAccessor;
    AchievementsScript achievementsScript;

    private void Awake()
    {
        //powerLevelCalculator = GameObject.Find("PowerLevelCalculator").gameObject;
        //gameManager = GameObject.Find("GameManager").gameObject;
        powerLevelCalculatorComponent = powerLevelCalculator.GetComponent<PowerLevelCalculator>();
        fileAccessor = GetComponent<FileAccessor>();
        allEquippedItems = fileAccessor.getSavedEquipment();
        gameFunctions = gameManager.GetComponent<GameFunctions>();
        achievementsScript = achievementContainer.GetComponent<AchievementsScript>();
        connectToPowerLevelCalculator();
        showEquippedItems();
        showPowerLevel();
    }

    public void connectToPowerLevelCalculator() 
    {
        powerLevelCalculatorComponent.WeaponRItemLevel = allEquippedItems[0].getItemLevel();
        powerLevelCalculatorComponent.WeaponLItemLevel = allEquippedItems[1].getItemLevel();
        powerLevelCalculatorComponent.HelmetItemLevel = allEquippedItems[2].getItemLevel();
        powerLevelCalculatorComponent.NecklaceItemLevel = allEquippedItems[3].getItemLevel();
        powerLevelCalculatorComponent.CloakItemLevel = allEquippedItems[4].getItemLevel();
        powerLevelCalculatorComponent.ShouldersItemLevel = allEquippedItems[5].getItemLevel();
        powerLevelCalculatorComponent.ChestItemLevel = allEquippedItems[6].getItemLevel();
        powerLevelCalculatorComponent.BracersItemLevel = allEquippedItems[7].getItemLevel();
        powerLevelCalculatorComponent.GlovesItemLevel = allEquippedItems[8].getItemLevel();
        powerLevelCalculatorComponent.RingItemLevel = allEquippedItems[9].getItemLevel();
        powerLevelCalculatorComponent.BeltItemLevel = allEquippedItems[10].getItemLevel();
        powerLevelCalculatorComponent.PantsItemLevel = allEquippedItems[11].getItemLevel();
        powerLevelCalculatorComponent.BootsItemLevel = allEquippedItems[12].getItemLevel();
        powerLevelCalculatorComponent.TrinketItemLevel = allEquippedItems[13].getItemLevel();
    }

    public void showEquippedItems() 
    {
        int slotsFilled = 0;
        int uniqueEquipment = 0;
        for(int i=0; i<14; i++)
        {
            if (allEquippedItems[i].getItemImage() != null)
            {
                Sprite jasd = allEquippedItems[i].getItemImage();
                transform.GetChild(i).transform.GetChild(1).GetComponent<Image>().sprite = allEquippedItems[i].getItemImage();
                slotsFilled++;
            }
            transform.GetChild(i).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gameFunctions.getItemDescriptionText(i, allEquippedItems[i].getItemLevel());
            transform.GetChild(i).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = allEquippedItems[i].getItemLevel()+"lvl";
            transform.GetChild(i).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[i].getItemLevel());
            //print("lorCo: "+ gameFunctions.getColorByRarity(allEquippedItems[i].getItemLevel()));
            transform.GetChild(i).transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = transform.GetChild(i).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color;
            transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = transform.GetChild(i).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color;
            transform.GetChild(i).transform.GetChild(2).GetComponent<Image>().color = transform.GetChild(i).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color;

            if (gameManager.GetComponent<GameFunctions>().getRarityByLevel(allEquippedItems[i].getItemLevel()) == "Unique") 
            {
                uniqueEquipment++;
            }
        }

        PlayerPrefs.SetInt("Max Slots Filled", Math.Max(PlayerPrefs.GetInt("Max Slots Filled",5),slotsFilled));
        PlayerPrefs.SetInt("Most unique equipped", Math.Max(PlayerPrefs.GetInt("Most unique equipped", 0), uniqueEquipment));
        //print(PlayerPrefs.GetInt("Slots Filled"));
        //print("Unique: " + PlayerPrefs.GetInt("Unique Equipment"));

    }
    public void showPowerLevel()
    {
        if (powerLevelCalculatorComponent.getRoundedPowerLevel() >= 1000 && PlayerPrefs.GetInt("Level Cap", 1) > 0.5f)//an level cap einai 1 tote o paikths paizei me level cap 
        {
            powerLevel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "1000";
            powerLevel.transform.GetChild(3).GetComponent<Image>().fillAmount = 1;
            powerLevel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "MAX";
            return;
        }

        powerLevel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = powerLevelCalculatorComponent.getRoundedPowerLevel()+""; 
        powerLevel.transform.GetChild(3).GetComponent<Image>().fillAmount = powerLevelCalculatorComponent.getPowerLevel() - powerLevelCalculatorComponent.getRoundedPowerLevel(); 
        powerLevel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = powerLevelCalculatorComponent.getRoundedPowerLevel()+1+""; 
    }

    public void equipNewItem(EquipmentItem newItem, int newItemType) 
    {
        if (newItemType >= 2)
        {
            if (allEquippedItems[newItemType].getItemLevel() < newItem.getItemLevel())
            {
                if (allEquippedItems[newItemType].getItemLevel() != 0)
                {
                    fileAccessor.saveCraftingItems(gameFunctions.getIndexByRarity(gameFunctions.getRarityByLevel(allEquippedItems[newItemType].getItemLevel())), 1);
                }
                allEquippedItems[newItemType] = newItem;
                transform.GetChild(newItemType).transform.GetChild(1).GetComponent<Image>().sprite = allEquippedItems[newItemType].getItemImage();
                transform.GetChild(newItemType).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gameFunctions.getItemDescriptionText(newItemType, allEquippedItems[newItemType].getItemLevel());
                transform.GetChild(newItemType).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = allEquippedItems[newItemType].getItemLevel() + "lvl";
                transform.GetChild(newItemType).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[newItemType].getItemLevel());
                transform.GetChild(newItemType).transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[newItemType].getItemLevel());
                transform.GetChild(newItemType).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[newItemType].getItemLevel());
                transform.GetChild(newItemType).transform.GetChild(2).GetComponent<Image>().color = gameFunctions.getColorByRarity(allEquippedItems[newItemType].getItemLevel());
            }
            else 
            {
                fileAccessor.saveCraftingItems(gameFunctions.getIndexByRarity(gameFunctions.getRarityByLevel(newItem.getItemLevel())), 1);
            }

        }
        else
        {
            if (allEquippedItems[0].getItemLevel() < newItem.getItemLevel())
            {
                if (allEquippedItems[1].getItemLevel() != 0)
                {
                    fileAccessor.saveCraftingItems(gameFunctions.getIndexByRarity(gameFunctions.getRarityByLevel(allEquippedItems[1].getItemLevel())), 1);
                }

                allEquippedItems[1] = allEquippedItems[0];
                allEquippedItems[0] = newItem;
                if (allEquippedItems[1].getItemImage() != null) transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = allEquippedItems[1].getItemImage();
                transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gameFunctions.getItemDescriptionText(1, allEquippedItems[1].getItemLevel());
                transform.GetChild(1).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = allEquippedItems[1].getItemLevel() + "lvl";
                transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[1].getItemLevel());
                transform.GetChild(1).transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[1].getItemLevel());
                transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[1].getItemLevel());
                transform.GetChild(1).transform.GetChild(2).GetComponent<Image>().color = gameFunctions.getColorByRarity(allEquippedItems[1].getItemLevel());
                if (allEquippedItems[0].getItemImage() != null) transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = allEquippedItems[0].getItemImage();
                transform.GetChild(0).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gameFunctions.getItemDescriptionText(0, allEquippedItems[0].getItemLevel());
                transform.GetChild(0).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = allEquippedItems[0].getItemLevel() + "lvl";
                transform.GetChild(0).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[0].getItemLevel());
                transform.GetChild(0).transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[0].getItemLevel());
                transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[0].getItemLevel());
                transform.GetChild(0).transform.GetChild(2).GetComponent<Image>().color = gameFunctions.getColorByRarity(allEquippedItems[0].getItemLevel());

            }
            else if (allEquippedItems[1].getItemLevel() < newItem.getItemLevel())
            {
                if (allEquippedItems[1].getItemLevel() != 0)
                {
                    fileAccessor.saveCraftingItems(gameFunctions.getIndexByRarity(gameFunctions.getRarityByLevel(allEquippedItems[1].getItemLevel())), 1);
                }

                allEquippedItems[1] = newItem;
                transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = allEquippedItems[1].getItemImage();
                transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gameFunctions.getItemDescriptionText(1, allEquippedItems[1].getItemLevel());
                transform.GetChild(1).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = allEquippedItems[1].getItemLevel() + "lvl";
                transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[1].getItemLevel());
                transform.GetChild(1).transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[1].getItemLevel());
                transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByRarity(allEquippedItems[1].getItemLevel());
                transform.GetChild(1).transform.GetChild(2).GetComponent<Image>().color = gameFunctions.getColorByRarity(allEquippedItems[1].getItemLevel());
            }
            else 
            {
                fileAccessor.saveCraftingItems(gameFunctions.getIndexByRarity(gameFunctions.getRarityByLevel(newItem.getItemLevel())), 1);
            }
        }
        countFilledSlots();
        connectToPowerLevelCalculator();
        fileAccessor.saveEquippedItems();
    }

    void countFilledSlots() 
    {
        int slotsFilled = 0;
        int uniqueEquipment = 0;

        for (int i = 0; i < 14; i++)
        {
            if (allEquippedItems[i].getItemImage() != null)
            {
                slotsFilled++;
                if (gameFunctions.getRarityByLevel(allEquippedItems[i].getItemLevel()) == "Unique")
                {
                    uniqueEquipment++;
                }
            }
        }

        PlayerPrefs.SetInt("Max Slots Filled", Math.Max(PlayerPrefs.GetInt("Max Slots Filled", 5), slotsFilled));
        PlayerPrefs.SetInt("Most unique equipped", Math.Max(PlayerPrefs.GetInt("Most unique equipped", 0), uniqueEquipment));

        achievementsScript.onEquipmentAchievementProgress();

        //print(PlayerPrefs.GetInt("Slots Filled"));
        //print("Unique: "+PlayerPrefs.GetInt("Unique Equipment"));
    }

    public int getEquippedItemLevel(int index) 
    {
        return allEquippedItems[index].getItemLevel();
    }

    public string getEquipmentToSave() 
    {
        string data = "";
        foreach (EquipmentItem item in allEquippedItems) 
        {
            data += item.getItemLevel() + "&" + item.getItemName() + "&" + item.getItemImagePath() + "\n";
        }
        return data;
    }

}
