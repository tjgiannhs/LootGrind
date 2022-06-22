using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour
{
    void Start()
    {   //sends analytics information on startup and immediately loads game scene
        Analytics.CustomEvent("Operating System", new Dictionary<string, object>
            {
                { "OS and Version", SystemInfo.operatingSystem},
                { "Session number", PlayerPrefs.GetInt("Played", 1)}
            });
        PlayerPrefs.SetInt("Played",PlayerPrefs.GetInt("Played") + 1);
        SceneManager.LoadScene("GameScene");
    }
}
