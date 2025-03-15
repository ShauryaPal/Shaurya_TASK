using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager_Player : MonoBehaviour
{
    public void LoadPlayerData()
    {
        var playerSavedData = SerializationManager.LoadPlayerData();
        PlayerReferences.Instance.health.LoadSavedData(playerSavedData.health);
        PlayerReferences.Instance.playerTransform.localPosition = new Vector3( playerSavedData.position[0], playerSavedData.position[1], playerSavedData.position[2]);
        PlayerReferences.Instance.inventory.LoadSavedData(playerSavedData.inventory);
    }

    public void SavePlayerData()
    {
        SerializationManager.SaveData(GetPlayerSaveData(), SerializationManager.playerSaveDataFileName);
    }

    private static SaveData_Player GetPlayerSaveData()
    {
        List<SaveData_PlayerInventory> inventorySaveData = new();

        foreach (var slot in PlayerReferences.Instance.inventory.inventorySlots)
        {
            var scriptableItemIndex = -1;

            if (slot.haveItem)
                scriptableItemIndex = DataReferences.Instance.GetIndexOfScriptableItem(slot.itemData.itemData);
            
            inventorySaveData.Add(new SaveData_PlayerInventory { itemQuantity = slot.quantity, scriptableItemIndex = scriptableItemIndex});
        }
        
        var playerSaveData = new SaveData_Player {
            health = PlayerReferences.Instance.health.currentHealth,
            position = new[] { PlayerReferences.Instance.playerTransform.localPosition.x, PlayerReferences.Instance.playerTransform.localPosition.y, PlayerReferences.Instance.playerTransform.localPosition.z },
            inventory = inventorySaveData.ToArray()
        };
        
        return playerSaveData;
    }
}

[Serializable]
public class SaveData_Player
{
    public float[] position;
    public int health;
    public SaveData_PlayerInventory[] inventory;
}

[Serializable]
public struct SaveData_PlayerInventory
{
    public int scriptableItemIndex;
    public int itemQuantity;
}
