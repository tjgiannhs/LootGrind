using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    GameFunctions gameFunctions;

    void Start()
    {
        //gameManager = GameObject.Find("GameManager");
        gameFunctions = gameManager.GetComponent<GameFunctions>();

        string craftingItems = GetComponent<FileAccessor>().getCraftingItems();
        string[] craftingMaterials = craftingItems.Split('\n');

        if (transform.name == "CraftingMaterials") 
        {
            for (int i = 0; i < transform.childCount; i++) 
            {
                transform.GetChild(i).GetComponent<Image>().color = gameFunctions.getColorByIndex(i);
                transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = craftingMaterials[i];
            }
        } else {

            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == 15)
                {
                    transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = gameFunctions.getRarityByIndex(i);
                    transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByIndex(i);
                    transform.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>().text = gameFunctions.getRarityByIndex(i);
                    transform.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByIndex(i);
                }
                else 
                {
                    transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = gameFunctions.getRarityByIndex(i);
                    transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByIndex(i);
                    transform.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>().text = gameFunctions.getRarityByIndex(i+1);
                    transform.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>().color = gameFunctions.getColorByIndex(i+1);
                }
            }
        }      
        
    }

    public void showCraftingMaterials()
    {
        string craftingItems = GetComponent<FileAccessor>().getCraftingItems();
        string[] craftingMaterials = craftingItems.Split('\n');
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = gameFunctions.getColorByIndex(i);
            transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = craftingMaterials[i];
        }
    }

}
