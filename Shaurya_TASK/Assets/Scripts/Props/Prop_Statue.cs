using UnityEngine;

public class Prop_Statue : Prop_Interactable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Scriptable_Item flowersItem;
    [SerializeField] private GameObject flowerGO;
    
    private const string IdleStatueKey = "Statue Idle";
    private const string ActivateStatueKey = "Statue Activate";
    private const string ActivatedStatueKey = "Statue Activated";

    protected override void Use()
    {
        if (!PlayerReferences.Instance.inventory.HaveItem(flowersItem, out var slot)) return;
        base.Use();
        slot.UpdateItemQuantity(-1);
        animator.Play(ActivateStatueKey);
        flowerGO.SetActive(true);
    }

    protected override void OnLoadSavedData(bool _interacted)
    {
        base.OnLoadSavedData(_interacted);
        animator.Play(_interacted ? ActivatedStatueKey : IdleStatueKey);
    }
}
