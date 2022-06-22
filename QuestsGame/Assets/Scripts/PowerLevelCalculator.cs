using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PowerLevelCalculator : MonoBehaviour
{

    public int WeaponRItemLevel;
    public int WeaponLItemLevel;
    public int HelmetItemLevel;
    public int ChestItemLevel;

    public int ShouldersItemLevel;
    public int BracersItemLevel;
    public int PantsItemLevel;
    public int BootsItemLevel;
    public int NecklaceItemLevel;

    public int GlovesItemLevel;
    public int BeltItemLevel;
    public int CloakItemLevel;
    public int RingItemLevel;
    public int TrinketItemLevel;

    [SerializeField] float previousPowerLevel = 0;
    [SerializeField] float PowerLevel = 0;
    [SerializeField] int RoundedPowerLevel;

    [SerializeField] GameObject characterEquipment;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject levelUpText;
    [SerializeField] GameObject speedrunTimers;
    [SerializeField] GameObject achievementsContainer;

    CharacterEquipment characterEquipmentComponent;
    SoundFX soundFX;
    Speedrun speedrunTimersScript;
    AchievementsScript achievementsScript;

    void calcPowerLevel()
    {
        previousPowerLevel = PowerLevel;
        PowerLevel = 9 / 111.0f * (WeaponRItemLevel + WeaponLItemLevel + HelmetItemLevel + ChestItemLevel)
                   + 8 / 111.0f * (ShouldersItemLevel + BracersItemLevel + PantsItemLevel + BootsItemLevel + NecklaceItemLevel)
                   + 7 / 111.0f * (GlovesItemLevel + BeltItemLevel + CloakItemLevel + RingItemLevel + TrinketItemLevel);
        RoundedPowerLevel = (int)PowerLevel;
    }



    // Start is called before the first frame update
    void Awake()
    {
        calcPowerLevel();
        //if (previousPowerLevel != PowerLevel)
        //{
        characterEquipmentComponent = characterEquipment.GetComponent<CharacterEquipment>();
        characterEquipmentComponent.showPowerLevel();
        soundFX = gameManager.GetComponent<SoundFX>();
        speedrunTimersScript = speedrunTimers.GetComponent<Speedrun>();
        achievementsScript = achievementsContainer.GetComponent<AchievementsScript>();
            //if ((int)previousPowerLevel < RoundedPowerLevel && previousPowerLevel != 0) soundFX.playLevelUp();
        //}

        //We set up highest level immediately so that the achievements can have a value before any level ups
        //PlayerPrefs.SetInt("Highest level", PlayerPrefs.GetInt("Highest level", 1));
        //print("Highest level: " + PlayerPrefs.GetInt("Highest level",1));
    }

    // Update is called once per frame
    void Update()
    {
        calcPowerLevel();
        if (previousPowerLevel != PowerLevel) //an yphrkse allagh sto powerlevel
        {
            characterEquipmentComponent.showPowerLevel();
            if ((int)previousPowerLevel < RoundedPowerLevel && previousPowerLevel != 0) //an anebhke epipedo o paikths, oxi apla aukshsh tou posostou pros to epomeno epipedo
            {
                PlayerPrefs.SetInt("Highest level reached", Math.Max(PlayerPrefs.GetInt("Highest level reached", 1), RoundedPowerLevel));

                achievementsScript.onLevellingAchievementProgress();

                //print("Highest level: " + PlayerPrefs.GetInt("Highest level"));

                Analytics.CustomEvent("Reached Level", new Dictionary<string, object>
                {
                    { "lvl", RoundedPowerLevel}
                });

                if (RoundedPowerLevel < 1000 || PlayerPrefs.GetInt("Level Cap", 1) < 0.5f)//mexri kai to epipedo 999 paizoume hxo kai emfanizetai to levelup h an den yparxei level cap, dhl einai 0 h timh
                {
                    levelUpText.gameObject.SetActive(true);
                    levelUpText.GetComponent<LevelUpTextBehavior>().resetAlpha();
                    soundFX.playLevelUp();
                }

                if (RoundedPowerLevel >= 1000 && (int)previousPowerLevel<1000) //thn prwth fora pou 8a kseperastei to 1000 level, eite einai 1000 akribws eite parapanw stamata kai to timer
                {
                    levelUpText.gameObject.SetActive(true);
                    levelUpText.GetComponent<LevelUpTextBehavior>().resetAlpha();
                    soundFX.playMaxLevelReached();
                    soundFX.playLevelUp();

                    speedrunTimersScript.stopLevelTimer();

                    Analytics.CustomEvent("Reached Max Level", new Dictionary<string, object>
                    {
                        { "Quests completed", PlayerPrefs.GetInt("Quests completed", 0) },
                        { "Items crafted", PlayerPrefs.GetInt("Items Crafted", 0) },
                        { "Playthrough time", PlayerPrefs.GetInt("Playthrough time",0)+(int)Time.timeSinceLevelLoad/60},
                        { "OS and Version", SystemInfo.operatingSystem}
                    });
                }
            }
        }
    }

    public int getRoundedPowerLevel() { return RoundedPowerLevel; }
    public float getPowerLevel() { return PowerLevel; }
}
