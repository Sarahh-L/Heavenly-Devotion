using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class CMD_DatabaseExtension_VisualNovel : CMD_DatabaseExtension
    {
        new public static void Extend(CommandDatabase database)
        {
            //Variable Assignment
            database.AddCommand("setplayername", new Action<string>(SetPlayerNameVariable));
            //database.AddCommand("setplayerstats", new Action<string>(SetPlayerStats));
            database.AddCommand("updatecommand", new Action<int>(UpdateCommand));
        }


        private static void SetPlayerNameVariable(string data)
        {
            VisualNovel.VNGameSave.activeFile.playerName = data;

        }

        /*private static void SetPlayerStats(string charisma)
        {
            VisualNovel.VNGameSave.activeFile.charisma = charisma;
        }*/

        private static void UpdateCommand(int value)
        {
            Debug.Log("your mom");
            VisualNovel.VNGameSave.activeFile.charVal = VisualNovel.VNGameSave.activeFile.charVal + value;
        }
    }
}