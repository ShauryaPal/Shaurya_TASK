using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Inventory_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("UI References")] 
    [SerializeField] private GameObject itemInfoPanel;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameTxt;
    [SerializeField] private TMP_Text itemDescriptionTxt;
    [SerializeField] private TMP_Text itemQuantityTxt;
    [SerializeField] private GameObject dropButton;

    private ItemData itemData => new() { itemData = assignedItem, quantity = quantity};
    internal Scriptable_Item assignedItem { get; private set; }
    internal bool haveItem;
    private int quantity;

    #region Inventory Slot Updation
    
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
        dropButton.SetActive(true);
    }

    public void UpdateItemQuantity(int extraItems)
    {
        quantity += extraItems;
        itemQuantityTxt.text = "x" + quantity;

        if (quantity <= 0)
            ResetSlot();
    }
    
    private void ResetSlot()
    {
        PlayerReferences.Instance.inventory.OnItemDepletedFromInventory(itemData);
        
        itemImage.color = new Color(1, 1, 1, 0);
        itemImage.sprite = null;
        itemNameTxt.text = "";
        itemDescriptionTxt.text = "";
        itemQuantityTxt.text = "";
        
        assignedItem = null;
        quantity = 0;
        haveItem = false;
        dropButton.SetActive(false);
    }
    
    #endregion

    #region Button OnClick Functions
    
    public void OnClick_Drop()
    {
        if (quantity > 0)
        {
            Scriptable_Item _assignedItem = assignedItem;
            UpdateItemQuantity(-1);
            Vector2 playerPos = DataReferences.Instance.playerTransform.position;
            var spawnLocation = playerPos + new Vector2(Random.Range(-2f, 2f), 1);
            var droppedItem = Instantiate(DataReferences.Instance.droppedItemPrefab.gameObject, spawnLocation, Quaternion.identity);
            droppedItem.GetComponent<Dropped_Item>().SetupDroppedItem(new ItemData { itemData = _assignedItem, quantity = 1});
        }
    }
    
    public void OnClick_Equip()
    {
       PlayerReferences.Instance.itemInteracter.SelectItem(itemData);
    }

    #endregion

    #region Handling Dragging And Info Box

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

    #endregion
    
}
