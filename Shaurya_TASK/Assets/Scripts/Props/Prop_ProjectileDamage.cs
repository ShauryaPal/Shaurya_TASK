using System;
using UnityEngine;

public class Prop_ProjectileDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeToDestroy = 10;
    [SerializeField] private bool destroyAfterFewSeconds;
    
    private float timeToWaitBeforeDoingDmg;
    
    private void Start()
    {
        if(destroyAfterFewSeconds)
            Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        timeToWaitBeforeDoingDmg -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && timeToWaitBeforeDoingDmg <= 0)
        {
            PlayerReferences.Instance.health.DamagePlayer(damage);
            timeToWaitBeforeDoingDmg = 0.5f;
        }
    }
}
