using UnityEngine;

public class Prop_AuraRock : Prop_Interactable
{
    [SerializeField] private Prop_Statue statue;
    [SerializeField] private Scriptable_Item auraItem;
    [SerializeField] private GameObject auraGO;

    protected override void Use()
    {
        if(!statue.interacted) return;
        if (!PlayerReferences.Instance.inventory.HaveItem(auraItem, out var slot)) return;
        if (PlayerReferences.Instance.itemInteracter.equippedItemData != null &&PlayerReferences.Instance.itemInteracter.equippedItemData.itemData.itemData != auraItem) return;
        base.Use();
        slot.UpdateItemQuantity(-1);
        auraGO.SetActive(true);
        GameUIManager.instance.ShowHideGameOverUI(true);
    }
}
