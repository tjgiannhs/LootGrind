using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    [SerializeField] AudioClip questAdded;
    [SerializeField] AudioClip questComplete;
    [SerializeField] AudioClip gainedItem;
    [SerializeField] AudioClip levelUp;
    [SerializeField] AudioClip maxLevelReached;
    [SerializeField] AudioClip refreshQuests;
    [SerializeField] AudioClip woodButtonClick;
    [SerializeField] AudioClip escapePressed;
    [SerializeField] AudioClip resetDown;
    [SerializeField] AudioClip resetUp;
    [SerializeField] AudioClip achievement;
    [SerializeField] AudioClip link;

    private void Awake()
    {
        /*
        if (PlayerPrefs.GetInt("JustResetted") == 1) 
        {
            playResetButtonUp();
            PlayerPrefs.SetInt("JustResetted", 0);
        }*/
    }

    public void playQuestAdded() 
    {
        AudioSource.PlayClipAtPoint(questAdded,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playQuestComplete() 
    {
        AudioSource.PlayClipAtPoint(questComplete,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playGainedItem() 
    {
        AudioSource.PlayClipAtPoint(gainedItem,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playLevelUp() 
    {
        AudioSource.PlayClipAtPoint(levelUp,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playMaxLevelReached() 
    {
        AudioSource.PlayClipAtPoint(maxLevelReached,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playRefreshQuests() 
    {
        AudioSource.PlayClipAtPoint(refreshQuests,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playWoodButtonClick() 
    {
        AudioSource.PlayClipAtPoint(woodButtonClick,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    
    public void playEscape() 
    {
        AudioSource.PlayClipAtPoint(escapePressed,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playMenuButtonDown() 
    {
        AudioSource.PlayClipAtPoint(resetDown,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playLinkButtonDown() 
    {
        AudioSource.PlayClipAtPoint(link,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playResetButtonUp() 
    {
        AudioSource.PlayClipAtPoint(resetUp,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }
    public void playAchievement() 
    {
        AudioSource.PlayClipAtPoint(achievement,Camera.main.transform.position, PlayerPrefs.GetFloat("Sounds volume", 0.2f));
    }

    //public void playQuestAdded() 
    //{
    //    GetComponent<AudioSource>().clip = questAdded;
    //    GetComponent<AudioSource>().Play();
    //}
    //public void playQuestComplete() 
    //{
    //    GetComponent<AudioSource>().clip = questComplete;
    //    GetComponent<AudioSource>().Play();
    //}
    //public void playGainedItem() 
    //{
    //    GetComponent<AudioSource>().clip = gainedItem;
    //    GetComponent<AudioSource>().Play();
    //}
}
