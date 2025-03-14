using UnityEngine;

public class PlayerManager_Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
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
}
