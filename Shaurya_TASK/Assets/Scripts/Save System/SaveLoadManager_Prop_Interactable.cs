using System;
using System.Linq;
using UnityEngine;

public class SaveLoadManager_Prop_Interactable : MonoBehaviour
{
    [SerializeField] private Prop_Interactable[] propsToSave;
    
    public void LoadInteractablePropsData()
    {
        var savedData = SerializationManager.LoadInteractablePropsData();

        if (savedData.Length != propsToSave.Length)
        {
            Debug.LogError("SaveLoadManager Prop Interactable: Saved Data Corroupted.");
            return;
        }
        
        for(var i = 0; i < propsToSave.Length; i++)
            propsToSave[i].LoadSavedData(savedData[i].interacted);
    }
    
    public void SaveInteractablePropsData()
    {
        SerializationManager.SaveData(GetInteractablePropsSaveData(), SerializationManager.interactablePropsSaveDataFileName);
    }
    
    private SaveData_PropInteractable[] GetInteractablePropsSaveData()
    {
        return propsToSave.Select(item => new SaveData_PropInteractable() { interacted = item.interacted }).ToArray();
    }
}

[Serializable]
public class SaveData_PropInteractable
{
    public bool interacted;
}
