using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SerializationManager
{
    public static void SaveData(object savedData, string fileName)
    {
        var formatter = new BinaryFormatter();
        var stream = new FileStream(fileName, FileMode.Create);
        formatter.Serialize(stream, savedData);
        stream.Close();
    }
    
    #region Player Saved Data

    public const string playerSaveDataFileName = "playerData.dat";
    
    public static SaveData_Player LoadPlayerData()
    {
        if (!File.Exists(playerSaveDataFileName)) return null;
        
        var formatter = new BinaryFormatter();
        var stream = new FileStream(playerSaveDataFileName, FileMode.Open);
        var playerSaveData = (SaveData_Player)formatter.Deserialize(stream);
        stream.Close();
        return playerSaveData;
    }
    
    #endregion

    #region Dropped Items Saved Data

    public const string droppedItemsSaveDataFileName = "droppedItemsData.dat";
    
    public static SaveData_DroppedItem[] LoadDroppedItemsData()
    {
        if (!File.Exists(droppedItemsSaveDataFileName)) return null;
        
        var formatter = new BinaryFormatter();
        var stream = new FileStream(droppedItemsSaveDataFileName, FileMode.Open);
        var saveData = (SaveData_DroppedItem[])formatter.Deserialize(stream);
        stream.Close();
        return saveData;
    }
    
    #endregion
    
    #region Interactable Props Saved Data

    public const string interactablePropsSaveDataFileName = "interactablePropsData.dat";
    
    public static SaveData_PropInteractable[] LoadInteractablePropsData()
    {
        if (!File.Exists(interactablePropsSaveDataFileName)) return null;
        
        var formatter = new BinaryFormatter();
        var stream = new FileStream(interactablePropsSaveDataFileName, FileMode.Open);
        var saveData = (SaveData_PropInteractable[])formatter.Deserialize(stream);
        stream.Close();
        return saveData;
    }
    
    #endregion

    #region Resource Props Saved Data

    public const string resourcePropsSaveDataFileName = "resourcePropsData.dat";
    
    public static SaveData_PropResources[] LoadResourcePropsData()
    {
        if (!File.Exists(resourcePropsSaveDataFileName)) return null;
        
        var formatter = new BinaryFormatter();
        var stream = new FileStream(resourcePropsSaveDataFileName, FileMode.Open);
        var saveData = (SaveData_PropResources[])formatter.Deserialize(stream);
        stream.Close();
        return saveData;
    }
    
    #endregion

    public static void DeleteAllSaveData()
    {
        if (File.Exists(playerSaveDataFileName)) File.Delete(playerSaveDataFileName);
        if (File.Exists(droppedItemsSaveDataFileName)) File.Delete(droppedItemsSaveDataFileName);
        if (File.Exists(interactablePropsSaveDataFileName)) File.Delete(interactablePropsSaveDataFileName);
        if (File.Exists(resourcePropsSaveDataFileName)) File.Delete(resourcePropsSaveDataFileName);
    }
    
}
