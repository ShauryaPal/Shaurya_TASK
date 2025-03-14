using TarodevController;
using UnityEngine;

public class EquippedItem_Axe : EquippedItem
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask resourcesPropLayer;
    
    public override void Use()
    {
        if(PlayerReferences.Instance.animator.isPlayingUnskippableAnimation) return;
        
        base.Use();
        PlayerReferences.Instance.animator.PlayUnskippableAnimation(PlayerAnimator.AttackKey, () => {});

        var resources = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, resourcesPropLayer);

        if (resources.Length > 0 && resources[0].TryGetComponent(out Prop_Resource resource))
            resource.Damage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
