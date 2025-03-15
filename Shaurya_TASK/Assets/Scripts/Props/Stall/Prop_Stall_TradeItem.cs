using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prop_Stall_TradeItem : MonoBehaviour
{
    [SerializeField] private Image tradedItemImg;
    [SerializeField] private TMP_Text tradedItemCountTxt;
    [SerializeField] private Image recievedItemImg;
    [SerializeField] private TMP_Text recievedItemCountTxt;
    private ItemData _tradedItem;
    private ItemData _recievedItem;

    public void SetTradedItem(Scriptable_Item tradedItem, Scriptable_Item recievedItem, int tradedItemCount, int recievedItemCount)
    {
        _tradedItem = new ItemData() { itemData = tradedItem, quantity = tradedItemCount };
        _recievedItem = new ItemData() { itemData = recievedItem, quantity = recievedItemCount };
        
        tradedItemImg.sprite = tradedItem.itemSprite;
        tradedItemCountTxt.text = "x" + tradedItemCount;
        recievedItemImg.sprite = recievedItem.itemSprite;
        recievedItemCountTxt.text = "x" + recievedItemCount;
    }

    public void OnClick()
    {
        if (PlayerReferences.Instance.inventory.HaveItem(_tradedItem.itemData, out Inventory_Slot itemSlot))
        {
            if (itemSlot.itemData.quantity >= _tradedItem.quantity)
            {
                itemSlot.UpdateItemQuantity(-_tradedItem.quantity);
                PlayerReferences.Instance.inventory.AddItemToInventory(_recievedItem);
            }
        }
    }
}
