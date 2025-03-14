using UnityEngine;
using UnityEngine.UI;

public class PlayerManager_UI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private Image healthFillBar;
    [SerializeField] private KeyCode inventoryUIKey = KeyCode.Tab;

    private void Update()
    {
        if (Input.GetKeyDown(inventoryUIKey))
        {
            ToggleInventoryUI();
        }
    }

    public void ToggleInventoryUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    public void UpdateHealthBar(float value)
    {
        healthFillBar.fillAmount = value;
    }
}
