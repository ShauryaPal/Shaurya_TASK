using TarodevController;

public class EquippedItem_Wine: EquippedItem
{
    public override void Use()
    {
        base.Use();
        PlayerReferences.Instance.animator.PlayUnskippableAnimation(PlayerAnimator.EatKey, () =>
        {
            PlayerReferences.Instance.inventory.OnItemUsedFromInventory(itemData);
            Destroy(gameObject);
        });
    }
}
