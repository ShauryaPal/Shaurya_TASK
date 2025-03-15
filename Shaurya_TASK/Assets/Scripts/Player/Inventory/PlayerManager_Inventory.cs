using System;
using UnityEngine;
using System.Linq;

public class PlayerManager_Inventory : MonoBehaviour
{
    public event Action OnItemAdded;
    public event Action<ItemData> OnItemDepleted;
    public Inventory_Slot[] inventorySlots;

    public bool HaveItem(Scriptable_Item checkItem, out Inventory_Slot itemSlot)
    {
        foreach (var slot in inventorySlots)
            if (slot.haveItem && slot.assignedItem == checkItem)
            {
                itemSlot = slot;
                return true;
            }

        itemSlot = null;
        return false;
    }

    public void OnItemDepletedFromInventory(ItemData itemData)
    {
        OnItemDepleted?.Invoke(itemData);
    }

    public void OnItemUsedFromInventory(ItemData itemData)
    {
        foreach (var slot in inventorySlots)
            if (slot.haveItem && slot.assignedItem == itemData.itemData)
                slot.UpdateItemQuantity(-1);
    }

    public void AddItemToInventory(Dropped_Item item)
    {
        OnItemAdded?.Invoke();
        var itemData = item.GetItemData();
        
        var itemAlreadyInInventory = inventorySlots.Any(i => i.assignedItem == itemData.itemData);

        if (itemAlreadyInInventory)
        {
            inventorySlots.First(i => i.assignedItem == itemData.itemData).UpdateItemQuantity(itemData.quantity);
        }
        else
        {
            var freeSpaceAvailableInInventory = inventorySlots.Any(i => !i.haveItem);

            if (freeSpaceAvailableInInventory)
                inventorySlots.First(i => !i.haveItem).AssignItem(itemData);
        }
        
        item.OnItemPickedUp();
    }
    
    public void AddItemToInventory(ItemData itemData)
    {
        var itemAlreadyInInventory = inventorySlots.Any(i => i.assignedItem == itemData.itemData);

        if (itemAlreadyInInventory)
        {
            inventorySlots.First(i => i.assignedItem == itemData.itemData).UpdateItemQuantity(itemData.quantity);
        }
        else
        {
            var freeSpaceAvailableInInventory = inventorySlots.Any(i => !i.haveItem);

            if (freeSpaceAvailableInInventory)
                inventorySlots.First(i => !i.haveItem).AssignItem(itemData);
        }
    }

    public void LoadSavedData(SaveData_PlayerInventory[] savedData)
    {
        for (var i = 0; i < savedData.Length; i++)
        {
            var item = savedData[i];
            var slot = inventorySlots[i];

            if (item.scriptableItemIndex != -1)
                slot.AssignItem(new ItemData(){itemData = DataReferences.Instance.GetScriptableItemFromIndex(item.scriptableItemIndex), quantity = item.itemQuantity});
            else
                slot.ResetSlot();
        }
    }
    
}
