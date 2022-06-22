using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PopUpButton : MonoBehaviour
{
    [SerializeField] GameObject popUpCanvas;
    [SerializeField] GameObject collectionContent;
    [SerializeField] GameObject character;
    [SerializeField] GameObject refreshQuests;

    public void hidePopUp()
    {
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
        transform.parent.gameObject.SetActive(false);
    }

    public void hideCraftingPopUp()
    {
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
        transform.parent.gameObject.SetActive(false);
    }

    public void exitGame() 
    {
        //this code now runs always before closing by any way so there is no need for it to be executed twice
        /*
        Analytics.CustomEvent("Session time", new Dictionary<string, object>
            {
                { "Minutes", Time.timeSinceLevelLoad/60}
            }) ;
        PlayerPrefs.SetInt("Playthrough time", PlayerPrefs.GetInt("Playthrough time", 0)+(int)Time.timeSinceLevelLoad/60);
        */
        Application.Quit();
    }

    public void closeExitPopup() 
    {
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
        transform.parent.gameObject.SetActive(false);
    }

    /*
    //the last used button can be pressed again with space or enter so we don't encourage the player to use them in any way
    private void Update()//close the popup that shows the item you just got by pressing space or enter as well as using the button
    {
        if (transform.parent.name== "ItemPopUp" && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))) { hidePopUp(); }
    }*/
}
