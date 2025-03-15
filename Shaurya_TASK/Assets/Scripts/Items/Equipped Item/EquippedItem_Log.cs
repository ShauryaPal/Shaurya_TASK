using UnityEngine;

public class EquippedItem_Log : EquippedItem
{
    [SerializeField] private Rigidbody2D logPrefab;
    [SerializeField] private Transform logSpawnPosition;
    
    public override void Use()
    {
        base.Use();
        Instantiate(logPrefab.gameObject, logSpawnPosition.position, Quaternion.identity);
        PlayerReferences.Instance.inventory.OnItemUsedFromInventory(itemData);
        Destroy(gameObject);
    }

}
