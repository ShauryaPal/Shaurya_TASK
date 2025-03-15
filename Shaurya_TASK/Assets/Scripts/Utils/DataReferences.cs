using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataReferences : MonoBehaviour
{
    public static DataReferences Instance;
    
    public Transform playerTransform;
    public Dropped_Item droppedItemPrefab;
    public Prop_Stall_UIManager propStallUIManager;
    public DialogueSystem_UIManager dialogueSystem_UIManager;
    public Transform propsHolderTransform;
    public ResourcesSprites[] resourcesSprites;
    public ResourcesPrefabs[] resourcesPrefabs;
    
    [SerializeField] List<Scriptable_Item> scriptableItems;
    
    
    private void Awake()
    {
        Instance = this;
    }

    public int GetIndexOfScriptableItem(Scriptable_Item scriptableItem)
    {
        return  scriptableItems.FindIndex(item => item == scriptableItem);
    }
    
    public Scriptable_Item GetScriptableItemFromIndex(int index)
    {
        return  scriptableItems[index];
    }

    public Sprite GetRandomSpriteForResource(ResourceType resourceType)
    {
        var sprites = resourcesSprites.First(item => item.resourceType == resourceType).sprites;
        return sprites[Random.Range(0, sprites.Length)];
    }
    
    public Prop_Resource GetPrefabFromResource(ResourceType resourceType)
    {
        return resourcesPrefabs.First(i => i.resourceType == resourceType).prefab;
    }
}

