using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager_DroppedItems : MonoBehaviour
{
    [SerializeField] private Dropped_Item droppedItemPrefab;
    
    public void LoadDroppedItemsData()
    {
        var savedData = SerializationManager.LoadDroppedItemsData();

        foreach (var item in GameObject.FindGameObjectsWithTag(TagsManager.DroppedItem))
            Destroy(item);

        foreach (var item in savedData)
        {
            Vector3 pos = new(item.position[0], item.position[1], item.position[2]);
            var droppedItem = Instantiate(droppedItemPrefab, pos, Quaternion.identity);
            droppedItem.SetupDroppedItem(new ItemData() {itemData = DataReferences.Instance.GetScriptableItemFromIndex(item.scriptableItemIndex), quantity = item.itemQuantity});
        }
    }

    public void SaveDroppedItemsData()
    {
        SerializationManager.SaveData(GetDroppedItemsSaveData(), SerializationManager.droppedItemsSaveDataFileName);
    }
    
    private static SaveData_DroppedItem[] GetDroppedItemsSaveData()
    {
        List<SaveData_DroppedItem> savedata_droppedItems = new();

        foreach (var item in GameObject.FindGameObjectsWithTag(TagsManager.DroppedItem))
        {
            if (!item.TryGetComponent(out Dropped_Item droppedItem)) continue;
            
            var data = new SaveData_DroppedItem()
            {
                position = new[] {item.transform.position.x, item.transform.position.y, item.transform.position.z},
                scriptableItemIndex = DataReferences.Instance.GetIndexOfScriptableItem(droppedItem.itemData),
                itemQuantity = droppedItem.quantity
            };
                
            savedata_droppedItems.Add(data);
        }

        return savedata_droppedItems.ToArray();
    }
}

[Serializable]
public class SaveData_DroppedItem
{
    public float[] position;
    public int scriptableItemIndex;
    public int itemQuantity;
}
