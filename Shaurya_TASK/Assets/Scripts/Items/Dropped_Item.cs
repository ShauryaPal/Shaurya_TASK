using UnityEngine;

public class Dropped_Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    internal Scriptable_Item itemData { get; private set; }
    internal int quantity { get; private set; }

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

    public void SetupDroppedItem(ItemData data)
    {
        itemData = data.itemData;
        spriteRenderer.sprite = data.itemData.itemSprite;
        quantity = data.quantity;
    }

    public ItemData GetItemData()
    {
        ItemData data = new()
        {
            itemData = itemData,
            quantity = quantity
        };
        
        return data;
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
