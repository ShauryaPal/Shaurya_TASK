using UnityEngine;

public class PlayerManager_PropInteracter : MonoBehaviour
{
    [SerializeField] private float interactRange;
    [SerializeField] private LayerMask propItemLayer;
    [SerializeField] private KeyCode propInteractKey = KeyCode.E;
    
    private void Update()
    {
        CheckForDroppedItems();
    }

    private void CheckForDroppedItems()
    {
        var results = Physics2D.OverlapCircleAll(transform.position, interactRange, propItemLayer);
        
        if (results.Length <= 0) return;
        if (!Input.GetKeyDown(propInteractKey)) return;
        
        if (results[0].TryGetComponent(out Prop_Interactable prop))
            prop.Interact();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
