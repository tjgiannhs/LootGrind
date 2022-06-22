using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class MenuBehavior : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    SoundFX soundFX;
    GameFunctions gameFunctions;

    [SerializeField] GameObject activityCanvas;
    [SerializeField] GameObject questsCanvas;
    [SerializeField] GameObject equipmentCanvas;
    [SerializeField] GameObject collectionCanvas;
    [SerializeField] GameObject craftingCanvas;
    [SerializeField] GameObject creditsCanvas;
    [SerializeField] GameObject howToPlayCanvas;
    [SerializeField] GameObject achievementsCanvas;
    [SerializeField] GameObject menusBackgroundCanvas;
    [SerializeField] GameObject speedrunCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject itemPopUp;
    [SerializeField] GameObject topPopUp;

    Canvas myCanvasComponent;
    Canvas activityCanvasComponent;
    Canvas questsCanvasComponent;
    Canvas equipmentCanvasComponent;
    Canvas collectionCanvasComponent;
    Canvas craftingCanvasComponent;
    Canvas creditsCanvasComponent;
    Canvas howToPlayCanvasComponent;
    Canvas achievementsCanvasComponent;
    Canvas menusBackgroundCanvasComponent;
    Canvas speedrunCanvasComponent;
    Canvas settingsCanvasComponent;
    Canvas topPopUpCanvasComponent;

    PopUpButton itemPopUpButtonScript;

    Transform achievementsScroll;

    private void Start()
    {
        soundFX = gameManager.GetComponent<SoundFX>();
        gameFunctions = gameManager.GetComponent<GameFunctions>();

        myCanvasComponent = GetComponent<Canvas>();
        activityCanvasComponent = activityCanvas.GetComponent<Canvas>();
        questsCanvasComponent = questsCanvas.GetComponent<Canvas>();
        equipmentCanvasComponent = equipmentCanvas.GetComponent<Canvas>();
        collectionCanvasComponent = collectionCanvas.GetComponent<Canvas>();
        craftingCanvasComponent = craftingCanvas.GetComponent<Canvas>();
        creditsCanvasComponent = creditsCanvas.GetComponent<Canvas>();
        howToPlayCanvasComponent = howToPlayCanvas.GetComponent<Canvas>();
        achievementsCanvasComponent = achievementsCanvas.GetComponent<Canvas>();
        menusBackgroundCanvasComponent = menusBackgroundCanvas.GetComponent<Canvas>();
        speedrunCanvasComponent = speedrunCanvas.GetComponent<Canvas>();
        settingsCanvasComponent = settingsCanvas.GetComponent<Canvas>();
        topPopUpCanvasComponent = topPopUp.GetComponent<Canvas>();
        achievementsScroll = achievementsCanvas.transform.GetChild(2).GetChild(0).GetChild(0).transform;

        itemPopUpButtonScript = itemPopUp.transform.GetChild(4).GetComponent<PopUpButton>();

    }

    private void OnApplicationQuit()
    {
        Analytics.CustomEvent("Session time", new Dictionary<string, object>
            {
                { "Minutes", Time.timeSinceLevelLoad/60}
            });
        PlayerPrefs.SetInt("Playthrough time", PlayerPrefs.GetInt("Playthrough time", 0) + (int)Time.timeSinceLevelLoad / 60);
        Application.Quit();
    }

    private void Update()
    {
        if (speedrunCanvasComponent.enabled == true && Input.GetMouseButtonDown(0) && menusBackgroundCanvasComponent.enabled == false && itemPopUp.active == true) 
        {
            itemPopUpButtonScript.hidePopUp();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            soundFX.playEscape();

            if (topPopUpCanvasComponent.enabled == true) 
            {
                topPopUpCanvasComponent.enabled = false;
                topPopUp.transform.GetChild(3).gameObject.SetActive(false);
                topPopUp.transform.GetChild(4).gameObject.SetActive(false);
            }
            else if (!myCanvasComponent.enabled)
            {
                loadMenu();
            }
            else 
            {
                gameFunctions.showExitPopUp();
            }
        }
    
    }

    public void unloadMenu()
    {
        myCanvasComponent.enabled = false;
        //activityCanvasComponent.enabled = false;
        //questsCanvasComponent.enabled = false;
        //equipmentCanvasComponent.enabled = true;
        //collectionCanvasComponent.enabled = false;
        //craftingCanvasComponent.enabled = false;
        creditsCanvasComponent.enabled = false;
        howToPlayCanvasComponent.enabled = false;
        achievementsCanvasComponent.enabled = false;
        menusBackgroundCanvasComponent.enabled = false;
        settingsCanvasComponent.enabled = false;
    }

    public void showCreditsCanvas() 
    {
        howToPlayCanvasComponent.enabled = false;
        achievementsCanvasComponent.enabled = false;
        creditsCanvasComponent.enabled = true;
        menusBackgroundCanvasComponent.enabled = true;
        myCanvasComponent.enabled = false;
        settingsCanvasComponent.enabled = false;
    }

    public void showHowToPlayCanvas() 
    {
        creditsCanvasComponent.enabled = false;
        achievementsCanvasComponent.enabled = false;
        howToPlayCanvasComponent.enabled = true;
        menusBackgroundCanvasComponent.enabled = true;
        myCanvasComponent.enabled = false;
        settingsCanvasComponent.enabled = false;
    }

    public void showAchievementsCanvas() 
    {
        creditsCanvasComponent.enabled = false;
        howToPlayCanvasComponent.enabled = false;
        achievementsCanvasComponent.enabled = true;
        menusBackgroundCanvasComponent.enabled = true;
        myCanvasComponent.enabled = false;
        settingsCanvasComponent.enabled = false;
        achievementsScroll.position = new Vector2(transform.position.x, -4000);

    }

    public void showSettingsCanvas() 
    {
        creditsCanvasComponent.enabled = false;
        howToPlayCanvasComponent.enabled = false;
        achievementsCanvasComponent.enabled = false;
        menusBackgroundCanvasComponent.enabled = true;
        myCanvasComponent.enabled = false;
        settingsCanvasComponent.enabled = true;
    }

    public void loadMenu() 
    {
        menusBackgroundCanvasComponent.enabled = true;
        myCanvasComponent.enabled = true;

        //activityCanvasComponent.enabled = false;
        //questsCanvasComponent.enabled = false;
        //equipmentCanvasComponent.enabled = false;
        //collectionCanvasComponent.enabled = false;
        //craftingCanvasComponent.enabled = false;
    }

}
