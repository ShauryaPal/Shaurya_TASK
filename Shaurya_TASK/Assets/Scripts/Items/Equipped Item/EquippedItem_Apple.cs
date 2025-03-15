using TarodevController;
using UnityEngine;

public class EquippedItem_Apple : EquippedItem
{
    [SerializeField] private int healAmount;
    
    public override void Use()
    {
        base.Use();
        PlayerReferences.Instance.animator.PlayUnskippableAnimation(PlayerAnimator.EatKey, () =>
        {
            PlayerReferences.Instance.health.HealPlayer(healAmount);
            PlayerReferences.Instance.inventory.OnItemUsedFromInventory(itemData);
            Destroy(gameObject);
        });
    }
}
