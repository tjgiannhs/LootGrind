using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class Speedrun : MonoBehaviour
{
    bool maxLevel;
    bool allItemsCollected;
    float gameTime;

    TextMeshProUGUI maxLevelTimer;
    TextMeshProUGUI allItemsTimer;

    Canvas speedrunCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
        maxLevel = false;
        allItemsCollected = false;
        speedrunCanvas = transform.parent.GetComponent<Canvas>();
        maxLevelTimer = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        allItemsTimer = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetInt("Game Started",0);
    }

    // Update is called once per frame
    void Update()
    {
        if (maxLevel && allItemsCollected) { return; }
        if (!speedrunCanvas.enabled) { return; }
        if (PlayerPrefs.GetInt("Game Started", 0)>0.5f)
        {
            gameTime += Time.deltaTime;
        }
        if (!maxLevel) 
        {
            //Max level: 00:00:00.00
            maxLevelTimer.text = "Max level: " + String.Format("{0:00}",(int)(gameTime / 3600)) + ":" + String.Format("{0:00}", (int)(gameTime / 60) % 60) + ":" + replaceCommaWithDot(String.Format("{0:00.00}", gameTime % 60));
        }
        if (!allItemsCollected) 
        {
            // / All items: 00:00:00.00
            allItemsTimer.text = "/ All items: " + String.Format("{0:00}", (int)(gameTime / 3600)) + ":" + String.Format("{0:00}", (int)(gameTime / 60) % 60) + ":" + replaceCommaWithDot(String.Format("{0:00.00}", gameTime % 60));
        }


    }

    public void resetGameTime() { gameTime = 0; maxLevel = false; allItemsCollected = false; }

    public void stopLevelTimer() 
    {
        maxLevel = true;
        Analytics.CustomEvent("Max level speedrun result", new Dictionary<string, object>
            {
                { "Time", String.Format("{0:00}", (int)(gameTime / 3600)) + ":" + String.Format("{0:00}", (int)(gameTime / 60)%60) + ":" + replaceCommaWithDot(String.Format("{0:00.00}", gameTime % 60))},
                { "OS and Version", SystemInfo.operatingSystem}
            });
        print("Max level time: "+String.Format("{0:00}", (int)(gameTime / 3600)) + ":" + String.Format("{0:00}", (int)(gameTime / 60) % 60) + ":" + replaceCommaWithDot(String.Format("{0:00.00}", gameTime % 60)));   
    }
    public void stopItemsTimer() 
    { 
        allItemsCollected = true;
        Analytics.CustomEvent("All items speedrun result", new Dictionary<string, object>
            {
                { "Time", String.Format("{0:00}", (int)(gameTime / 3600)) + ":" + String.Format("{0:00}", (int)(gameTime / 60)%60) + ":" + replaceCommaWithDot(String.Format("{0:00.00}", gameTime % 60))},
                { "OS and Version", SystemInfo.operatingSystem}
            });
        print("All items time: " + String.Format("{0:00}", (int)(gameTime / 3600)) + ":" + String.Format("{0:00}", (int)(gameTime / 60) % 60) + ":" + replaceCommaWithDot(String.Format("{0:00.00}", gameTime % 60))); 
    }

    string replaceCommaWithDot(string input) 
    {
        try 
        {
            return input.Split(',')[0] + "." + input.Split(',')[1]; 
        } catch
        {
            return input;
        }
    }
}
