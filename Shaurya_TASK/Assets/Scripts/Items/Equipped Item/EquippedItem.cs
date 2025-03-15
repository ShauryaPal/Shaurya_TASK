using UnityEngine;

public class EquippedItem : MonoBehaviour
{
    internal ItemData itemData;
    [SerializeField] private AudioClip sfx;

    public void SetupEquippedItem(ItemData itemData)
    {
        this.itemData = itemData;
    }
    
    public virtual void Use()
    {
        DataReferences.Instance.soundManager.PlaySfx(sfx);
    }
}
