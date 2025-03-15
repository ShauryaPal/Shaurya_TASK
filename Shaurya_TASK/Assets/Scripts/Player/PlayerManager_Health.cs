using UnityEngine;

public class PlayerManager_Health : MonoBehaviour
{
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
        currentHealth -= damage;
        PlayerReferences.Instance.ui.UpdateHealthBar(currentHealth/(float)maxHealth);

        if (currentHealth <= 0)
        {
            //TODO - Die
        }
    }

    public void LoadSavedData(int health)
    {
        currentHealth = health;
        PlayerReferences.Instance.ui.UpdateHealthBar(currentHealth/(float)maxHealth, false);
    }
}
