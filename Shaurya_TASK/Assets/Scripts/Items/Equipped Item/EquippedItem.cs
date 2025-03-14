using UnityEngine;

public class EquippedItem : MonoBehaviour
{
    internal ItemData itemData;

    public void SetupEquippedItem(ItemData itemData)
    {
        this.itemData = itemData;
    }
    
    public virtual void Use()
    {
        
    }
}
