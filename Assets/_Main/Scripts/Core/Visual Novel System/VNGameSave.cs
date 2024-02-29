using Dialogue;
using History;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GO BACK IN THE HISTORY INTERMISSION AND FIX THE DIALOGUE PROMPT THING
// 20:56
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

    public string[] activeConversations;
    public HistoryState activeState;
    public HistoryState[] historyLogs;

    public void Save()
    {
        activeState = HistoryState.Capture();
        historyLogs = HistoryManager.instance.history.ToArray();
        activeConversations = GetConversationData();

        string saveJSON = JsonUtility.ToJson(this);
    }

    public void load()
    {

    }

    public string[] GetConversationData()
    {
        List<string> retData = new List<string>();

        var conversations = DialogueSystem.instance.conversationManager.GetConversationQueue();

        for(int i = 0; i < conversations.Length; i++)
        {
            var conversation = conversations[i];
            string data = "";

            if (conversation.file != string.Empty)
            {
                var compressedData = new VN_ConversationDataCompressed();
                compressedData.fileName = conversation.file;
                compressedData.progress = conversation.GetProgress();
                compressedData.startIndex = conversation.fileStartIndex;
                compressedData.endindex = conversation.fileEndIndex;
                data = JsonUtility.ToJson(compressedData);
            }

            else
            {
                var fullData = new VN_ConversationData();
                fullData.conversation = conversation.GetLines();
                fullData.progress = conversation.GetProgress();
                data = JsonUtility.ToJson(fullData);
            }

            retData.Add(data);
        }

        return retData.ToArray();
    }
}
