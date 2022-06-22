using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{   void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicPlayer");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void setMusicVolume() 
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music volume", 0.15f);
    }
}
