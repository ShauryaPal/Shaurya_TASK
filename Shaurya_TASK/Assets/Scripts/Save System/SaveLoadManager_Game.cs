using System;
using UnityEngine;

public class SaveLoadManager_Game : MonoBehaviour
{
    [SerializeField] private SaveLoadManager_Player player;
    [SerializeField] private SaveLoadManager_DroppedItems droppedItems;
    [SerializeField] private SaveLoadManager_Prop_Interactable propInteractable;
    [SerializeField] private SaveLoadManager_Prop_Resources resources;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            LoadGame();
        }
    }


    public void SaveGame()
    {
        player.SavePlayerData();
        droppedItems.SaveDroppedItemsData();
        propInteractable.SaveInteractablePropsData();
        resources.SaveResourcePropsData();
    }

    public void LoadGame()
    {
        player.LoadPlayerData();
        droppedItems.LoadDroppedItemsData();
        propInteractable.LoadInteractablePropsData();
        resources.LoadResourceProps();
    }
}
