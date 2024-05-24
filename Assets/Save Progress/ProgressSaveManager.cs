using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using UnityEngine;
using System;

public enum SaveType
{
    AutoSave,
    ManualSave,
    BasicSave
}

public class ProgressSaveManager
{
    private ProgressData TemporalSave;

    public void SaveProgress(SaveType saveType, Player player)
    {
        var fileName = saveType.ToString() + ".json";
        var playerData = new ProgressData(player);
        var playerDataSerialized = JsonSerializer.Serialize(playerData);
        File.WriteAllText(fileName, playerDataSerialized);
    }

    public bool TryReadFromSave(SaveType saveType, out ProgressData playerData)
    {
        var fileName = saveType.ToString() + ".json";
        if (!File.Exists(fileName)) 
        {
            playerData = null;
            return false;
        }
        var playerDataSerialized = File.ReadAllText(fileName);
        playerData = JsonSerializer.Deserialize<ProgressData>(playerDataSerialized);
        return true;
    }
    
}
