using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager_Game : MonoBehaviour
{
    public static SaveLoadManager_Game instance;
    
    [SerializeField] private SaveLoadManager_Player player;
    [SerializeField] private SaveLoadManager_DroppedItems droppedItems;
    [SerializeField] private SaveLoadManager_Prop_Interactable propInteractable;
    [SerializeField] private SaveLoadManager_Prop_Resources resources;

    private void Awake()
    {
        instance = this;
    }

    public void DeleteSaveData()
    {
        SerializationManager.DeleteAllSaveData();
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
        try
        {
            player.LoadPlayerData();
            droppedItems.LoadDroppedItemsData();
            propInteractable.LoadInteractablePropsData();
            resources.LoadResourceProps();
        }
        catch (Exception e)
        {
            Debug.Log("Unable To Load Game" + e.Message);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
}
