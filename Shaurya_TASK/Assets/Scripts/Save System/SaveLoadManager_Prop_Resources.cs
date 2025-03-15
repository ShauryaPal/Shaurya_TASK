using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveLoadManager_Prop_Resources : MonoBehaviour
{
    public void LoadResourceProps()
    {
        var data = SerializationManager.LoadResourcePropsData();

        foreach (var item in GameObject.FindGameObjectsWithTag(TagsManager.ResourceProps))
            Destroy(item);
        
        foreach (var item in data)
        {
            var position = new Vector3(item.position[0], item.position[1], item.position[2]);
            var rotation = Quaternion.Euler(item.eulerAngles[0], item.eulerAngles[1], item.eulerAngles[2]);
            var scale = new Vector3(item.scale[0], item.scale[1], item.scale[2]);
            
            var prop = Instantiate(DataReferences.Instance.GetPrefabFromResource((ResourceType)item.resourceType).gameObject, position, rotation).GetComponent<Prop_Resource>();
            prop.transform.localScale = scale;
            prop.transform.SetParent(DataReferences.Instance.propsHolderTransform);
            
            var itemsToDrop = item.scriptableItemsToDropIndexes.Select(index => DataReferences.Instance.GetScriptableItemFromIndex(index)).ToArray();
            var minmaxXOffset = new Vector2(item.minMaxXOffsets[0], item.minMaxXOffsets[1]);
            var minmaxYOffset = new Vector2(item.minMaxYOffsets[0], item.minMaxYOffsets[1]);
            
            prop.LoadSaveData((ResourceType) item.resourceType, item.health,  minmaxXOffset, minmaxYOffset, itemsToDrop);
        }
    }
    
    public void SaveResourcePropsData()
    {
        SerializationManager.SaveData(GetResourcePropsSaveData(), SerializationManager.resourcePropsSaveDataFileName);
    }
    
    private SaveData_PropResources[] GetResourcePropsSaveData()
    {
        List<SaveData_PropResources> saveData = new();
        
        foreach (var item in GameObject.FindGameObjectsWithTag(TagsManager.ResourceProps))
        {
            if (!item.TryGetComponent(out Prop_Resource prop)) continue;
            
            var scriptableItemsToDropIndexes = prop.itemsToDrop.Select(drop => DataReferences.Instance.GetIndexOfScriptableItem(drop)).ToArray();

            SaveData_PropResources propSaveData = new() {
                position = new[]{prop.transform.position.x, prop.transform.position.y, prop.transform.position.z},
                eulerAngles = new[]{prop.transform.localEulerAngles.x, prop.transform.localEulerAngles.y, prop.transform.localEulerAngles.z},
                scale = new[]{prop.transform.localScale.x, prop.transform.localScale.y, prop.transform.localScale.z},
                resourceType = (int)prop.resourceType,
                health = prop.health,
                scriptableItemsToDropIndexes = scriptableItemsToDropIndexes,
                minMaxXOffsets = new[]{prop.minMaxXOffsetForDrops.x, prop.minMaxXOffsetForDrops.y},
                minMaxYOffsets = new[]{prop.minMaxYOffsetForDrops.x, prop.minMaxYOffsetForDrops.y}
            };
                
            saveData.Add(propSaveData);
        }
        
        return saveData.ToArray();
    }
}


[Serializable]
public class SaveData_PropResources
{
    public float[] position;
    public float[] eulerAngles;
    public float[] scale;
    public int resourceType;
    public int health;
    public int[] scriptableItemsToDropIndexes;
    public float[] minMaxXOffsets;
    public float[] minMaxYOffsets;
}