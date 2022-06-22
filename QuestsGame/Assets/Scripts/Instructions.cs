using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    [SerializeField] GameObject titlos;
    [SerializeField] GameObject keimeno;
    [SerializeField] GameObject otherButtonParent;

    TextMeshProUGUI titlosText;
    TextMeshProUGUI keimenoText;

    string[] titloi = {"General","Quests 1","Quests 2","Quests 3","Crafting","Settings 1","Settings 2" };
    string[] keimena = { "Loot Grind Simulator is a game about earning and equipping items to increase your power level up to a maximum of 1000.\nYou can do that by choosing and completing quests as well as by crafting.\nThe game also features secondary objectives such as collecting all item images or earning all achievements.\n\nIt’s highly recommended to read through these instructions before playing.\nPressing Escape will get you back to the main menu.",
        "You can browse available quests in the quests tab. When you pick one it starts running automatically in the activity tab.\nOnce its progress bar is filled click on the quest to complete it and get an item reward. The item’s type and level depend on the info written on the quest.\nThe level of the item depends both on the quest’s level and item quality, with more stars meaning more chances for better rewards.\nThe rank of an item is determined only by its level, with higher level items belonging to higher ranks.",
        "The completion rate of a quest is affected by its level and your current power level.\nIf your current power level is higher/lower than the quest’s level then the quest progresses faster/slower, in proportion to the level difference. This means higher level quests give better rewards but take longer to complete.\nAlso note that when you start a quest and then raise your power level by equipping stronger items the quest will progress faster from that moment on, so be sure to collect your new items as soon as you can.",
        "Items you earn from quests get equipped automatically if they are stronger than your current equipment for that slot. \nYou can also change the available quests by pressing the reload button, which holds up to two charges and gets one recharge by completing and collecting the rewards from 5 quests.",
        "When you get an item, if there already is an item equipped in that slot the weaker item gets replaced and you earn a crafting material of the same rank.\nYou can trade crafting materials to make new items of a higher rank that are guaranteed to have an image that you haven’t unlocked yet in the collection.",
        "The game features the option for a speedrun timer. It can be enabled in the settings and it starts as soon as you press play.\nWhen the timer is active you can close the new item popup by clicking anywhere on the screen instead of having to press on it and achievement notifications won’t appear.\nThe timer only counts the time since its activation, so turning it off or closing the game will reset it. Please also note that the game doesn’t save your timescore.",
        "You can reset your progress from the settings and start the game again from level 1 with the option to keep your achievement progress.\nIn the settings you can also change the window size to your preference as well as choose whether to allow levelling up after reaching level 1000.\nIn addition to clicking the buttons at the top of the screen you can also cycle through the game tabs by using the left and right arrows or A and D." };
    // Start is called before the first frame update
    void Start()
    {
        titlosText = titlos.GetComponent<TextMeshProUGUI>();
        keimenoText = keimeno.GetComponent<TextMeshProUGUI>();
    }

    public void onNextButtonClick() 
    {
        otherButtonParent.GetComponent<Image>().enabled = true;
        otherButtonParent.transform.GetChild(0).GetComponent<Image>().enabled = true;
        otherButtonParent.transform.GetChild(0).GetComponent<Button>().enabled = true;

        int currentIndex =0;
        for(int i=0; i<titloi.Length; i++)
        {
            if (titloi[i] == titlosText.text)
            {
                currentIndex = i;
                break;
            }
        }

        currentIndex++;

        titlosText.text = titloi[currentIndex];
        keimenoText.text = keimena[currentIndex];

        if (currentIndex == titloi.Length-1) { transform.parent.GetComponent<Image>().enabled=false; GetComponent<Image>().enabled = false; GetComponent<Button>().enabled = false; }
    }
    public void onPreviousButtonClick() 
    {
        otherButtonParent.GetComponent<Image>().enabled = true;
        otherButtonParent.transform.GetChild(0).GetComponent<Image>().enabled = true;
        otherButtonParent.transform.GetChild(0).GetComponent<Button>().enabled = true;

        int currentIndex = 0;
        for (int i = 0; i < titloi.Length; i++)
        {
            if (titloi[i] == titlosText.text)
            {
                currentIndex = i;
                break;
            }
        }

        currentIndex--;

        titlosText.text = titloi[currentIndex];
        keimenoText.text = keimena[currentIndex];

        if (currentIndex == 0) { transform.parent.GetComponent<Image>().enabled = false; GetComponent<Image>().enabled = false; GetComponent<Button>().enabled = false; }
    }
}
