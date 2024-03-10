using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;

public class SaveFileManager : MonoBehaviour
{

    [MenuItem("Save/Delete")]
    public static void RemoveAllSaveFile()
    {
        string path = Application.persistentDataPath + "/";

        string[] test = Directory.GetFiles(path);

        for (int i = 0; i < test.Length; i++)
        {
            File.Delete(test[i]);
        }
    }
    [MenuItem("Save/OpenFolder")]
    public static void OpenSaveFolder()
    {   
        string path = Application.persistentDataPath + "/";

        Process.Start(path);
    }
}
