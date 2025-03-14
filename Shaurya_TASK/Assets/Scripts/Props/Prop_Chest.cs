using UnityEngine;

public class Prop_Chest : Prop_Interactable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Scriptable_Item[] rewardItems;
    [SerializeField] private Vector2 minMaxXOffsetForDrops;
    [SerializeField] private Vector2 minMaxYOffsetForDrops;
    
    private const string OpenChestKey = "OpenChest";
    private bool opened = false;
    
    public override void Use()
    {
        if (opened) return;
        opened = true;
        
        animator.Play(OpenChestKey);
        Invoke(nameof(DropLoot), 1f);
        
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
