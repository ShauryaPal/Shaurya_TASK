using UnityEngine;

public class Prop_Statue : Prop_Interactable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Scriptable_Item flowersItem;
    [SerializeField] private GameObject flowerGO;
    
    private const string ActivateStatueKey = "Statue Activate";

    public override void Use()
    {
        base.Use();

        if (PlayerReferences.Instance.inventory.HaveItem(flowersItem, out var slot))
        {
            slot.UpdateItemQuantity(-1);
            animator.Play(ActivateStatueKey);
            flowerGO.SetActive(true);
        }
    }
}
