using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSaving : MonoBehaviour
{
    private string path = string.Empty;
    private string persistentpath = string.Empty;

    private void setPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentpath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SavaData.json";
    }

    private void Start()
    {
        setPaths();
        saveData(new PlayerData());
    }

    public void saveData(PlayerData playerData)
    {
        string savePath = path;
        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(playerData);
        Debug.Log("json");

        using StreamWriter writer= new StreamWriter(savePath);
        writer.Write(json);
    }

    public void loadData()
    {
        using StreamReader reader= new StreamReader(path);
        string json = reader.ReadToEnd();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log(data.ToString());
    }
}
