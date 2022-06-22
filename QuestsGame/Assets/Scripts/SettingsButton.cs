using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] GameObject speedrunCanvas;
    [SerializeField] GameObject timerOn;
    [SerializeField] GameObject timerOff;
    [SerializeField] GameObject bugForm;

    Speedrun speedrunComponent;

    private void Start()
    {
        speedrunComponent = speedrunCanvas.transform.GetChild(2).GetComponent<Speedrun>();
    }

    public void turnTimerOn() { speedrunCanvas.GetComponent<Canvas>().enabled = true; timerOff.GetComponent<Button>().interactable = true; GetComponent<Button>().interactable = false; }
    //sto off kanw to played 0 gia na stamata kai mhdenizei to timer meta to off kai na ksekinaei me to pathma tou play
    public void turnTimerOff() { speedrunCanvas.GetComponent<Canvas>().enabled = false; /*and stop timer*/ timerOn.GetComponent<Button>().interactable = true; GetComponent<Button>().interactable = false; 
        PlayerPrefs.SetInt("Game Started", 0); speedrunComponent.resetGameTime(); }

    public void showBugForm() { bugForm.SetActive(true); bugForm.transform.parent.GetComponent<Canvas>().enabled=true; }

}
