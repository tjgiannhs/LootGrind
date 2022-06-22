using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.RemoteConfig;
using System;
using System.IO;

public class GameFunctions : MonoBehaviour
{
    [SerializeField] Color[] rarityColors;
    [SerializeField] GameObject popUpCanvas;
    [SerializeField] GameObject topPopUpCanvas;
    [SerializeField] GameObject collectionContent;
    [SerializeField] GameObject character;
    [SerializeField] GameObject refreshQuests;
    [SerializeField] TextAsset allItemsTextFile;
    CharacterEquipment characterEquipment;
    RefreshQuestsButton refreshQuestsButton;
    Canvas popUpCanvasComponent;
    Canvas topPopUpCanvasComponent;

    public struct userAttributes { };
    public struct appAttributes { };

    float sta8era = 4;
    float bash = 0.05f;
    float ek8eths = 10 / 9.0f;
    float diaireths = 2.0f;
    float minQtime = 1/3.0f;
    float speedFactor = 4/3.0f;
    float questTimePercentLevel1 = 1/3.0f;
    float questTimeMaxLevel = 60;
    int maxQtimeLevel = 60;
    int uniqueLevel = 1000;
    int godlikeLevel = 940;
    int astralLevel = 875;
    int mythicLevel = 805;
    int legendaryLevel = 725;
    int epicLevel = 640;
    int masterLevel = 550;
    int nobleLevel = 460;
    int exoticLevel = 370;
    int specialLevel = 285;
    int rareLevel = 210;
    int uncommonLevel = 145;
    int commonLevel = 90;
    int serviceableLevel = 50;
    int poorLevel = 20;
    int oneStarQualityMaxLevel = 10;

