using System;
using UnityEngine;
using TarodevController;

public class PlayerManager_Health : MonoBehaviour
{
    public event Action OnDamageTaken;
    [SerializeField] private int maxHealth = 100;
    internal int currentHealth { get; set; }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        PlayerReferences.Instance.ui.UpdateHealthBar(currentHealth/(float)maxHealth);
    }

    public void DamagePlayer(int damage)
    {
        if (currentHealth <= 0)
            return;
        
        currentHealth -= damage;
        PlayerReferences.Instance.ui.UpdateHealthBar(currentHealth/(float)maxHealth);
        OnDamageTaken?.Invoke();
        // CameraShakeManager.instance.ShakeCamera(0.2f);

        if (currentHealth <= 0)
        {
            //TODO - Die
            PlayerReferences.Instance.animator.PlayUnskippableAnimation(PlayerAnimator.DieKey,() =>
            {
                GameUIManager.instance.ShowHideDeathUI(true);
            });
        }
    }

    public void LoadSavedData(int health)
    {
        currentHealth = health;
        PlayerReferences.Instance.ui.UpdateHealthBar(currentHealth/(float)maxHealth, false);
    }
}
