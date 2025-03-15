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

        foreach (var item in results)
        {
            foreach (var prop in item.GetComponents<Prop_Interactable>())
            {
                if (!prop.interacted)
                {
                    if (Input.GetKeyDown(propInteractKey))
                    {
                        prop.Interact();
                        return;
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
