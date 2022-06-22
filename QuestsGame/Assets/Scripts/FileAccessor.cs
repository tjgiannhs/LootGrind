using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using System.Runtime.Serialization.Formatters.Binary;

public class FileAccessor : MonoBehaviour
{
    //[SerializeField] TextAsset saveFile;
    [SerializeField] GameObject character;
    [SerializeField] GameObject craftingMaterials;
    [SerializeField] GameObject powerLevelCalculator;
    [SerializeField] GameObject speedrunTimers;
    [SerializeField] GameObject achievemnetsContainer;

    CharacterEquipment characterEquipment;
    Crafting craftingComponent;
    PowerLevelCalculator powerLevelCalculatorComponent;
    Speedrun speedrunTimersScript;
    AchievementsScript achievementsScript;

    private void Start()
    {
        characterEquipment = character.GetComponent<CharacterEquipment>();
        craftingComponent = craftingMaterials.GetComponent<Crafting>();
        powerLevelCalculatorComponent = powerLevelCalculator.GetComponent<PowerLevelCalculator>();
        speedrunTimersScript = speedrunTimers.GetComponent<Speedrun>();
        achievementsScript = achievemnetsContainer.GetComponent<AchievementsScript>();
        print(Application.persistentDataPath);
    }
    public string getUnlockedItems() 
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string unlockedItemsPath = Application.persistentDataPath + "/UnlockedItems.txt";
        string allLines;

        if (!File.Exists(unlockedItemsPath))
        {
            allLines = "Weapon/0\nChest/0\nBracers/0\nPants/0\nBoots/0";
            //File.WriteAllText(unlockedItemsPath, allLines);
            FileStream stream = new FileStream(unlockedItemsPath, FileMode.Create);
            formatter.Serialize(stream, allLines);
            stream.Close();
        }
        else
        {
            //allLines = File.ReadAllText(unlockedItemsPath);
            FileStream stream = new FileStream(unlockedItemsPath, FileMode.Open);
            //var textFile = Resources.Load<TextAsset>("UnlockedItems");
            allLines = formatter.Deserialize(stream) as string;
            stream.Close();
        }

        setUnlockedItemsPlayerPrefs(allLines, false);

