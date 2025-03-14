using System;
using UnityEngine;

public class Prop_Spike : MonoBehaviour
{
    [SerializeField] private int damagePerSecond;
    [SerializeField] private Vector2 damageHitboxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damageSmoothingRate = 2;

    private float timeLeftBeforeDoingDmg = 0;
    
    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, damageHitboxSize, 0f, playerLayer);

        if (colliders.Length > 0)
        {
            if (timeLeftBeforeDoingDmg > 0)
            {
                timeLeftBeforeDoingDmg -= Time.deltaTime;
            }
            else
            {
                timeLeftBeforeDoingDmg = 1f/damageSmoothingRate;
                PlayerReferences.Instance.health.DamagePlayer(damagePerSecond/damageSmoothingRate);   
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, damageHitboxSize);
    }
}
