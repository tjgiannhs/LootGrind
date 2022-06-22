using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    SoundFX soundFX;

    [SerializeField] GameObject equipmentCanvas;
    [SerializeField] GameObject questsCanvas;
    [SerializeField] GameObject activityCanvas;
    [SerializeField] GameObject collectionCanvas;
    [SerializeField] GameObject craftingCanvas;
    [SerializeField] GameObject activeQuests;
    [SerializeField] GameObject collectionContent;
    [SerializeField] GameObject craftingButtons;
    [SerializeField] GameObject popUp;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject menusBackground;

    Canvas equipmentCanvasComponent;
    Canvas craftingCanvasComponent;
    Canvas collectionCanvasComponent;
    Canvas questsCanvasComponent;
    Canvas activityCanvasComponent;
    Canvas popUpCanvasComponent;
    Canvas menuCanvasComponent;
    Canvas menusBackgroundCanvasComponent;

    ScrollContent activeQuestsScrollContent;
    ScrollContent collectionScrollContent;

    [SerializeField] bool myUpdateWorks;//only one of the buttons has it true because we want to do the actions once, not for each button



    private void Start()
    {
        soundFX = gameManager.GetComponent<SoundFX>();

        equipmentCanvasComponent = equipmentCanvas.GetComponent<Canvas>();
        craftingCanvasComponent = craftingCanvas.GetComponent<Canvas>();
        collectionCanvasComponent = collectionCanvas.GetComponent<Canvas>();
        questsCanvasComponent = questsCanvas.GetComponent<Canvas>();
        activityCanvasComponent = activityCanvas.GetComponent<Canvas>();
        activeQuestsScrollContent = activeQuests.GetComponent<ScrollContent>();
        collectionScrollContent = collectionContent.GetComponent<ScrollContent>();
        popUpCanvasComponent = popUp.GetComponent<Canvas>();
        menuCanvasComponent = menuCanvas.GetComponent<Canvas>();
        menusBackgroundCanvasComponent = menusBackground.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (!myUpdateWorks) { return; }
        if (menusBackgroundCanvasComponent.enabled) { return; }
        if (menuCanvasComponent.enabled) { return; }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a") /*|| Input.GetKeyDown("q")*/) { goToLeftTab(); }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d")/* || Input.GetKeyDown("e")*/) { goToRightTab(); }

    }

    private void goToLeftTab()
    {
        if (popUpCanvasComponent.enabled) { return; }

        if (craftingCanvasComponent.enabled == true)
        {
            enableCollectionCanvas();
        }
        else if (collectionCanvasComponent.enabled == true)
        {
            enableEquipmentCanvas();
        }
        else if (equipmentCanvasComponent.enabled == true)
        {
            enableQuestsCanvas();
        }
        else if (questsCanvasComponent.enabled == true) 
        {
            enableActivityCanvas();
        }
    }

    private void goToRightTab()
    {
        if (popUpCanvasComponent.enabled) { return; }

        if (activityCanvasComponent.enabled == true)
        {
            enableQuestsCanvas();
        }
        else if (questsCanvasComponent.enabled == true)
        {
            enableEquipmentCanvas();
        }
        else if (equipmentCanvasComponent.enabled == true)
        {
            enableCollectionCanvas();
        }
        else if (collectionCanvasComponent.enabled == true)
        {
            enableCraftingCanvas();
        }
    }

    public void disableEquipmentCanvas() 
    {
        equipmentCanvasComponent.enabled = false;
    }
    public void disableCraftingCanvas() 
    {
        craftingCanvasComponent.enabled = false;
    }
    public void disableCollectionCanvas() 
    {
        collectionCanvasComponent.enabled = false;
    }
    public void disableQuestsCanvas() 
    {
        questsCanvasComponent.enabled = false;
    }
    public void disableActivityCanvas() 
    {
        activityCanvasComponent.enabled = false;
    }
    
    public void enableEquipmentCanvas()
    {
        if (equipmentCanvasComponent.enabled != true) { soundFX.playWoodButtonClick(); }
        equipmentCanvasComponent.enabled = true;
        disableActivityCanvas();
        disableQuestsCanvas();
        disableCollectionCanvas();
        disableCraftingCanvas();
    }    
    public void enableQuestsCanvas()
    {
        if (questsCanvasComponent.enabled != true) { soundFX.playWoodButtonClick(); }
        questsCanvasComponent.enabled = true;
        disableActivityCanvas();
        disableEquipmentCanvas();
        disableCollectionCanvas();
        disableCraftingCanvas();
    }
    public void enableActivityCanvas()
    {
        if (activityCanvasComponent.enabled != true) { soundFX.playWoodButtonClick(); }
        activityCanvasComponent.enabled = true;
        activeQuestsScrollContent.ScrollToTop();
        disableQuestsCanvas();
        disableEquipmentCanvas();
        disableCollectionCanvas();
        disableCraftingCanvas();
    }
    public void enableCollectionCanvas()
    {
        if (collectionCanvasComponent.enabled != true) { soundFX.playWoodButtonClick(); }
        collectionCanvasComponent.enabled = true;
        collectionScrollContent.ScrollToTop();
        disableActivityCanvas();
        disableQuestsCanvas();
        disableEquipmentCanvas();
        disableCraftingCanvas();
    }
    public void enableCraftingCanvas() 
    {
        if (craftingCanvasComponent.enabled != true) { soundFX.playWoodButtonClick(); }
        craftingCanvasComponent.enabled = true;
        craftingButtons.transform.position = new Vector2(craftingButtons.transform.position.x, 0);
        disableActivityCanvas();
        disableQuestsCanvas();
        disableEquipmentCanvas();
        disableCollectionCanvas();
    }

}