        return allLines;
    }

    //to bool einai gia otan ekteleitai sthn arxh tou paixnidiou den stelnw sta analytics oti oloklhrw8hke h syllogh enos typou antikeimenwn
    void setUnlockedItemsPlayerPrefs(string oneline, bool runningBecauseNewItem) 
    {
        int[] unlockedImagesCounters = new int[13];

        for (int i = 0; i < 13; i++) 
        {
            unlockedImagesCounters[i] = 0;
        }

        string[] splitArray = oneline.Split(char.Parse("\n"));
        int counterOfAllCollectedItems=0;
        foreach (string s in splitArray) 
        {
            string itemType = s.Split(char.Parse("/"))[0];
            switch (itemType) 
            {
                case "Weapon":
                    unlockedImagesCounters[0]++;
                    break;
                case "Helmet":
                    unlockedImagesCounters[1]++;
                    break;
                case "Necklace":
                    unlockedImagesCounters[2]++;
                    break;
                case "Cloak":
                    unlockedImagesCounters[3]++;
                    break;
                case "Shoulders":
                    unlockedImagesCounters[4]++;
                    break;
                case "Chest":
                    unlockedImagesCounters[5]++;
                    break;
                case "Bracers":
                    unlockedImagesCounters[6]++;
                    break;
                case "Gloves":
                    unlockedImagesCounters[7]++;
                    break;
                case "Ring":
                    unlockedImagesCounters[8]++;
                    break;
                case "Belt":
                    unlockedImagesCounters[9]++;
                    break;
                case "Pants":
                    unlockedImagesCounters[10]++;
                    break;
                case "Boots":
                    unlockedImagesCounters[11]++;
                    break;
                default://Trinket
                    unlockedImagesCounters[12]++;
                    break;
            }
            counterOfAllCollectedItems++;
        }

        //elegxos prwta an allakse o ari8mos twn ksekleidwmenwn eikonwn gia ka8e typo antikeimenwn
        //nea timh ki elegxos an exoume neo antikeimeno h ekteleitai sthn arxh o kwdikas, an nai tote an oles oi eikones ksekleidwtes tote stelnw analytics
        if (PlayerPrefs.GetInt("Weapon Images", 0) != unlockedImagesCounters[0]) 
        {
            PlayerPrefs.SetInt("Weapon Images", unlockedImagesCounters[0]);
            print("Weapons collected: " + PlayerPrefs.GetInt("Weapon Images", unlockedImagesCounters[0]) + "/95");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[0] == 95)
                {
                    sendAnalyticsOnCollectionFilled("Weapon");
                }
                PlayerPrefs.SetInt("Collected weapons", unlockedImagesCounters[0]);
                achievementsScript.onCollectionsAchievementProgress();
            }
        }
        if (PlayerPrefs.GetInt("Helmet Images", 0) != unlockedImagesCounters[1]) 
        {
            PlayerPrefs.SetInt("Helmet Images", unlockedImagesCounters[1]);
            print("Helmet collected: " + PlayerPrefs.GetInt("Helmet Images", unlockedImagesCounters[1]) + "/25");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[1] == 25) sendAnalyticsOnCollectionFilled("Helmet");
            }
        }
        if (PlayerPrefs.GetInt("Necklace Images", 0) != unlockedImagesCounters[2]) 
        {
            PlayerPrefs.SetInt("Necklace Images", unlockedImagesCounters[2]);
            print("Necklace collected: " + PlayerPrefs.GetInt("Necklace Images", unlockedImagesCounters[2]) + "/30");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[2] == 30) sendAnalyticsOnCollectionFilled("Necklace");
            }
        }
        if (PlayerPrefs.GetInt("Cloak Images", 0) != unlockedImagesCounters[3]) 
        {
            PlayerPrefs.SetInt("Cloak Images", unlockedImagesCounters[3]);
            print("Cloak collected: " + PlayerPrefs.GetInt("Cloak Images", unlockedImagesCounters[3]) + "/20");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[3] == 20) sendAnalyticsOnCollectionFilled("Cloak");
            }
        }
        if (PlayerPrefs.GetInt("Shoulders Images", 0) != unlockedImagesCounters[4]) 
        {
            PlayerPrefs.SetInt("Shoulders Images", unlockedImagesCounters[4]);
            print("Shoulders collected: " + PlayerPrefs.GetInt("Shoulders Images", unlockedImagesCounters[4]) + "/20");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[4] == 20) sendAnalyticsOnCollectionFilled("Shoulders");
            }
        }
        if (PlayerPrefs.GetInt("Chest Images", 0) != unlockedImagesCounters[5]) 
        {
            PlayerPrefs.SetInt("Chest Images", unlockedImagesCounters[5]);
            print("Chest collected: " + PlayerPrefs.GetInt("Chest Images", unlockedImagesCounters[5]) + "/20");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[5] == 20) sendAnalyticsOnCollectionFilled("Chest");
            }
        }
        if (PlayerPrefs.GetInt("Bracers Images", 0) != unlockedImagesCounters[6]) 
        {
            PlayerPrefs.SetInt("Bracers Images", unlockedImagesCounters[6]);
            print("Bracers collected: " + PlayerPrefs.GetInt("Bracers Images", unlockedImagesCounters[6]) + "/20");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[6] == 20) sendAnalyticsOnCollectionFilled("Bracers");
            }
        }
        if (PlayerPrefs.GetInt("Gloves Images", 0) != unlockedImagesCounters[7]) 
        {
            PlayerPrefs.SetInt("Gloves Images", unlockedImagesCounters[7]);
            print("Gloves collected: " + PlayerPrefs.GetInt("Gloves Images", unlockedImagesCounters[7]) + "/20");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[7] == 20) sendAnalyticsOnCollectionFilled("Gloves");
            }
        }
        if (PlayerPrefs.GetInt("Ring Images", 0) != unlockedImagesCounters[8]) 
        {
            PlayerPrefs.SetInt("Ring Images", unlockedImagesCounters[8]);
            print("Ring collected: " + PlayerPrefs.GetInt("Ring Images", unlockedImagesCounters[8]) + "/35");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[8] == 35) sendAnalyticsOnCollectionFilled("Ring");
            }
        }
        if (PlayerPrefs.GetInt("Belt Images", 0) != unlockedImagesCounters[9]) 
        {
            PlayerPrefs.SetInt("Belt Images", unlockedImagesCounters[9]);
            print("Belt collected: " + PlayerPrefs.GetInt("Belt Images", unlockedImagesCounters[9]) + "/20");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[9] == 20) sendAnalyticsOnCollectionFilled("Belt");
            }
        }
        if (PlayerPrefs.GetInt("Pants Images", 0) != unlockedImagesCounters[10]) 
        {
            PlayerPrefs.SetInt("Pants Images", unlockedImagesCounters[10]);
            print("Pants collected: " + PlayerPrefs.GetInt("Pants Images", unlockedImagesCounters[10]) + "/20");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[10] == 20) sendAnalyticsOnCollectionFilled("Pants");
            }
        }
        if (PlayerPrefs.GetInt("Boots Images", 0) != unlockedImagesCounters[11]) 
        {
            PlayerPrefs.SetInt("Boots Images", unlockedImagesCounters[11]);
            print("Boots collected: " + PlayerPrefs.GetInt("Boots Images", unlockedImagesCounters[11]) + "/20");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[11] == 20) sendAnalyticsOnCollectionFilled("Boots");
            }
        }
        if (PlayerPrefs.GetInt("Trinket Images", 0) != unlockedImagesCounters[12]) 
        {
            PlayerPrefs.SetInt("Trinket Images", unlockedImagesCounters[12]);
            print("Trinkets collected: " + PlayerPrefs.GetInt("Trinket Images", unlockedImagesCounters[12]) + "/25");
            if (runningBecauseNewItem){
                if (unlockedImagesCounters[12] == 25) sendAnalyticsOnCollectionFilled("Trinket");
            }
        }

        if (runningBecauseNewItem) 
        {
            //if the number of items collected is the same as the number of all items in the game then it sends analytics
            if (counterOfAllCollectedItems == 370)
            {
                speedrunTimersScript.stopItemsTimer();
                //auksanei to total collections completed sta 14 me thn oloklhrwsh olwn twn item collections, opote de xreiazetai logika alla den empodizei kati opote...
                sendAnalyticsOnCollectionFilled("All Collections");
            }
        }
    }

    void sendAnalyticsOnCollectionFilled(string collectionName) 
    {
        PlayerPrefs.SetInt("Total collections completed",PlayerPrefs.GetInt("Total collections completed",0)+1);

        achievementsScript.onCollectionsAchievementProgress();

        Analytics.CustomEvent("Collection filled", new Dictionary<string, object> {
            { "Collection", collectionName},
            { "Level", powerLevelCalculatorComponent.getRoundedPowerLevel() },
            { "Quests", PlayerPrefs.GetInt("Quests completed") },
            { "OS and Version", SystemInfo.operatingSystem },
            { "Playthrough time", PlayerPrefs.GetInt("Playthrough time", 0) + (int)Time.timeSinceLevelLoad / 60 }
        });
    }

    public string getCraftingItems() 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string craftingItemsPath = Application.persistentDataPath + "/CraftingItems.txt";
        string allLines = "";

        if (!File.Exists(craftingItemsPath))
        {
            allLines = "0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0\n0";
            //File.WriteAllText(craftingItemsPath, allLines);
            FileStream stream = new FileStream(craftingItemsPath, FileMode.Create);
            formatter.Serialize(stream, allLines);
            stream.Close();
        }
        else
        {
            //allLines = File.ReadAllText(craftingItemsPath);
            //var textFile = Resources.Load<TextAsset>("CraftingItems");
            FileStream stream = new FileStream(craftingItemsPath, FileMode.Open);
            allLines = formatter.Deserialize(stream) as string;
            stream.Close();
        }

        return allLines;
    }
    public void saveCraftingItems(int index, int additionValue)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string craftingItemsPath = Application.persistentDataPath + "/CraftingItems.txt";
        string[] allLines; 
        FileStream stream = new FileStream(craftingItemsPath, FileMode.Open);
        string allText = formatter.Deserialize(stream) as string;
        stream.Close();
        allLines = allText.Split('\n');
        string newLines="";
        //print(allLines.Length);
        for (int i = 0; i < 16; i++) 
        {
            //print(i);
            if (i == index)
            {
                newLines += (int.Parse(allLines[i])+additionValue)+"\n";
            }
            else 
            {
                newLines += allLines[i]+"\n";
            }
        }
        //File.WriteAllText(craftingItemsPath, newLines);
        stream = new FileStream(craftingItemsPath, FileMode.Create);
        formatter.Serialize(stream, newLines);
        stream.Close();
        craftingComponent.showCraftingMaterials();
    }
    public void saveNewUnlockedItems(string newUnlockedItem)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string unlockedItemsPath = Application.persistentDataPath + "/UnlockedItems.txt";
        FileStream stream = new FileStream(unlockedItemsPath, FileMode.Open);
        string allLines = formatter.Deserialize(stream) as string + "\n" + newUnlockedItem;
        stream.Close();
        setUnlockedItemsPlayerPrefs(allLines,true);
        //File.WriteAllText(unlockedItemsPath, allLines);
        stream = new FileStream(unlockedItemsPath, FileMode.Create);
        formatter.Serialize(stream, allLines);
        stream.Close();
    }

    public EquipmentItem[] getSavedEquipment()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //Path of the file
        string path = Application.persistentDataPath + "/Save.txt";
        string backupPath = Application.persistentDataPath + "/BackupSave.txt";
        string tempPath = Application.persistentDataPath + "/TempSave.txt";

        string allLines="";

        if (!File.Exists(path))
        {
            Debug.Log("No Save File Found.");
            if (!File.Exists(backupPath))
            {
                //first time playing
                
                allLines = "3&&Weapon/0\n&&\n&&\n&&\n&&\n&&\n4&&Chest/0\n1&&Bracers/0\n&&\n&&\n&&\n3&&Pants/0\n2&&Boots/0\n&&";
                FileStream stream = new FileStream(path, FileMode.Create);
                formatter.Serialize(stream, allLines);
                stream.Close();
                stream = new FileStream(backupPath, FileMode.Create);
                formatter.Serialize(stream, allLines);
                stream.Close();
                stream = new FileStream(tempPath, FileMode.Create);
                formatter.Serialize(stream, allLines);
                stream.Close();
                //File.WriteAllText(path, allLines);
                //File.WriteAllText(backupPath, allLines);
                //File.WriteAllText(tempPath, allLines);
            }
            else 
            {
                Debug.Log("Using Backup Save File.");
                //allLines = File.ReadAllText(backupPath);
                FileStream stream = new FileStream(backupPath, FileMode.Open);
                allLines = formatter.Deserialize(stream) as string;
                stream.Close();
                stream = new FileStream(path, FileMode.Create);
                formatter.Serialize(stream, allLines);
                stream.Close();
                stream = new FileStream(tempPath, FileMode.Create);
                formatter.Serialize(stream, allLines);
                stream.Close();
                //File.WriteAllText(path, allLines);
                //File.WriteAllText(tempPath, allLines);
            }
        }
        else 
        {
            Debug.Log("Read from Save File.");
            //allLines = File.ReadAllText(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            allLines = formatter.Deserialize(stream) as string;
            stream.Close();
            stream = new FileStream(backupPath, FileMode.Create);
            formatter.Serialize(stream, allLines);
            stream.Close();
            stream = new FileStream(tempPath, FileMode.Create);
            formatter.Serialize(stream, allLines);
            stream.Close();
            //File.WriteAllText(backupPath, allLines);
            //File.WriteAllText(tempPath, allLines);
        }

        EquipmentItem[] savedEquipment = new EquipmentItem[14];

        var lines = allLines.Split("\n"[0]);
        for (int i = 0; i < 14; i++)
        {
            EquipmentItem temp = new EquipmentItem();
            string level = lines[i].Split("&"[0])[0];
            if (level != "") { temp.setItemLevel(Convert.ToInt32(level)); }
            string name = lines[i].Split("&"[0])[1];
            if (name != "") { temp.setItemName(name); }
            string imagePath = lines[i].Split("&"[0])[2];
            if (imagePath != "")
            {
                Sprite itemImage = Resources.Load<Sprite>(imagePath);
                print("ImagePath: " + imagePath);
                temp.setItemImagePath(imagePath);
                temp.setItemImage(itemImage);
            }
            savedEquipment[i] = temp;
        }

        return savedEquipment;
    }
    /*
    public EquipmentItem[] getSavedEquipment()
    {
        string allLines = saveFile.text;

        EquipmentItem[] savedEquipment = new EquipmentItem[13];

        var lines = allLines.Split("\n"[0]);
        for (int i = 0; i < 13; i++)
        {
            EquipmentItem temp = new EquipmentItem();
            string level = lines[i].Split("&"[0])[0];
            if (level != "Empty") { temp.setItemLevel(Convert.ToInt32(level)); }
            string name = lines[i].Split("&"[0])[1];
            if (level != "Empty") { temp.setItemName(name); }

            savedEquipment[i] = temp;
        }

        return savedEquipment;
    }*/

    public void saveEquippedItems() 
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //Path of the file
        string path = Application.persistentDataPath + "/Save.txt";
        string backupPath = Application.persistentDataPath + "/BackupSave.txt";
        string tempPath = Application.persistentDataPath + "/TempSave.txt";
        
        File.Replace(path,backupPath,tempPath);
        File.Delete(path);
        string content = characterEquipment.getEquipmentToSave();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, content);
        stream.Close();
        //File.WriteAllText(path, content);
        //Content of the file
    }


    public void deleteSavefiles() 
    {
        string path = Application.persistentDataPath + "/Save.txt";
        string backupPath = Application.persistentDataPath + "/BackupSave.txt";
        string tempPath = Application.persistentDataPath + "/TempSave.txt";
        string unlockedItemsPath = Application.persistentDataPath + "/UnlockedItems.txt";
        string craftingItemsPath = Application.persistentDataPath + "/CraftingItems.txt";
        File.Delete(path);
        File.Delete(backupPath);
        File.Delete(tempPath);
        File.Delete(unlockedItemsPath);
        File.Delete(craftingItemsPath);
    }

}


//string spritePath = files[Random.Range(0, files.Length)].ToString().Replace("\\", "/");
//Sprite questRewardSprite = Resources.Load<Sprite>(spritePath.Replace(Application.dataPath + "/Resources/", "").Split('.')[0]);

//newItem.setItemLevel(questRewardLevel);
//        newItem.setItemImage(questRewardSprite);



//Assets/Resources/swords/sv_b_08.PNG