using System;
using UnityEngine;

public class Prop_Stall : Prop_Interactable
{
    [SerializeField] private GameObject stallUIMenu;
    [SerializeField] private Prop_Stall_Items[] stallItems;
    
    protected override void Use()
    {
        DataReferences.Instance.propStallUIManager.SetupStallUI(stallItems);
        stallUIMenu.SetActive(!stallUIMenu.activeSelf);    
        
        if(stallUIMenu.activeSelf)
            PlayerReferences.Instance.ui.ToggleInventoryUI(true);
        else
            PlayerReferences.Instance.ui.ToggleInventoryUI(false, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            stallUIMenu.SetActive(false);
            PlayerReferences.Instance.ui.ToggleInventoryUI(false, true);
        }
    }
}

[Serializable]
public struct Prop_Stall_Items
{
    public Scriptable_Item tradedItem;
    public Scriptable_Item recievedItem;
    public int tradedItemCount;
    public int recievedItemCount;
}
