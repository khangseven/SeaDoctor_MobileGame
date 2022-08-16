using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad 
{
    public static void Save(Player player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(player);

        binaryFormatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public static void Save2(PlayerData player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        binaryFormatter.Serialize(fileStream, player);
        fileStream.Close();
    }

    public static PlayerData Load()
    {
        string path = Application.persistentDataPath + "/player.dat";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            PlayerData data= binaryFormatter.Deserialize(fileStream) as PlayerData;
            return data;
        }
        else
        {
            //Save2(new PlayerData());
            return null;
        }
    }
}
