using UnityEngine;

public class Prop_Chest : Prop_Interactable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Scriptable_Item[] rewardItems;
    [SerializeField] private Vector2 minMaxXOffsetForDrops;
    [SerializeField] private Vector2 minMaxYOffsetForDrops;
    
    private const string IdleChestKey = "IdleChest";
    private const string OpenChestKey = "OpenChest";
    private const string OpenedChestKey = "OpenedChest";
    
    protected override void Use()
    {
        base.Use();
        animator.Play(OpenChestKey);
        Invoke(nameof(DropLoot), 1f);
    }

    protected override void OnLoadSavedData(bool _interacted)
    {
        base.OnLoadSavedData(_interacted);
        animator.Play(_interacted ? OpenedChestKey : IdleChestKey);
    }
    
    private void DropLoot()
    {
        foreach (var item in rewardItems)
        {
            var droppedItem = Instantiate(DataReferences.Instance.droppedItemPrefab.gameObject, transform.position + new Vector3(Random.Range(minMaxXOffsetForDrops.x, minMaxXOffsetForDrops.y), Random.Range(minMaxYOffsetForDrops.x, minMaxYOffsetForDrops.y), 0), Quaternion.identity).GetComponent<Dropped_Item>();
            droppedItem.SetupDroppedItem(new ItemData {itemData = item, quantity = 1});
        }
    }

    
}
