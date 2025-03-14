using System;
using UnityEngine;
using System.Linq;

public class PlayerManager_Inventory : MonoBehaviour
{
    public event Action<ItemData> OnItemDepleted;
    [SerializeField] Inventory_Slot[] inventorySlots;

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
}
