using System;
using UnityEngine;

public class DataReferences : MonoBehaviour
{
    public static DataReferences Instance;

    public Transform playerTransform;
    public Dropped_Item droppedItemPrefab;
    
    private void Awake()
    {
        Instance = this;
    }
}
