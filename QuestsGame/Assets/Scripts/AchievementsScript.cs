using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class AchievementsScript : MonoBehaviour
{
    [SerializeField] GameObject speedrunCanvas;
    [SerializeField] GameObject achievementPopUpContainer;
    [SerializeField] GameObject achievementPopUpPrefab;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject powerLevelCalc;

    Canvas speedrunCanvasComponent;
    Canvas achievementPopUpCanvas;
    SoundFX soundFX;
    PowerLevelCalculator powerLevelCalculatorComponent;

    SteamObject steamObject;

    // Start is called before the first frame update
    void Start()
    {
        speedrunCanvasComponent = speedrunCanvas.GetComponent<Canvas>();
        achievementPopUpCanvas = achievementPopUpContainer.transform.parent.GetComponent<Canvas>();
        soundFX = gameManager.GetComponent<SoundFX>();
        powerLevelCalculatorComponent = powerLevelCalc.GetComponent<PowerLevelCalculator>();
        steamObject = GameObject.FindGameObjectWithTag("Steam").GetComponent<SteamObject>();
        loadAchievementsProgress();
    }

    private void loadAchievementsProgress() // Gets called in the beginning so no popups happen, affects all achievements' bars and counter texts
    {
        transform.GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0);
        transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0),1)+"/1";
        
        transform.GetChild(1).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 10.0f;
        transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0),10)+"/10";    
        
        transform.GetChild(2).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 50.0f;
        transform.GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0),50)+"/50";

        transform.GetChild(3).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 100.0f;
        transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0),100)+"/100";
        
        transform.GetChild(4).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 250.0f;
        transform.GetChild(4).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0),250)+"/250"; 
        
        transform.GetChild(5).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 500.0f;
        transform.GetChild(5).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0),500)+"/500"; 
        
        transform.GetChild(6).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 750.0f;
        transform.GetChild(6).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0),750)+"/750";
        
        transform.GetChild(7).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 1000.0f;
        transform.GetChild(7).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0),1000)+"/1000";

        transform.GetChild(8).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) - 1;
        transform.GetChild(8).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 2) - 1 + "/1";

        transform.GetChild(9).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 10.0f;
        transform.GetChild(9).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 10) + "/10";

        transform.GetChild(10).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 50.0f;
        transform.GetChild(10).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 50) + "/50";

        transform.GetChild(11).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 100.0f;
        transform.GetChild(11).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 100) + "/100";

        transform.GetChild(12).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 200.0f;
        transform.GetChild(12).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 200) + "/200";

        transform.GetChild(13).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 400.0f;
        transform.GetChild(13).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 400) + "/400";

        transform.GetChild(14).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 600.0f;
        transform.GetChild(14).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 600) + "/600";

        transform.GetChild(15).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 800.0f;
        transform.GetChild(15).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 800) + "/800";

        transform.GetChild(16).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 1000.0f;
        transform.GetChild(16).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 1000) + "/1000";

        transform.GetChild(17).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0);
        transform.GetChild(17).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 1) + "/1";

        transform.GetChild(18).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 10.0f;
        transform.GetChild(18).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 10) + "/10";

        transform.GetChild(19).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 25.0f;
        transform.GetChild(19).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 25) + "/25";

        transform.GetChild(20).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 50.0f;
        transform.GetChild(20).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 50) + "/50";

        transform.GetChild(21).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 75.0f;
        transform.GetChild(21).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 75) + "/75";

        transform.GetChild(22).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 100.0f;
        transform.GetChild(22).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 100) + "/100";

        transform.GetChild(23).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Max Slots Filled",0) / 14.0f;
        transform.GetChild(23).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Max Slots Filled", 0), 14) + "/14";

        transform.GetChild(24).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Most unique equipped",0);
        transform.GetChild(24).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Most unique equipped", 0), 1) + "/1";

        transform.GetChild(25).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Most unique equipped",0) / 14.0f;
        transform.GetChild(25).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Most unique equipped", 0), 14) + "/14";

        transform.GetChild(26).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total collections completed",0);
        transform.GetChild(26).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total collections completed", 0), 1) + "/1";

        transform.GetChild(27).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Collected weapons",1)/95.0f;
        transform.GetChild(27).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Collected weapons", 1), 95) + "/95";

        transform.GetChild(28).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total collections completed",0) / 13.0f;
        transform.GetChild(28).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total collections completed", 0), 13) + "/13";

        transform.GetChild(29).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Achievements completed",0) / 29.0f;
        transform.GetChild(29).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Achievements completed", 0), 29) + "/29";
    }

    public void onQuestsAchievementProgress() // when playerprefs "Total quests completed" changes update only the affected achievenements as well as the meta achievement
    {
        float prevFillAmount;
        int counter = 0;
        for (int i = 0; i < 8; i++) 
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) counter++;
        }

        prevFillAmount = transform.GetChild(0).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(0).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0);
        transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0), 1) + "/1";
        if (transform.GetChild(0).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Complete a quest"); }
       
        prevFillAmount = transform.GetChild(1).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(1).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 10.0f;
        transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0), 10) + "/10";
        if (transform.GetChild(1).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Complete 10 quests"); }
        
        prevFillAmount = transform.GetChild(2).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(2).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 50.0f;
        transform.GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0), 50) + "/50";
        if (transform.GetChild(2).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Complete 50 quests"); }
        
        prevFillAmount = transform.GetChild(3).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(3).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 100.0f;
        transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0), 100) + "/100";
        if (transform.GetChild(3).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Complete 100 quests"); }
        
        prevFillAmount = transform.GetChild(4).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(4).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 250.0f;
        transform.GetChild(4).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0), 250) + "/250";
        if (transform.GetChild(4).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Complete 250 quests"); }
        
        prevFillAmount = transform.GetChild(5).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(5).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 500.0f;
        transform.GetChild(5).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0), 500) + "/500";
        if (transform.GetChild(5).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Complete 500 quests"); }
        
        prevFillAmount = transform.GetChild(6).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(6).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 750.0f;
        transform.GetChild(6).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0), 750) + "/750";
        if (transform.GetChild(6).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Complete 750 quests"); }
        
        prevFillAmount = transform.GetChild(7).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(7).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total quests completed",0) / 1000.0f;
        transform.GetChild(7).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total quests completed", 0), 1000) + "/1000";
        if (transform.GetChild(7).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Complete 1000 quests"); }

        steamObject.updateTotalQuestsCompleted(PlayerPrefs.GetInt("Total quests completed", 0));

        int newCounter = 0;
        for (int i = 0; i < 8; i++)
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) newCounter++;
        }

        if (newCounter != counter) { PlayerPrefs.SetInt("Achievements completed", PlayerPrefs.GetInt("Achievements completed", 0)+newCounter-counter); onMetaAchievementsProgress(); }
    }
    public void onLevellingAchievementProgress() // when playerprefs "Highest level reached" changes update only the affected achievenements as well as the meta achievement
    {
        float prevFillAmount;
        int counter = 0;
        for (int i = 8; i < 17; i++) 
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) counter++;
        }

        prevFillAmount = transform.GetChild(8).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(8).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) - 1;
        transform.GetChild(8).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 2) - 1 + "/1";
        if (transform.GetChild(8).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Level up for the first time"); }

        prevFillAmount = transform.GetChild(9).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(9).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 10.0f;
        transform.GetChild(9).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 10) + "/10";
        if (transform.GetChild(9).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Reach level 10"); }

        prevFillAmount = transform.GetChild(10).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(10).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 50.0f;
        transform.GetChild(10).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 50) + "/50";
        if (transform.GetChild(10).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Reach level 50"); }

        prevFillAmount = transform.GetChild(11).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(11).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 100.0f;
        transform.GetChild(11).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 100) + "/100";
        if (transform.GetChild(11).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Reach level 100"); }

        prevFillAmount = transform.GetChild(12).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(12).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 200.0f;
        transform.GetChild(12).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 200) + "/200";
        if (transform.GetChild(12).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Reach level 200"); }

        prevFillAmount = transform.GetChild(13).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(13).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 400.0f;
        transform.GetChild(13).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 400) + "/400";
        if (transform.GetChild(13).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Reach level 400"); }

        prevFillAmount = transform.GetChild(14).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(14).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 600.0f;
        transform.GetChild(14).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 600) + "/600";
        if (transform.GetChild(14).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Reach level 600"); }

        prevFillAmount = transform.GetChild(15).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(15).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 800.0f;
        transform.GetChild(15).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 800) + "/800";
        if (transform.GetChild(15).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Reach level 800"); }

        prevFillAmount = transform.GetChild(16).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(16).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Highest level reached",1) / 1000.0f;
        transform.GetChild(16).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Highest level reached", 1), 1000) + "/1000";
        if (transform.GetChild(16).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Reach max level"); }

        steamObject.updateHighestLevelReached(PlayerPrefs.GetInt("Highest level reached", 1));

        int newCounter = 0;
        for (int i = 8; i < 17; i++)
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) newCounter++;
        }

        if (newCounter != counter) { PlayerPrefs.SetInt("Achievements completed", PlayerPrefs.GetInt("Achievements completed", 0)+newCounter-counter); onMetaAchievementsProgress(); }
    }
    public void onCraftingAchievementProgress() // when playerprefs "Total items crafted" changes update only the affected achievenements as well as the meta achievement
    {
        float prevFillAmount;
        int counter = 0;
        for (int i = 17; i < 23; i++) 
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) counter++;
        }

        prevFillAmount = transform.GetChild(17).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(17).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0);
        transform.GetChild(17).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 1) + "/1";
        if (transform.GetChild(17).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Craft an item"); }

        prevFillAmount = transform.GetChild(18).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(18).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 10.0f;
        transform.GetChild(18).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 10) + "/10";
        if (transform.GetChild(18).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Craft 10 items"); }

        prevFillAmount = transform.GetChild(19).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(19).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 25.0f;
        transform.GetChild(19).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 25) + "/25";
        if (transform.GetChild(19).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Craft 25 items"); }

        prevFillAmount = transform.GetChild(20).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(20).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 50.0f;
        transform.GetChild(20).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 50) + "/50";
        if (transform.GetChild(20).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Craft 50 items"); }

        prevFillAmount = transform.GetChild(21).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(21).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 75.0f;
        transform.GetChild(21).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 75) + "/75";
        if (transform.GetChild(21).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Craft 75 items"); }

        prevFillAmount = transform.GetChild(22).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(22).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total items crafted",0) / 100.0f;
        transform.GetChild(22).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total items crafted", 0), 100) + "/100";
        if (transform.GetChild(22).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Craft 100 items"); }

        steamObject.updateTotalItemsCrafted(PlayerPrefs.GetInt("Total items crafted", 0));

        int newCounter = 0;
        for (int i = 17; i < 23; i++)
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) newCounter++;
        }

        if (newCounter != counter) { PlayerPrefs.SetInt("Achievements completed", PlayerPrefs.GetInt("Achievements completed", 0)+newCounter-counter); onMetaAchievementsProgress(); }
    }
    public void onEquipmentAchievementProgress() // when playerprefs "Max Slots Filled" and "Most unique equipped" change(they are together in code so no point to seperate them) update only the affected achievenements as well as the meta achievement
    {
        float prevFillAmount;
        int counter = 0;
        for (int i = 23; i < 26; i++) 
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) counter++;
        }

        prevFillAmount = transform.GetChild(23).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(23).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Max Slots Filled",0) / 14.0f;
        transform.GetChild(23).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Max Slots Filled", 0), 14) + "/14";
        if (transform.GetChild(23).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Equip items in all slots"); }

        prevFillAmount = transform.GetChild(24).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(24).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Most unique equipped",0);
        transform.GetChild(24).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Most unique equipped", 0), 1) + "/1";
        if (transform.GetChild(24).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Equip a unique item"); }

        prevFillAmount = transform.GetChild(25).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(25).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Most unique equipped",0) / 14.0f;
        transform.GetChild(25).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Most unique equipped", 0), 14) + "/14";
        if (transform.GetChild(25).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Equip unique items in\nall slots"); }

        steamObject.updateMaxSlotsFilled(PlayerPrefs.GetInt("Max Slots Filled", 0));
        steamObject.updateTotalUniqueEquipped(PlayerPrefs.GetInt("Most unique equipped", 0));

        int newCounter = 0;
        for (int i = 23; i < 26; i++)
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) newCounter++;
        }

        if (newCounter != counter) { PlayerPrefs.SetInt("Achievements completed", PlayerPrefs.GetInt("Achievements completed", 0)+newCounter-counter); onMetaAchievementsProgress(); }
    }
    public void onCollectionsAchievementProgress() // when playerprefs for the collection change update only the affected achievenements as well as the meta achievement
    {
        float prevFillAmount;
        int counter = 0;
        for (int i = 26; i < 29; i++) 
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) counter++;
        }

        prevFillAmount = transform.GetChild(26).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(26).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total collections completed",0);
        transform.GetChild(26).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total collections completed", 0), 1) + "/1";
        if (transform.GetChild(26).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1){ showAchievementNotification("Complete a collection"); }

        prevFillAmount = transform.GetChild(27).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(27).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Collected weapons",1)/95.0f;
        transform.GetChild(27).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Collected weapons", 1), 95) + "/95";
        if (transform.GetChild(27).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Collect all weapons"); }

        prevFillAmount = transform.GetChild(28).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(28).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Total collections completed",0) / 13.0f;
        transform.GetChild(28).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Total collections completed", 0), 13) + "/13";
        if (transform.GetChild(28).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Complete all collections"); }

        steamObject.updateTotalCollectionsCompleted(PlayerPrefs.GetInt("Total collections completed", 0));
        steamObject.updateTotalWeaponsCollected(PlayerPrefs.GetInt("Collected weapons", 1));

        int newCounter = 0;
        for (int i = 26; i < 29; i++)
        {
            if (transform.GetChild(i).GetChild(2).GetComponent<Image>().fillAmount >= 1) newCounter++;
        }

        if (newCounter != counter) { PlayerPrefs.SetInt("Achievements completed", PlayerPrefs.GetInt("Achievements completed", 0)+newCounter-counter); onMetaAchievementsProgress(); }
    }
    void onMetaAchievementsProgress()
    {
        float prevFillAmount = transform.GetChild(29).GetChild(2).GetComponent<Image>().fillAmount;
        transform.GetChild(29).GetChild(2).GetComponent<Image>().fillAmount = PlayerPrefs.GetInt("Achievements completed",0) / 29.0f;
        transform.GetChild(29).GetChild(4).GetComponent<TextMeshProUGUI>().text = Mathf.Min(PlayerPrefs.GetInt("Achievements completed", 0), 29) + "/29";
        if (transform.GetChild(29).GetChild(2).GetComponent<Image>().fillAmount == 1 && prevFillAmount != 1) { showAchievementNotification("Unlock all achievements"); }

        steamObject.updateTotalAchievementsCompleted(PlayerPrefs.GetInt("Achievements completed", 0));
    }
    void showAchievementNotification(string achievementName) 
    {
        Analytics.CustomEvent("Achievement earned", new Dictionary<string, object> {
            { "Name", achievementName},
            { "Level", powerLevelCalculatorComponent.getRoundedPowerLevel() },
            { "OS and Version", SystemInfo.operatingSystem },
            { "Playthrough time", PlayerPrefs.GetInt("Playthrough time", 0) + (int)Time.timeSinceLevelLoad / 60 }
        });

        if (speedrunCanvasComponent.enabled == true) { return; }
        soundFX.playAchievement();
        GameObject newAchievementPopUp = Instantiate(achievementPopUpPrefab, achievementPopUpContainer.transform);
        newAchievementPopUp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(achievementName);
        
    }
}
