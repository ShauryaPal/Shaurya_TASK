using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerManager_UI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private Image healthFillBar;
    [SerializeField] private float healthBarFillSpeed = 1f;
    [SerializeField] private KeyCode inventoryUIKey = KeyCode.Tab;

    private void Update()
    {
        if (Input.GetKeyDown(inventoryUIKey))
        {
            ToggleInventoryUI();
        }
    }

    public void ToggleInventoryUI(bool forceOn = false, bool forceOff = false)
    {
        if (forceOn)
        {
            inventoryUI.SetActive(true);
            return;
        }

        if (forceOff)
        {
            inventoryUI.SetActive(false);
            return;
        }
        
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    public void UpdateHealthBar(float value, bool animate = true)
    {
        if(animate)
            healthFillBar.DOFillAmount(value, healthBarFillSpeed);
        else
            healthFillBar.fillAmount = value;
    }
}
