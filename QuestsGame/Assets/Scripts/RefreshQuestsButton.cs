using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RefreshQuestsButton : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    SoundFX soundFX;

    int counter = 0;

    private void Start()
    {
        soundFX = gameManager.GetComponent<SoundFX>();
        counter = PlayerPrefs.GetInt("Refresh Quests Counter", 0);
        setCounterText();
    }

    void setCounterText() 
    {
        if (counter >= 5)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = counter + "/10";
            transform.GetComponent<Image>().color = Color.white;
        }
        else 
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = counter + "/5";
            transform.GetComponent<Image>().color = Color.gray;
        }

    }

    public void addCompletedQuest() 
    {
        counter++;
        counter = Mathf.Clamp(counter,0,10) ;
        PlayerPrefs.SetInt("Refresh Quests Counter", counter);
        setCounterText();
    }

    public void refreshQuests()
    {
        if (counter >= 5)
        {
            soundFX.playRefreshQuests();
            for (int i = 3; i < transform.parent.childCount; i++) 
            {
                transform.parent.GetChild(i).GetComponent<QuestManager>().createQuest();
            }
            counter -= 5;
            PlayerPrefs.SetInt("Refresh Quests Counter", counter);
            setCounterText();
        }
    }
    
    public void refreshQuestsFree()
    {
        if (counter >= 5)
        {
            for (int i = 3; i < transform.parent.childCount; i++) 
            {
                transform.parent.GetChild(i).GetComponent<QuestManager>().createQuest();
            }
        }
    }
}
