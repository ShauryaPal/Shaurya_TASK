using System;
using UnityEngine;

public class Dropped_Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Scriptable_Item _itemData;
    [SerializeField] private int quantity;

    private Vector3 originalPosition;
    private Vector3 tempPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        AnimateDroppedItem();
    }

    public void SetupDroppedItem(ItemData itemData)
    {
        _itemData = itemData.itemData;
        spriteRenderer.sprite = itemData.itemData.itemSprite;
        quantity = itemData.quantity;
    }

    public ItemData GetItemData()
    {
        ItemData itemData = new()
        {
            itemData = _itemData,
            quantity = quantity
        };
        
        return itemData;
    }

    public void OnItemPickedUp()
    {
        Destroy(gameObject);
    }

    #region  Dropped Item Animation

    private void AnimateDroppedItem()
    {
        tempPosition = originalPosition;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 1) * 0.15f;
        transform.position = tempPosition;
    }


    #endregion
}
