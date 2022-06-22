using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{

    [SerializeField] GameObject gameManager;
    [SerializeField] bool isLevelCapToggle;

    SoundFX soundFX;

    private void Start()
    {
        soundFX = gameManager.GetComponent<SoundFX>();
        if (isLevelCapToggle) 
        {
            if (PlayerPrefs.GetInt("Level Cap", 1)>0.5f)
            {
                GetComponent<Toggle>().isOn = true;
            }
            else 
            {
                GetComponent<Toggle>().isOn = false;
            }
        }
    }

    public void playWoodClickSound()
    {
        soundFX.playWoodButtonClick();
    }
    public void playPaperSwooshSound()
    {
        soundFX.playLinkButtonDown();
    }

    public void setLevelCapPlayerPref() 
    {
        if(GetComponent<Toggle>().isOn == PlayerPrefs.GetInt("Level Cap", 1) > 0.5f) { return; }//this prevents the sound from playing at the initial setup because of Start()
        
        playWoodClickSound();
        if (GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("Level Cap", 1);
        }
        else 
        {
            PlayerPrefs.SetInt("Level Cap", 0);
        }

        GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterEquipment>().showPowerLevel();//show the change immediately
    }
}
