using UnityEngine;
using System.Linq;

public class PlayerManager_Inventory : MonoBehaviour
{
    [SerializeField] Inventory_Slot[] inventorySlots;

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
