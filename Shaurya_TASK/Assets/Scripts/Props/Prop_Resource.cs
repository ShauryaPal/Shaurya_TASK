using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Prop_Resource : MonoBehaviour
{
    public ResourceType resourceType;
    public int health;
    public Scriptable_Item[] itemsToDrop;
    public Vector2 minMaxXOffsetForDrops;
    public Vector2 minMaxYOffsetForDrops;
    
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PolygonCollider2D polygonCollider2D;

    private void Awake()
    {
        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (TryGetComponent(out PolygonCollider2D _polygonCollider2D))
            polygonCollider2D = _polygonCollider2D;
    }

    private void Start()
    {
        spriteRenderer.sprite = DataReferences.Instance.GetRandomSpriteForResource(resourceType);
        
        if(polygonCollider2D != null)
            polygonCollider2D.UpdateShapeToSprite(spriteRenderer.sprite);
    }

    public void Damage(int damage)
    {
        health-= damage;

        if (health > 0) return;
        
        foreach (var item in itemsToDrop)
        {
            var droppedItem = Instantiate(DataReferences.Instance.droppedItemPrefab.gameObject, transform.position + new Vector3(Random.Range(minMaxXOffsetForDrops.x, minMaxXOffsetForDrops.y), Random.Range(minMaxYOffsetForDrops.x, minMaxYOffsetForDrops.y), 0), Quaternion.identity).GetComponent<Dropped_Item>();
            droppedItem.SetupDroppedItem(new ItemData {itemData = item, quantity = 1});
        }
            
        Destroy(gameObject);

    }

    public void LoadSaveData(ResourceType type, int _health, Vector2 _minMaxXOffsetForDrops, Vector2 _minMaxYOffsetForDrops, Scriptable_Item[] _itemsToDrop)
    {
        resourceType = type;
        
        spriteRenderer.sprite = DataReferences.Instance.GetRandomSpriteForResource(type);
        
        if(polygonCollider2D != null)
            polygonCollider2D.UpdateShapeToSprite(spriteRenderer.sprite);
        
        health = _health;
        minMaxXOffsetForDrops = _minMaxXOffsetForDrops;
        minMaxYOffsetForDrops = _minMaxYOffsetForDrops;
        itemsToDrop = _itemsToDrop;
    }
    
}

[Serializable]
public struct ResourcesSprites
{
    public ResourceType resourceType;
    public Sprite[] sprites;
}

[Serializable]
public struct ResourcesPrefabs
{
    public ResourceType resourceType;
    public Prop_Resource prefab;
}

public enum ResourceType
{
    Rock,
    Tree,
    Boxes,
    Barrel,
    Pots,
    Wheat,
    WheatBag,
    SmallRock
}