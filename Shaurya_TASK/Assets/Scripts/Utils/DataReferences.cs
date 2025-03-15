using System;
using UnityEngine;

public class DataReferences : MonoBehaviour
{
    public static DataReferences Instance;

    public Transform playerTransform;
    public Dropped_Item droppedItemPrefab;
    public Prop_Stall_UIManager propStallUIManager;
    
    private void Awake()
    {
        Instance = this;
    }
}
