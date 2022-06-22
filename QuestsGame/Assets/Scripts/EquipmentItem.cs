using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class EquipmentItem
{
    [SerializeField] Sprite itemImage;
    [SerializeField] string itemImagePath;
    [SerializeField] string itemName;
    [SerializeField] int    itemLevel;

    public EquipmentItem() 
    {
        itemImage = null; itemLevel = 0; itemName = "";
    }
    public Sprite getItemImage() { return itemImage; }    
    public string getItemImagePath() { return itemImagePath; }
    public string getItemName() { return itemName; }
    public int getItemLevel() { return itemLevel; }

    public void setItemImage(Sprite image) { itemImage = image; }
    public void setItemImage() { itemImage = Resources.Load<Sprite>(itemImagePath); }
    public void setItemImagePath(string path) { itemImagePath = path; }
    public void setItemName(string name) { itemName = name; }
    public void setItemLevel(int level) { itemLevel = level; }
}