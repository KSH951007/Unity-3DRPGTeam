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
        DirectoryInfo directory = new DirectoryInfo(path);

        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete();
        }

        foreach (DirectoryInfo dir in directory.GetDirectories())
        {
            dir.Delete(true);
        }
    }
    [MenuItem("Save/OpenFolder")]
    public static void OpenSaveFolder()
    {
        string path = Application.persistentDataPath + "/";
        Process.Start(path);
    }
}
