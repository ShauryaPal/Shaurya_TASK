using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("UI References")] 
    [SerializeField] private GameObject itemInfoPanel;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameTxt;
    [SerializeField] private TMP_Text itemDescriptionTxt;
    [SerializeField] private TMP_Text itemQuantityTxt;

    internal ItemData itemData => new() { itemData = assignedItem, quantity = quantity};
    internal Scriptable_Item assignedItem { get; private set; }
    internal bool haveItem;
    private int quantity;
    
    public void AssignItem(ItemData item)
    {
        itemImage.color = new Color(1, 1, 1, 1);
        itemImage.sprite = item.itemData.itemSprite;
        itemNameTxt.text = item.itemData.itemName;
        itemDescriptionTxt.text = item.itemData.itemDescription;
        itemQuantityTxt.text = "x" + item.quantity;
        
        assignedItem = item.itemData;
        quantity = item.quantity;
        haveItem = true;
    }

    private void ResetSlot()
    {
        itemImage.color = new Color(1, 1, 1, 0);
        itemImage.sprite = null;
        itemNameTxt.text = "";
        itemDescriptionTxt.text = "";
        itemQuantityTxt.text = "";
        
        assignedItem = null;
        quantity = 0;
        haveItem = false;
    }

    public void UpdateItemQuantity(int extraItems)
    {
        quantity += extraItems;
        itemQuantityTxt.text = "x" + quantity;
    }
    
    public void Drop()
    {
        if (quantity > 0)
        {
            quantity--;
            //TODO - 
            //Drop Item In World
        }
        else
        {
            itemImage.color = new(1, 1, 1, 0);
            assignedItem = null;
            haveItem = false;
        }
    }
    
    public void Select()
    {
        // quantity--;
        //
        // if (quantity <= 0)
        // {
        //     itemImage.color = new(1, 1, 1, 0);
        //     assignedItem = null;
        //     hasItem = false;
        // }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(haveItem)
            itemInfoPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoPanel.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!haveItem) return;
        itemImage.raycastTarget = false;
        itemImage.transform.SetParent(transform.root);
        itemImage.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!haveItem) return;
        itemImage.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.transform.SetParent(transform);
        itemImage.raycastTarget = true;

    }

    public void OnDrop(PointerEventData eventData)
    {
        var droppedGO = eventData.pointerDrag;
        if (droppedGO.TryGetComponent(out Inventory_Slot otherSlot) && otherSlot != this)
        {
            var itemDataOfThisSlot = itemData;
            AssignItem(otherSlot.itemData);

            if (itemDataOfThisSlot.itemData != null)
                otherSlot.AssignItem(itemDataOfThisSlot);
            else
                otherSlot.ResetSlot();
        }
    }
}
