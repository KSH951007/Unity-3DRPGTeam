using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class DataManager : Singleton<DataManager>
{

    private string path;
    private List<ISavable> saveData;

    private void Awake()
    {
        if (!Init())
            return;



        path = Application.persistentDataPath + "/";
        if (LoadData<int>("ASD", out int index))
            Debug.Log("¾øÀ½");

        saveData = new List<ISavable>();
        Debug.Log(path);
        SceneManager.sceneUnloaded += (_) => { ChangeSceneSaveData(); };
    }
    public void SaveData(object saveData, string fileName, string forlderName = "")
    {
        string data = JsonUtility.ToJson(saveData);


        if (forlderName != "")
        {
            if (!Directory.Exists(path + forlderName))
            {
                Directory.CreateDirectory(path + forlderName);
            }
        }
        Debug.Log(data);
        File.WriteAllText(path + forlderName + fileName, data);
    }
    public bool LoadData<T>(string fileName, out T loadData)
    {
        if (!File.Exists(path + fileName))
        {
            loadData = default(T);
            return false;
        }


        string data = File.ReadAllText(path + fileName);
        Debug.Log(data);
        
        loadData = JsonUtility.FromJson<T>(data);
        return true;
    }
    
    public int GetFileCount(string folderName)
    {
        if (!Directory.Exists(path + folderName))
            return 0;

        return Directory.GetFiles(path + folderName).Length;
    }
    public void AddSaveHandler(ISavable save)
    {
        saveData.Add(save);
    }
    public void ChangeSceneSaveData()
    {
        foreach (var save in saveData)
        {
            save.SaveData();
        }
    }
}