    private void Awake()
    {
        popUpCanvasComponent = popUpCanvas.GetComponent<Canvas>();
        topPopUpCanvasComponent = topPopUpCanvas.GetComponent<Canvas>();
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void ApplyRemoteSettings(ConfigResponse obj)
    {
        characterEquipment = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterEquipment>();
        refreshQuestsButton = GameObject.FindGameObjectWithTag("RefreshQuests").GetComponent<RefreshQuestsButton>();

        switch (obj.requestOrigin) 
        {
            case ConfigOrigin.Default:
                //no setting loaded this session, using default values
                Debug.Log("No remote settings loaded");
                break;
            case ConfigOrigin.Cached:
                //no setting loaded this session, using cached values from previous session
                Debug.Log("No remote settings loaded, using cached values from previous session");
                break;
            case ConfigOrigin.Remote:
                //new settings loaded this session, update values accordingly
                sta8era = ConfigManager.appConfig.GetFloat("sta8era");
                bash = ConfigManager.appConfig.GetFloat("bash");
                ek8eths = ConfigManager.appConfig.GetFloat("ek8eths");
                diaireths = ConfigManager.appConfig.GetFloat("diaireths");

                minQtime = ConfigManager.appConfig.GetFloat("minQtime");
                maxQtimeLevel = ConfigManager.appConfig.GetInt("maxQtimeLevel");

                speedFactor = ConfigManager.appConfig.GetFloat("speedFactor");

                questTimePercentLevel1 = ConfigManager.appConfig.GetFloat("questTimePercentLevel1");
                questTimeMaxLevel = ConfigManager.appConfig.GetFloat("questTimeMaxLevel");

                uniqueLevel = ConfigManager.appConfig.GetInt("Unique level");
                godlikeLevel = ConfigManager.appConfig.GetInt("Godlike level");
                astralLevel = ConfigManager.appConfig.GetInt("Astral level");
                mythicLevel = ConfigManager.appConfig.GetInt("Mythic level");
                legendaryLevel = ConfigManager.appConfig.GetInt("Legendary level");
                epicLevel = ConfigManager.appConfig.GetInt("Epic level");
                masterLevel = ConfigManager.appConfig.GetInt("Master level");
                nobleLevel = ConfigManager.appConfig.GetInt("Noble level");
                exoticLevel = ConfigManager.appConfig.GetInt("Exotic level");
                specialLevel = ConfigManager.appConfig.GetInt("Special level");
                rareLevel = ConfigManager.appConfig.GetInt("Rare level");
                uncommonLevel = ConfigManager.appConfig.GetInt("Uncommon level");
                commonLevel = ConfigManager.appConfig.GetInt("Common level");
                serviceableLevel = ConfigManager.appConfig.GetInt("Serviceable level");
                poorLevel = ConfigManager.appConfig.GetInt("Poor level");

                oneStarQualityMaxLevel = ConfigManager.appConfig.GetInt("oneStarQualityMaxLevel");

                //reload equipment to show correct rarities
                characterEquipment.showEquippedItems();

                //reload quests with correct values for quest levels
                refreshQuestsButton.refreshQuestsFree();

                Debug.Log("Remote settings applied");

                break;
        }
    }

    public float getSta8era() { return sta8era; }
    public float getBash() { return bash; }
    public float getEk8eths() { return ek8eths; }
    public float getDiaireths() { return diaireths; }
    public float getSpeedFactor() { return speedFactor; }
    public float getQuestTimeMaxLevel() { return questTimeMaxLevel; }
    public float getQuestTimePercentLevel1() { return questTimePercentLevel1; }

    public float getMinQtime() { return minQtime; }
    public float getMaxQtimeLevel() { return maxQtimeLevel; }

    public int getOneStarQualityMaxLevel() { return oneStarQualityMaxLevel; }


    private void Start()
    {
        //popUpCanvas = GameObject.Find("PopUp Canvas"); 
        //collectionContent = GameObject.Find("CollectionContent");
    }

    public string getSlotNameFromIndex(int index)
    {

        switch (index)
        {
            case 0:
                return "Weapon";
                break;
            case 1:
                return "Weapon";
                break;
            case 2:
                return "Helmet";
                break;
            case 3:
                return "Necklace";
                break;
            case 4:
                return "Cloak";
                break;
            case 5:
                return "Shoulders";
                break;
            case 6:
                return "Chest";
                break;
            case 7:
                return "Bracers";
                break;
            case 8:
                return "Gloves";
                break;
            case 9:
                return "Ring";
                break;
            case 10:
                return "Belt";
                break;
            case 11:
                return "Pants";
                break;
            case 12:
                return "Boots";
                break;
            case 13:
                return "Trinket";
                break;
            case 14:
                return "Random";
                break;
            default:
                return "Mystery";
        }

    }

    public string getRarityByLevel(int itemLevel) 
    {
        if (itemLevel >= uniqueLevel)
        {
            return "Unique";
        } else if (itemLevel >= godlikeLevel) 
        {
            return "Godlike";
        } else if (itemLevel >= astralLevel)
        {
            return "Astral";
        } else if (itemLevel >= mythicLevel)
        {
            return "Mythic";
        } else if (itemLevel >= legendaryLevel)
        {
            return "Legendary";
        } else if (itemLevel >= epicLevel)
        {
            return "Epic";
        } else if (itemLevel >= masterLevel)
        {
            return "Master";
        } else if (itemLevel >= nobleLevel)
        {
            return "Noble";
        } else if (itemLevel >= exoticLevel)
        {
            return "Exotic";
        } else if (itemLevel >= specialLevel)
        {
            return "Special";
        } else if (itemLevel >= rareLevel)
        {
            return "Rare";
        } else if (itemLevel >= uncommonLevel)
        {
            return "Uncommon";
        } else if (itemLevel >= commonLevel)
        {
            return "Common";
        } else if (itemLevel >= serviceableLevel)
        {
            return "Serviceable";
        } else if (itemLevel >= poorLevel)
        {
            return "Poor";
        } else
        {
            return "Junk";
        }
    }

    public string getItemDescriptionText(int index, int itemLevel) 
    {
        if (itemLevel == 0) return "";
        //string description = itemLevel + "lvl " + getRarityByLevel(itemLevel) + " " + getSlotNameFromIndex(index);
        string description = getRarityByLevel(itemLevel);
        return description;
    }
//    public string getItemDescriptionText(int itemLevel) 
//    {
//        string description = itemLevel + "lvl " + getRarityByLevel(itemLevel);
//        return description;
//    }

    public Color getColorByRarity(int level) 
    {
        string rarity = getRarityByLevel(level);

        if (rarity == "Unique")
        {
            return rarityColors[15];
        }
        else if (rarity == "Godlike")
        {
            return rarityColors[14];
        }
        else if (rarity == "Astral")
        {
            return rarityColors[13];
        }
        else if (rarity == "Mythic")
        {
            return rarityColors[12];
        }
        else if (rarity == "Legendary")
        {
            return rarityColors[11];
        }
        else if (rarity == "Epic")
        {
            return rarityColors[10];
        }
        else if (rarity == "Master")
        {
            return rarityColors[9];
        }
        else if (rarity == "Noble")
        {
            return rarityColors[8];
        }
        else if (rarity == "Exotic")
        {
            return rarityColors[7];
        }
        else if (rarity == "Special")
        {
            return rarityColors[6];
        }
        else if (rarity == "Rare")
        {
            return rarityColors[5];
        }
        else if (rarity == "Uncommon")
        {
            return rarityColors[4];
        }
        else if (rarity == "Common")
        {
            return rarityColors[3];
        }
        else if (rarity == "Serviceable")
        {
            return rarityColors[2];
        }
        else if (rarity == "Poor")
        {
            return rarityColors[1];
        }
        else
        {
            return rarityColors[0];
        }
    }

    public Color getColorByIndex(int index)
    {
        if (index == 15)
        {
            return rarityColors[15];
        }
        else if (index == 14)
        {
            return rarityColors[14];
        }
        else if (index == 13)
        {
            return rarityColors[13];
        }
        else if (index == 12)
        {
            return rarityColors[12];
        }
        else if (index == 11)
        {
            return rarityColors[11];
        }
        else if (index == 10)
        {
            return rarityColors[10];
        }
        else if (index == 9)
        {
            return rarityColors[9];
        }
        else if (index == 8)
        {
            return rarityColors[8];
        }
        else if (index == 7)
        {
            return rarityColors[7];
        }
        else if (index == 6)
        {
            return rarityColors[6];
        }
        else if (index == 5)
        {
            return rarityColors[5];
        }
        else if (index == 4)
        {
            return rarityColors[4];
        }
        else if (index == 3)
        {
            return rarityColors[3];
        }
        else if (index == 2)
        {
            return rarityColors[2];
        }
        else if (index == 1)
        {
            return rarityColors[1];
        }
        else
        {
            return rarityColors[0];
        }
    }
    public string getRarityByIndex(int index)
    {
        if (index == 15)
        {
            return "Unique";
        }
        else if (index == 14)
        {
            return "Godlike";
        }
        else if (index == 13)
        {
            return "Astral";
        }
        else if (index == 12)
        {
            return "Mythic";
        }
        else if (index == 11)
        {
            return "Legendary";
        }
        else if (index == 10)
        {
            return "Epic";
        }
        else if (index == 9)
        {
            return "Master";
        }
        else if (index == 8)
        {
            return "Noble";
        }
        else if (index == 7)
        {
            return "Exotic";
        }
        else if (index == 6)
        {
            return "Special";
        }
        else if (index == 5)
        {
            return "Rare";
        }
        else if (index == 4)
        {
            return "Uncommon";
        }
        else if (index == 3)
        {
            return "Common";
        }
        else if (index == 2)
        {
            return "Serviceable";
        }
        else if (index == 1)
        {
            return "Poor";
        }
        else
        {
            return "Junk";
        }
    }    
    public int getIndexByRarity(string rarity)
    {
        if (rarity == "Unique")
        {
            return 15;
        }
        else if (rarity == "Godlike")
        {
            return 14;
        }
        else if (rarity == "Astral")
        {
            return 13;
        }
        else if (rarity == "Mythic")
        {
            return 12;
        }
        else if (rarity == "Legendary")
        {
            return 11;
        }
        else if (rarity == "Epic")
        {
            return 10;
        }
        else if (rarity == "Master")
        {
            return 9;
        }
        else if (rarity == "Noble")
        {
            return 8;
        }
        else if (rarity == "Exotic")
        {
            return 7;
        }
        else if (rarity == "Special")
        {
            return 6;
        }
        else if (rarity == "Rare")
        {
            return 5;
        }
        else if (rarity == "Uncommon")
        {
            return 4;
        }
        else if (rarity == "Common")
        {
            return 3;
        }
        else if (rarity == "Serviceable")
        {
            return 2;
        }
        else if (rarity == "Poor")
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int getMinLevelByRarity(string rarity) 
    {
        if (rarity == "Unique")
        {
            return uniqueLevel;
        }
        else if (rarity == "Godlike")
        {
            return godlikeLevel;
        }
        else if (rarity == "Astral")
        {
            return astralLevel;
        }
        else if (rarity == "Mythic")
        {
            return mythicLevel;
        }
        else if (rarity == "Legendary")
        {
            return legendaryLevel;
        }
        else if (rarity == "Epic")
        {
            return epicLevel;
        }
        else if (rarity == "Master")
        {
            return masterLevel;
        }
        else if (rarity == "Noble")
        {
            return nobleLevel;
        }
        else if (rarity == "Exotic")
        {
            return exoticLevel;
        }
        else if (rarity == "Special")
        {
            return specialLevel;
        }
        else if (rarity == "Rare")
        {
            return rareLevel;
        }
        else if (rarity == "Uncommon")
        {
            return uncommonLevel;
        }
        else if (rarity == "Common")
        {
            return commonLevel;
        }
        else if (rarity == "Serviceable")
        {
            return serviceableLevel;
        }
        else if (rarity == "Poor")
        {
            return poorLevel;
        }
        else
        {
            return 1;
        }
    }

    public void showPopUp(string description, Sprite image, Color rarityColor) 
    {
        popUpCanvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = description;
        popUpCanvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().color = rarityColor;
        popUpCanvas.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = image;
        popUpCanvas.transform.GetChild(1).GetChild(2).GetComponent<Image>().color = rarityColor;
        popUpCanvas.transform.GetChild(1).gameObject.SetActive(true);
        popUpCanvasComponent.enabled = true;
    }
    public void showCraftingPopUp() 
    {
        popUpCanvas.transform.GetChild(2).gameObject.SetActive(true);
        popUpCanvasComponent.enabled = true;
    }
    public void showExitPopUp() 
    {
        topPopUpCanvas.transform.GetChild(3).gameObject.SetActive(true);
        topPopUpCanvasComponent.enabled = true;
    }
    /*
    public void hidePopUp() 
    {
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
    }
    
    public void hideCraftingPopUp() 
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
    }
    */
    public int getMissingItemIndex(int itemIndex) 
    {
        int itemsNumber = collectionContent.transform.GetChild(itemIndex).GetChildCount();
        List<int> indexesArray = new List<int>();
        for (int i = 0; i < itemsNumber; i++) 
        {
            if (collectionContent.transform.GetChild(itemIndex).GetChild(i).GetComponent<Image>().color != Color.white) 
            {
                indexesArray.Add(i);
            }
        }
        if (indexesArray.Count != 0)
        { 
            return indexesArray[UnityEngine.Random.Range(0, indexesArray.Count)];
        }
        print(itemIndex);
        return -1;
    }

    //Prints a single string with all the item names and their parent folder like Weapon/0dagger_08_b
    void printItemNames() 
    {
        string ola = "";

        for (int i = 0; i < 14; i++) 
        {
            var info = new DirectoryInfo(Application.dataPath + "/Resources/" + getSlotNameFromIndex(i));
            var files = info.GetFiles("*.png");

            for (int j = 0; j < files.Length; j++) 
            {
                ola = ola + '\n' + files[j].ToString().Replace("\\", "/").Replace(Application.dataPath + "/Resources/", "").Split('.')[0];
            }
        }

        print(ola);
    }

    //returns the names of all items of a specific type along with their parent folder like Weapon/0dagger_08_b
    public List<string> getItemsForResourceLoad(int typeIndex)
    {
        List<string> ola = new List<string>();
        string text = allItemsTextFile.text;
        string[] lines = text.Split('\n');
        string type = getSlotNameFromIndex(typeIndex);
        foreach (string s in lines)
        {
            if (s.Contains(type)) 
            {
                ola.Add(s.Remove(s.Length-1));
                //print(s);
            }
        }
        return ola;
    }


}