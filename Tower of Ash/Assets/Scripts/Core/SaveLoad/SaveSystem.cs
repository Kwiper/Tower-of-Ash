
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SavePlayer (PlayerData playerData, CombatData combatData, UpgradeData upgradeData){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.smd";
        FileStream stream = new FileStream(path,FileMode.Create);
        Debug.Log("I happen");
        SaveDataManager data = new SaveDataManager(playerData,combatData,upgradeData);

        formatter.Serialize(stream,data);
        stream.Close();

    }


    public static SaveDataManager LoadPlayer(){
        string path = Application.persistentDataPath + "/player.smd";  
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            SaveDataManager data = formatter.Deserialize(stream) as SaveDataManager;
            stream.Close();
            return data;  
        }
        else{
            Debug.LogError("Save file not found in "+ path + " . Lmao can't have shit in Detroit.");
            return null;
        }
    }
}
