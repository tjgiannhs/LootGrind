using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetProgressButton : MonoBehaviour
{
    bool pointerDown = false;

    [SerializeField] GameObject gameManager;
    SoundFX soundFX;

    [SerializeField] GameObject character;
    [SerializeField] GameObject powerLevelCalculator;
    [SerializeField] GameObject toggle;

    PowerLevelCalculator powerLvlCalc;
    FileAccessor fileAccessor;
    AudioSource myAudioSource;
    Image myImage;

    void Start()
    {
        myImage = GetComponent<Image>();
        myAudioSource = GetComponent<AudioSource>();
        powerLvlCalc = powerLevelCalculator.GetComponent<PowerLevelCalculator>();
        fileAccessor = character.GetComponent<FileAccessor>();
        soundFX = gameManager.GetComponent<SoundFX>();
    }

    void Update()
    {
        if (pointerDown) 
        {
            myImage.fillAmount += Time.deltaTime / 3.0f;
            if (myImage.fillAmount == 1) myAudioSource.volume = 0;
        }
    }

    public void onPointerDown() 
    {
        myAudioSource.volume = PlayerPrefs.GetFloat("Sounds volume", 0.2f); ;
        soundFX.playMenuButtonDown();
        pointerDown = true;
    }
    
    public void onPointerUp() 
    {
        myAudioSource.volume = 0;
        if (myImage.fillAmount == 1) 
        {
            Analytics.CustomEvent("Reset used", new Dictionary<string, object>
            {
                { "Level", powerLvlCalc.getRoundedPowerLevel()},
                { "OS and Version", SystemInfo.operatingSystem}
            });

            //Don't delete number of times the game has been played or audio settings, plus counters for achievements that persist resetting
            int timesPlayed = PlayerPrefs.GetInt("Played", 1);
            float soundsVolume = PlayerPrefs.GetFloat("Sounds volume", 0.2f);
            float musicVolume = PlayerPrefs.GetFloat("Music volume", 0.15f);
            float windowSlider = PlayerPrefs.GetFloat("Window slider", 0.33f);
            int levelCap = PlayerPrefs.GetInt("Level Cap", 1);
            int totalQuestsCompleted = PlayerPrefs.GetInt("Total quests completed", 0);
            int totalItemsCrafted = PlayerPrefs.GetInt("Total items crafted", 0);
            int highestLevelReached = PlayerPrefs.GetInt("Highest level reached", 1);
            int totalCollectionsCompleted = PlayerPrefs.GetInt("Total collections completed", 0);
            int collectedAllWeapons = PlayerPrefs.GetInt("Collected weapons", 1);
            int achievementsCompleted = PlayerPrefs.GetInt("Achievements completed",0);
            int mostUniqueEquipped = PlayerPrefs.GetInt("Most unique equipped", 0);
            int maxSlotsFilled = PlayerPrefs.GetInt("Max Slots Filled", 5);

            PlayerPrefs.DeleteAll();


            
            PlayerPrefs.SetInt("Played", timesPlayed);
            PlayerPrefs.SetFloat("Sounds volume", soundsVolume);
            PlayerPrefs.SetFloat("Music volume", musicVolume);
            PlayerPrefs.SetFloat("Window slider", windowSlider);
            PlayerPrefs.SetInt("Level Cap", levelCap);
            
            //if achievements are to be reset just put these lines in comments
            //if toggle is on we keep their old values otherwise achievements get deleted
            if (toggle.GetComponent<Toggle>().isOn) {
                PlayerPrefs.SetInt("Total quests completed", totalQuestsCompleted);
                PlayerPrefs.SetInt("Total items crafted", totalItemsCrafted);
                PlayerPrefs.SetInt("Highest level reached", highestLevelReached);
                PlayerPrefs.SetInt("Total collections completed", totalCollectionsCompleted);
                PlayerPrefs.SetInt("Collected weapons", collectedAllWeapons);
                PlayerPrefs.SetInt("Achievements completed", achievementsCompleted);
                PlayerPrefs.SetInt("Most unique equipped", mostUniqueEquipped);
                PlayerPrefs.SetInt("Max Slots Filled", maxSlotsFilled);
            }

            //PlayerPrefs.SetInt("JustResetted", 1);

            //popUp gia Delete achievements as well?
            fileAccessor.deleteSavefiles();
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
        pointerDown = false;
        GetComponent<Image>().fillAmount = 0;
    }

    public void onPointerExit() 
    {
        myAudioSource.volume = 0;
        pointerDown = false;
        myImage.fillAmount = 0;
    }
}
