using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VNGameSave
{
    public static VNGameSave activeFile = null;

    public const string file_type = ".vns";
    public const string screenshot_file_type = ".jpg";
    public const bool encrypt_files = false;

    public string filePath => $"{FilePaths.gameSaves}{slotNumber}{file_type}";
    public string screenshotPath => $"{FilePaths.gameSaves}{slotNumber}{screenshot_file_type}";

    public string playerName;
    public int slotNumber = 1;

    //public string[] activeConversations;      -- used for history

    public void Save()
    {
        
    }

    public void load()
    {

    }
}
