using System;
using UnityEngine;

public class Player_ItemPickier : MonoBehaviour
{
    [SerializeField] private PlayerManager_Inventory playerInventory;
    [SerializeField] private float pickupRange;
    [SerializeField] private LayerMask droppedItemLayer;
    [SerializeField] private KeyCode droppedItemPickupKey = KeyCode.E;

    private void Update()
    {
        CheckForDroppedItems();
    }

    private void CheckForDroppedItems()
    {
        var results = Physics2D.OverlapCircleAll(transform.position, pickupRange, droppedItemLayer);
        
        if (results.Length <= 0) return;
        if (!Input.GetKeyDown(droppedItemPickupKey)) return;
        
        if (results[0].TryGetComponent(out Dropped_Item droppedItem))
        {
            playerInventory.AddItemToInventory(droppedItem);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
