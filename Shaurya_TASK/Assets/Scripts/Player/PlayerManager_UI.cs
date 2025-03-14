using System;
using UnityEngine;

public class PlayerManager_UI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private KeyCode inventoryUIKey = KeyCode.Tab;

    private void Update()
    {
        if (Input.GetKeyDown(inventoryUIKey))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
