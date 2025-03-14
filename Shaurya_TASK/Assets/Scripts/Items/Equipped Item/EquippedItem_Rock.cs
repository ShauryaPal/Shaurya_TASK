using Unity.Mathematics;
using UnityEngine;

public class EquippedItem_Rock : EquippedItem
{
    [SerializeField] private Rigidbody2D projectileRock;
    [SerializeField] private float forceMagnitude;
    [SerializeField] private Transform rockSpawnPosition;
    
    public override void Use()
    {
        base.Use();
        ThrowRock();
        PlayerReferences.Instance.inventory.OnItemUsedFromInventory(itemData);
        Destroy(gameObject);
    }

    private void ThrowRock()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        
        var rock = Instantiate(projectileRock, rockSpawnPosition.position, quaternion.identity);
        rock.transform.up = dir;
        rock.AddForce(dir * forceMagnitude, ForceMode2D.Impulse);

        Destroy(rock.gameObject, 20f);
    }
}
