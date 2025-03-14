using UnityEngine;

public class Prop_Resource : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Scriptable_Item itemToDrop;

    public void Damage(int damage)
    {
        health-= damage;
        
    }
}
