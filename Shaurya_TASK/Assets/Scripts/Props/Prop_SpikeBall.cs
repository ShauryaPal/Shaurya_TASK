using System;
using UnityEngine;

public class Prop_SpikeBall : MonoBehaviour
{
    [SerializeField] private int damage;
    private float timeToWaitBeforeDoingDmg;
    
    private void Start()
    {
        Destroy(gameObject, 10);
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
