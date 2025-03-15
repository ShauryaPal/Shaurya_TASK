using System;
using UnityEngine;

public class PlayerManager_ItemInteracter : MonoBehaviour
{
    [SerializeField] private Transform playerEquippedItemHolderTransform;
    internal EquippedItem equippedItemData;

    private void Start()
    {
        PlayerReferences.Instance.inventory.OnItemDepleted += InventoryOnItemDepleted;
    }

    private void Update()
    {
        if(equippedItemData == null) return;
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        equippedItemData.Use();
    }

    private void InventoryOnItemDepleted(ItemData item)
    {
        if(equippedItemData == null || equippedItemData.itemData.itemData == null) return;
        
        if (item.itemData == equippedItemData.itemData.itemData)
            UnequipItem();
    }

    public void SelectItem(ItemData itemData)
    {
        if(itemData.itemData== null) return;
        
        if (playerEquippedItemHolderTransform.childCount > 0)
            foreach (Transform child in playerEquippedItemHolderTransform)
                Destroy(child.gameObject);
        
        equippedItemData = Instantiate(itemData.itemData.equippablePrefab.gameObject, playerEquippedItemHolderTransform).GetComponent<EquippedItem>();
        equippedItemData.SetupEquippedItem(itemData);
        
        PlayerReferences.Instance.ui.ToggleInventoryUI();
    }

    public void UnequipItem()
    {
        if (playerEquippedItemHolderTransform.childCount > 0)
            foreach (Transform child in playerEquippedItemHolderTransform)
                Destroy(child.gameObject);
        
        equippedItemData = null;
    }
}
