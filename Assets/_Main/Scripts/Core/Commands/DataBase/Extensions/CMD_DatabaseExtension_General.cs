using Dialogue;
using System;
using System.Collections;
using UnityEngine;

namespace Commands
{
    public class CMD_DatabaseExtension_General : CMD_DatabaseExtension
    {
        private static string[] Param_Immediate => new string[] { "-i", "-immediate" };     // Specify within the MoveCharacter command - will not work for CreateCharacter
        private static string[] Param_Speed => new string[] { "-spd", "-speed" };
        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("wait", new Func<string, IEnumerator>(Wait));

            // Dialogue box controls
            database.AddCommand("showdb", new Func<string[], IEnumerator>(ShowDialogueBox));
            database.AddCommand("hidedb", new Func<string[], IEnumerator>(HideDialogueBox));

                   // Dialogue system controls
            database.AddCommand("showui", new Func<string[], IEnumerator>(ShowDialogueSystem));
            database.AddCommand("hideui", new Func<string[], IEnumerator>(HideDialogueSystem));

        }

        #region Wait
        private static IEnumerator Wait(string data)
        {
            if (float.TryParse(data, out float time)) 
            {
                yield return new WaitForSeconds(time);
            }
        }
        #endregion

        #region Show / Hide Dialogue Box
        private static IEnumerator ShowDialogueBox(string[] data)
        {
            float speed;
            bool immediate;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(Param_Speed, out speed, defaultValue: 1f);
            parameters.TryGetValue(Param_Immediate, out immediate, defaultValue: false);

            yield return DialogueSystem.instance.dialogueContainer.Show(speed, immediate);
        }

        private static IEnumerator HideDialogueBox(string[] data)
        {
            float speed;
            bool immediate;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(Param_Speed, out speed, defaultValue: 1f);
            parameters.TryGetValue(Param_Immediate, out immediate, defaultValue: false);

            yield return DialogueSystem.instance.dialogueContainer.Hide(speed, immediate);
        }
        #endregion

        #region Show / Hide Dialogue System
        private static IEnumerator ShowDialogueSystem(string[] data)
        {
            float speed;
            bool immediate;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(Param_Speed, out speed, defaultValue: 1f);
            parameters.TryGetValue(Param_Immediate, out immediate, defaultValue: false);

            yield return DialogueSystem.instance.Show(speed, immediate);
        }

        private static IEnumerator HideDialogueSystem(string[] data)
        {
            float speed;
            bool immediate;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(Param_Speed, out speed, defaultValue: 1f);
            parameters.TryGetValue(Param_Immediate, out immediate, defaultValue: false);

            yield return DialogueSystem.instance.Hide(speed, immediate);
        }
        #endregion
    }
}
