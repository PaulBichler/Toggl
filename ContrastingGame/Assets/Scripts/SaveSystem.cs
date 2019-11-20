using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game;
using UnityEngine;

[Serializable]
public sealed class SaveGame
{
    public Dictionary<string, bool> playerProgress;
    public SaveGame()
    {
        playerProgress = GameState.LevelUnlockStatus;
    }
}

public static class SaveSystem
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.save";
        FileStream stream = new FileStream(path,FileMode.Create);
        var saveGame = new SaveGame();
        formatter.Serialize(stream,saveGame);
    }

    public static SaveGame Load()
    {
        string path = Application.persistentDataPath + "/game.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            SaveGame data = formatter.Deserialize(stream) as SaveGame;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("No Progress found");
            return new SaveGame();
        }
    }
}
