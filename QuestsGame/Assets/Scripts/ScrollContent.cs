using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollContent : MonoBehaviour
{
    [SerializeField] bool isCollection;
    [SerializeField] GameObject emptyPrefab;
    [SerializeField] GameObject imagePrefab;
    [SerializeField] GameObject gameManager;

    GameFunctions gameFunctions;
    RectTransform myRectTransform;
    FileAccessor fileAccessor;

    private void Start()
    {
        gameFunctions = gameManager.GetComponent<GameFunctions>();
        myRectTransform = transform.GetComponent<RectTransform>();
        fileAccessor = GetComponent<FileAccessor>();
        if (isCollection) { loadCollection(); }
    }

    public void ScrollToTop() 
    {
        transform.position = new Vector2(transform.position.x, 0);
    }

    private void loadCollection()
    {
        string[] itemPaths = fileAccessor.getUnlockedItems().Split('\n');
        foreach (string s in itemPaths)
        {
            print(s);
        }
        int totalItems = 0;
        for (int i = 1; i < 14; i++) 
        {
            Instantiate(emptyPrefab,transform);
            transform.GetChild(i-1).GetComponent<RectTransform>().anchorMin = new Vector2(0,1);
            transform.GetChild(i-1).GetComponent<RectTransform>().anchorMax = new Vector2(0,1);
            transform.GetChild(i-1).GetComponent<RectTransform>().pivot = new Vector2(0.5f,1);
            transform.GetChild(i - 1).GetComponent<RectTransform>().localPosition = new Vector2(500, -totalItems / 5 * 200 - (i - 1) * 60 - 20);
            string catergoryName = gameFunctions.getSlotNameFromIndex(i);
            if (catergoryName[catergoryName.Length-1]!='s')
            {
                transform.GetChild(i - 1).GetComponent<TextMeshProUGUI>().text = catergoryName+"s";
            }
            else 
            {
                transform.GetChild(i - 1).GetComponent<TextMeshProUGUI>().text = catergoryName;
            }

            List<string> itemStrings = gameFunctions.getItemsForResourceLoad(i);
            //print( itemStrings.Count); 

            //var info = new DirectoryInfo(Application.dataPath + "/Resources/" + gameFunctions.getSlotNameFromIndex(i));
            //var files = info.GetFiles("*.png");
            totalItems+=itemStrings.Count;
            
            //print(totalItems);
            for (int j = 0; j < itemStrings.Count; j++) 
            {
                Instantiate(imagePrefab, transform.GetChild(i - 1));
                transform.GetChild(i-1).GetChild(j).GetComponent<Image>().sprite = Resources.Load<Sprite>(itemStrings[j]);
                if (Array.IndexOf(itemPaths, itemStrings[j]) <= -1) { transform.GetChild(i - 1).GetChild(j).GetComponent<Image>().color = new Color(15 / 255.0f, 15 / 255.0f, 15 / 255.0f); }
            }
            //transform.GetChild(i - 1).GetComponent<LayoutElement>().preferredHeight = files.Length/5*200+50;
            transform.GetChild(i-1).GetComponent<RectTransform>().sizeDelta = new Vector2(1000, itemStrings.Count/ 5 * 200 + 50);
            //print(totalItems / 5 * 200 + 50);
        }
        myRectTransform.sizeDelta = new Vector2(1000, totalItems / 5 * 200 + 13 * 63);
        //transform.GetComponent<RectTransform>().position = new Vector2(540,0);

    }

    public void highlightNewlyObtainedItem(int type,Sprite itemSprite) 
    {
        if (type == 0) type = 1;
        for (int i = 0; i < transform.GetChild(type - 1).childCount; i++) 
        {
            //gia ka8e antikeimeno autou tou typou
            if (transform.GetChild(type - 1).GetChild(i).GetComponent<Image>().sprite == itemSprite) 
            {
                //an to antikeimeno auto den htan hdh syllegmeno
                if (transform.GetChild(type - 1).GetChild(i).GetComponent<Image>().color != Color.white) 
                {
                    //print(GameObject.Find("GameManager").GetComponent<GameFunctions>().getSlotNameFromIndex(type)+"/"+itemSprite.name);

                    //prwta to kommati ths emfanishs ston paikth ki an den ginei h apo8hkeush amesws meta 8a ksanafainetai mauro se epomena playthrough alla me ena crafting 8a ksanaasprisei
                    //Ginetai ligotero pi8ano na ginei antilhpto kai na enoxlhsei paikth auto to hdh poly spanio bug tou na mhn gemizei h syllogh eksaitias tou teleutaiou crafted item ths sylloghs
                    transform.GetChild(type - 1).GetChild(i).GetComponent<Image>().color = Color.white;
                    fileAccessor.saveNewUnlockedItems(gameFunctions.getSlotNameFromIndex(type) + "/" + itemSprite.name);
                }
                break;
            }
        }
    }

    void Update()
    {
        if (isCollection) 
        {
            return;
        }
        if (transform.childCount * 235 > 1740)
        {
            if (transform.childCount * 235 + 100 != myRectTransform.sizeDelta.y)
            {
                myRectTransform.sizeDelta = new Vector2(1060, 100 + transform.childCount * 235);
                transform.localPosition = new Vector2(transform.localPosition.x, -myRectTransform.sizeDelta.y / 2);
            }
        }
        else 
        {
            myRectTransform.sizeDelta = new Vector2(1060, 1740);
        }
        //else if(transform.GetComponent<RectTransform>().sizeDelta.y!=1450 && transform.childCount * 160 != transform.GetComponent<RectTransform>().sizeDelta.y)
        //{
        //    transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1060, 1650);
        //}
    }
}
