using UnityEngine;

[CreateAssetMenu]
public class Scriptable_Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public string itemDescription;
    public EquippedItem equippablePrefab;
}
