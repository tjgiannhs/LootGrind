using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamObject : MonoBehaviour
{

    [SerializeField] bool steamBuild;

    // Start is called before the first frame update
    void Start()
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }
    }

    public void updateTotalQuestsCompleted(int totalQuests)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }
        int currentTotalQuests;
        Steamworks.SteamUserStats.GetStat("Total quests completed", out currentTotalQuests);
        if (totalQuests > currentTotalQuests) { currentTotalQuests = totalQuests; }
        Steamworks.SteamUserStats.SetStat("Total quests completed", currentTotalQuests);
        setTotalQuestsCompletedAchievements(currentTotalQuests);
    }

    public void setTotalQuestsCompletedAchievements(int totalQuests)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }

        bool achievementCompleted;
        Steamworks.SteamUserStats.GetAchievement("Complete a quest", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalQuests>=1){Steamworks.SteamUserStats.SetAchievement("Complete a quest");}
        }

        Steamworks.SteamUserStats.GetAchievement("Complete 10 quests", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalQuests>=10){Steamworks.SteamUserStats.SetAchievement("Complete 10 quests");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Complete 50 quests", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalQuests>=50){Steamworks.SteamUserStats.SetAchievement("Complete 50 quests");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Complete 100 quests", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalQuests>=100){Steamworks.SteamUserStats.SetAchievement("Complete 100 quests");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Complete 250 quests", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalQuests>=250){Steamworks.SteamUserStats.SetAchievement("Complete 250 quests");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Complete 500 quests", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalQuests>=500){Steamworks.SteamUserStats.SetAchievement("Complete 500 quests");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Complete 750 quests", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalQuests>=750){Steamworks.SteamUserStats.SetAchievement("Complete 750 quests");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Complete 1000 quests", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalQuests>=1000){Steamworks.SteamUserStats.SetAchievement("Complete 1000 quests");}
        }
        Steamworks.SteamUserStats.StoreStats();
    }

    public void updateHighestLevelReached(int newLevel)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }
        int currentHighestLevelReached;
        Steamworks.SteamUserStats.GetStat("Highest level reached", out currentHighestLevelReached);
        if (newLevel > currentHighestLevelReached) { currentHighestLevelReached = newLevel; }
        Steamworks.SteamUserStats.SetStat("Highest level reached", currentHighestLevelReached);
        setHighestLevelReachedAchievements(currentHighestLevelReached);
    }

    public void setHighestLevelReachedAchievements(int highestLevel)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }

        bool achievementCompleted;
        Steamworks.SteamUserStats.GetAchievement("Level up for the first time", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (highestLevel >= 2){Steamworks.SteamUserStats.SetAchievement("Level up for the first time");}
        }

        Steamworks.SteamUserStats.GetAchievement("Reach level 10", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (highestLevel >= 10){Steamworks.SteamUserStats.SetAchievement("Reach level 10");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Reach level 50", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (highestLevel >= 50){Steamworks.SteamUserStats.SetAchievement("Reach level 50");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Reach level 100", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (highestLevel >= 100){Steamworks.SteamUserStats.SetAchievement("Reach level 100");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Reach level 200", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (highestLevel >= 200){Steamworks.SteamUserStats.SetAchievement("Reach level 200");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Reach level 400", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (highestLevel >= 400){Steamworks.SteamUserStats.SetAchievement("Reach level 400");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Reach level 600", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (highestLevel >= 600){Steamworks.SteamUserStats.SetAchievement("Reach level 600");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Reach level 800", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (highestLevel >= 800){Steamworks.SteamUserStats.SetAchievement("Reach level 800");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Reach max level", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (highestLevel >= 1000){Steamworks.SteamUserStats.SetAchievement("Reach max level");}
        }
        Steamworks.SteamUserStats.StoreStats();
    }
    public void updateTotalItemsCrafted(int totalItems)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }
        int currentTotalItemsCrafted;
        Steamworks.SteamUserStats.GetStat("Total items crafted", out currentTotalItemsCrafted);
        if(totalItems > currentTotalItemsCrafted){ currentTotalItemsCrafted = totalItems; }
        Steamworks.SteamUserStats.SetStat("Total items crafted", currentTotalItemsCrafted);
        setTotalItemsCraftedAchievements(currentTotalItemsCrafted);
    } 

    public void setTotalItemsCraftedAchievements(int itemsCrafted)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }

        bool achievementCompleted;
        Steamworks.SteamUserStats.GetAchievement("Craft an item", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (itemsCrafted >= 1){Steamworks.SteamUserStats.SetAchievement("Craft an item");}
        }

        Steamworks.SteamUserStats.GetAchievement("Craft 10 items", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (itemsCrafted >= 10){Steamworks.SteamUserStats.SetAchievement("Craft 10 items");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Craft 25 items", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (itemsCrafted >= 25){Steamworks.SteamUserStats.SetAchievement("Craft 25 items");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Craft 50 items", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (itemsCrafted >= 50){Steamworks.SteamUserStats.SetAchievement("Craft 50 items");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Craft 75 items", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (itemsCrafted >= 75){Steamworks.SteamUserStats.SetAchievement("Craft 75 items");}
        }
        
        Steamworks.SteamUserStats.GetAchievement("Craft 100 items", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (itemsCrafted >= 100){Steamworks.SteamUserStats.SetAchievement("Craft 100 items");}
        }
        Steamworks.SteamUserStats.StoreStats();
    }

    public void updateMaxSlotsFilled(int maxSlots)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }
        int currentMaxSlotsFilled;
        Steamworks.SteamUserStats.GetStat("Max Slots Filled", out currentMaxSlotsFilled);
        if(maxSlots > currentMaxSlotsFilled){ currentMaxSlotsFilled = maxSlots; }
        Steamworks.SteamUserStats.SetStat("Max Slots Filled", currentMaxSlotsFilled);
        setMaxSlotsFilledAchievement(currentMaxSlotsFilled);
    }
    
    public void setMaxSlotsFilledAchievement(int slotsFilled)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }

        bool achievementCompleted;
        Steamworks.SteamUserStats.GetAchievement("Equip items in all slots", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (slotsFilled >= 14){Steamworks.SteamUserStats.SetAchievement("Equip items in all slots");}
        }
        Steamworks.SteamUserStats.StoreStats();
    }

    public void updateTotalUniqueEquipped(int totalUnique)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }
        int currentTotalUniqueEquipped;
        Steamworks.SteamUserStats.GetStat("Most unique equipped", out currentTotalUniqueEquipped);
        if (totalUnique > currentTotalUniqueEquipped) { currentTotalUniqueEquipped = totalUnique; }
        Steamworks.SteamUserStats.SetStat("Most unique equipped", currentTotalUniqueEquipped);
        setTotalUniqueEquippedAchievements(currentTotalUniqueEquipped);
    }    
    public void setTotalUniqueEquippedAchievements(int uniqueEquipped)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }

        bool achievementCompleted;
        Steamworks.SteamUserStats.GetAchievement("Equip a unique item", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (uniqueEquipped >= 1){Steamworks.SteamUserStats.SetAchievement("Equip a unique item");}
        }       
        
        Steamworks.SteamUserStats.GetAchievement("Equip unique items in all slots", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (uniqueEquipped >= 14){Steamworks.SteamUserStats.SetAchievement("Equip unique items in all slots");}
        }
        Steamworks.SteamUserStats.StoreStats();
    }
    public void updateTotalCollectionsCompleted(int totalCollections)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }
        int currentTotalCollectionsCompleted;
        Steamworks.SteamUserStats.GetStat("Total collections completed", out currentTotalCollectionsCompleted);
        if (totalCollections > currentTotalCollectionsCompleted) { currentTotalCollectionsCompleted = totalCollections; }
        Steamworks.SteamUserStats.SetStat("Total collections completed", currentTotalCollectionsCompleted);
        setTotalCollectionsCompletedAchievement(currentTotalCollectionsCompleted);
    }
    public void setTotalCollectionsCompletedAchievement(int collectionsCompleted)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }

        bool achievementCompleted;
        Steamworks.SteamUserStats.GetAchievement("Complete a collection", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (collectionsCompleted >= 1){Steamworks.SteamUserStats.SetAchievement("Complete a collection");}
        }       
        
        Steamworks.SteamUserStats.GetAchievement("Complete all collections", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (collectionsCompleted >= 13){Steamworks.SteamUserStats.SetAchievement("Complete all collections");}
        }
        Steamworks.SteamUserStats.StoreStats();
    }
    public void updateTotalWeaponsCollected(int totalWeapons)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }
        int currentTotalWeaponsCollected;
        Steamworks.SteamUserStats.GetStat("Collected weapons", out currentTotalWeaponsCollected);
        if (totalWeapons > currentTotalWeaponsCollected) { currentTotalWeaponsCollected = totalWeapons; }
        Steamworks.SteamUserStats.SetStat("Collected weapons", currentTotalWeaponsCollected);
        setTotalWeaponsCollectedAchievement(currentTotalWeaponsCollected);
    }
    public void setTotalWeaponsCollectedAchievement(int totalWeaponsCollected)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }

        bool achievementCompleted;
        Steamworks.SteamUserStats.GetAchievement("Collect all weapons", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalWeaponsCollected >= 95){Steamworks.SteamUserStats.SetAchievement("Collect all weapons");}
        }
        Steamworks.SteamUserStats.StoreStats();
    }

    public void updateTotalAchievementsCompleted(int totalAchievements)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }
        int currentTotalAchievementsCompleted;
        Steamworks.SteamUserStats.GetStat("Achievements completed", out currentTotalAchievementsCompleted);
        if (totalAchievements > currentTotalAchievementsCompleted) { currentTotalAchievementsCompleted = totalAchievements; }
        Steamworks.SteamUserStats.SetStat("Achievements completed", currentTotalAchievementsCompleted);
        setTotalAchievementsCompletedAchievement(currentTotalAchievementsCompleted);

    }

    public void setTotalAchievementsCompletedAchievement(int totalAchievementsCompleted)
    {
        if (!steamBuild) { return; }
        if (!SteamManager.Initialized) { return; }

        bool achievementCompleted;
        Steamworks.SteamUserStats.GetAchievement("Unlock all achievements", out achievementCompleted);
        if (achievementCompleted == false)
        {
            if (totalAchievementsCompleted >= 29){Steamworks.SteamUserStats.SetAchievement("Unlock all achievements");}
        }
        Steamworks.SteamUserStats.StoreStats();
    }
}
