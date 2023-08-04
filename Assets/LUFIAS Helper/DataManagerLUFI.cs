using System.IO;
using UnityEditor;
using UnityEngine;

public static class DataManagerLUFI
{
    #region Save
    public static void SaveToJson<T>(string keyName, T data)
    {
        string json = JsonUtility.ToJson(data, true);
        string path = Application.dataPath + "/SavedFiles";

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        File.WriteAllText(path + $"/{keyName}.uwu", json);

        Debug.Log("File Saved as: " + keyName + ".uwu");
        Debug.Log(json);
    }
    #endregion

    #region Load
    /// <summary>
    /// Returns false if file does not exists, or true with the file
    /// </summary>
    /// <typeparam name="T">Type of object which the JSON was complied</typeparam>
    /// <param name="keyName">Name of the JSON file</param>
    /// <returns></returns>
    public static (T,bool) LoadFromJson<T>(string keyName)
    {
        string path = Application.dataPath + $"/SavedFiles/{keyName}.uwu";
        if (!File.Exists(path))
        {
            Debug.Log("File does not exist. Path: " + path);
            return (default,false);
        }

        string json = File.ReadAllText(path);
        return (JsonUtility.FromJson<T>(json),true); ;
    }
    #endregion

    #region Helper
#if UNITY_EDITOR
    [MenuItem("LUFiAS/DATA/Show All Data")]
#endif
    [ButtonLUFI]
    static void ShowAllData()
    {
        string path = Path.GetDirectoryName(Application.dataPath + "/SavedFiles/");

        string[] files = Directory.GetFiles(path);

        if (files.Length == 0) return;

        foreach (string file in files)
        {
            Debug.Log(file);
        }
    }

#if UNITY_EDITOR
    [MenuItem("LUFiAS/DATA/Erase All Data")]
#endif
    [ButtonLUFI]
    static void DeleteAllKey()
    {
        string path = Path.GetDirectoryName(Application.dataPath + "/SavedFiles/");

        string[] files = Directory.GetFiles(path);

        if (files.Length == 0) return;

        foreach (string file in files)
        {
            File.Delete(file);
        }
        Debug.Log("File(s) deleted");
    }
    #endregion
}