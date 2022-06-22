using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    [SerializeField] bool musicSlider;
    [SerializeField] bool windowSize;

    Slider mySlider;
    MusicPlayer musicPlayerComponent;

    private void Start()
    {
        mySlider = GetComponent<Slider>();
        musicPlayerComponent = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();

        if (windowSize) 
        {
            mySlider.value = PlayerPrefs.GetFloat("Window slider", 0.33f);
        }
        else if (musicSlider) { mySlider.value = PlayerPrefs.GetFloat("Music volume", 0.15f)*2; } 
        else { mySlider.value = PlayerPrefs.GetFloat("Sounds volume", 0.2f)*2; }
    }


    public void onSliderChange() 
    {
        if (windowSize) 
        {
            PlayerPrefs.SetFloat("Window slider", mySlider.value);
            Screen.SetResolution((int)(281+ mySlider.value*844), (int)(500+mySlider.value*1500),false);
            return;
        }

        if (musicSlider) 
        { 
            PlayerPrefs.SetFloat("Music volume", mySlider.value/2.0f);
            musicPlayerComponent.setMusicVolume(); 
        } else { PlayerPrefs.SetFloat("Sounds volume", mySlider.value/2.0f); }
    }
}
