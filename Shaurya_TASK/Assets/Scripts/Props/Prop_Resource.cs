using UnityEngine;

public class Prop_Resource : MonoBehaviour
{
    public ResourceType resourceType;
    [SerializeField] private int health;
    [SerializeField] private Scriptable_Item[] itemsToDrop;
    [SerializeField] private Vector2 minMaxXOffsetForDrops;
    [SerializeField] private Vector2 minMaxYOffsetForDrops;

    public void Damage(int damage)
    {
        health-= damage;

        if (health <= 0)
        {
            foreach (var item in itemsToDrop)
            {
                var droppedItem = Instantiate(DataReferences.Instance.droppedItemPrefab.gameObject, transform.position + new Vector3(Random.Range(minMaxXOffsetForDrops.x, minMaxXOffsetForDrops.y), Random.Range(minMaxYOffsetForDrops.x, minMaxYOffsetForDrops.y), 0), Quaternion.identity).GetComponent<Dropped_Item>();
                droppedItem.SetupDroppedItem(new ItemData {itemData = item, quantity = 1});
            }
            
            Destroy(gameObject);
        }
        
    }
}

public enum ResourceType
{
    Rock,
    Tree
}