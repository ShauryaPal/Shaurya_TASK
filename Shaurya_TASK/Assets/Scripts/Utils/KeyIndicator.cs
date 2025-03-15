using System;
using UnityEngine;
using UnityEngine.UI;

public class KeyIndicator : MonoBehaviour
{
    [SerializeField] private float scanRadius = 2;
    [SerializeField] private Sprite eKey;
    [SerializeField] private Sprite tabKey;
    [SerializeField] private Image keyIndicatorImg;
    [SerializeField] private LayerMask itemLayers;
    private KeyCode currentKeyIndicator = KeyCode.None;
    private bool newItemAddedInInventory = false;
    
    private void Start()
    {
        PlayerReferences.Instance.inventory.OnItemAdded += () => newItemAddedInInventory = true;
    }

    private void Update()
    {
        var results = Physics2D.OverlapCircleAll(transform.position, scanRadius, itemLayers);

        if (results.Length >= 1)
        { 
            ShowKeyToUse(KeyCode.E);
        }
        else if(newItemAddedInInventory)
        {
            ShowKeyToUse(KeyCode.Tab);
        }
        else
        {
            currentKeyIndicator = KeyCode.None;
            keyIndicatorImg.enabled = false;
        }
        
        if (Input.GetKeyDown(currentKeyIndicator))
        {
            if(currentKeyIndicator == KeyCode.Tab)
                newItemAddedInInventory = false;
            
            currentKeyIndicator = KeyCode.None;
            keyIndicatorImg.enabled = false;
        }
    }

    public void ShowKeyToUse(KeyCode keyCode)
    {
        currentKeyIndicator = keyCode;
        keyIndicatorImg.enabled = true;

        if(keyCode == KeyCode.E)
            keyIndicatorImg.sprite = eKey;
        else if (keyCode == KeyCode.Tab)
            keyIndicatorImg.sprite = tabKey;
    }
}
