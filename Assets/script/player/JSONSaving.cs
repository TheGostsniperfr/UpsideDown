using System.IO;
using UnityEngine;

public static class JSONSaving
{
    private static string path = string.Empty;

    private static void setPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    public static PlayerData loadSettings()
    {
        setPaths();

        //check if file exist

        if (!System.IO.File.Exists(path))
        {
            saveData(new PlayerData());
        }

        return loadData();
    }

    public static void saveData(PlayerData playerData)
    {
        setPaths();

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }

        string savePath = path;
        Debug.Log("Saving Data at " + savePath);


        string json = JsonUtility.ToJson(playerData);
        Debug.Log("json");

        StreamWriter writer = new StreamWriter(savePath, false);
        writer.Write(json);

        writer.Close();
    }

    public static PlayerData loadData()
    {
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log(data.ToString());

        reader.Close();

        return data;
    }
}
