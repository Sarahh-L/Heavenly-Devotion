using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Characters;
using System.Linq;

namespace Commands
{
    public class CMD_DatabaseExtension_Characters : CMD_DatabaseExtension
    {
        private static string[] Param_Enable => new string[] { "-e", "-enable" };
        private static string[] Param_Immediate => new string[] { "-i", "-immediate" };     // Specify within the MoveCharacter command - will not work for CreateCharacter
        private static string[] Param_Speed => new string[] { "-spd", "-speed" };
        private static string[] Param_Smooth => new string[] { "-s", "-smooth" };
        private static string Param_XPos => "-x";
        private static string Param_YPos => "-y";
        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("createcharacter", new Action<string[]>(CreateCharacter));
            database.AddCommand("movecharacter", new Func<string[], IEnumerator>(MoveCharacter));
            database.AddCommand("show", new Func<string[], IEnumerator>(ShowAll));
            database.AddCommand("hide", new Func<string[], IEnumerator>(HideAll));
        }

        #region Create character
        public static void CreateCharacter(string[] data)
        {
            string characterName = data[0];
            bool enable = false;
            bool immediate = false;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(Param_Enable, out enable, defaultValue: false);
            parameters.TryGetValue(Param_Immediate, out immediate, defaultValue: false);


            Character character = CharacterManager.instance.CreateCharacter(characterName);

            if (!enable)
                return;

            else if (immediate)
                character.isVisible = true;
            else
                character.Show();
        }
        #endregion

        private static IEnumerator MoveCharacter(string[] data)
        {
            string characterName = data[0];
            Character character = CharacterManager.instance.GetCharacter(characterName);

            if (character == null)
                yield break;

            float x = 0, y = 0;
            float speed = 1;
            bool smoothing = false;
            bool immediate = false;

            var parameters = ConvertDataToParameters(data);

            // try to get the x axis position
            parameters.TryGetValue(Param_XPos, out x);

            // try to get the y axis position
            parameters.TryGetValue(Param_YPos, out y);

            // try to get the speed
            parameters.TryGetValue(Param_Speed, out speed, defaultValue: 1);

            // try to get smoothing
            parameters.TryGetValue(Param_Smooth, out smoothing, defaultValue: false);

            // try to get immediate setting of position
            parameters.TryGetValue(Param_Immediate, out immediate, defaultValue: false);

            Vector2 position = new Vector2(x, y);

            if (immediate)
                character.SetPosition(position);
            else
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => { character?.SetPosition(position); });
                yield return character.MoveToPostion(position, speed, smoothing);
            }
        }

        #region Show characters
        public static IEnumerator ShowAll(string[] data)
        {
            List<Character> characters = new List<Character>();
            bool immediate = false;
            float speed = 1f;

            foreach (string s in data)
            {
                Character character = CharacterManager.instance.GetCharacter(s, createIfDoesNotExist: false);
                if (character != null)
                    characters.Add(character);
            }

            if (characters.Count == 0)
                yield break;

            // Convert the data array to a parameter container
            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(Param_Immediate, out immediate, defaultValue: false);
            parameters.TryGetValue(Param_Speed, out speed, defaultValue: 1f);

            // Call the logic on all the characters
            foreach (Character character in characters)
            {
                if (immediate)
                    character.isVisible = true;
                else
                    character.Show(speed);
            }

            if(!immediate)
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() =>
                {
                    foreach(Character character in characters)
                        character.isVisible = true;
                });

                while(characters.Any(c => c.isRevealing))
                    yield return null;
            }
        }
        #endregion

        #region Hide characters
        public static IEnumerator HideAll(string[] data)
        {
            List<Character> characters = new List<Character>();
            bool immediate = false;
            float speed = 1f;

            foreach (string s in data)
            {
                Character character = CharacterManager.instance.GetCharacter(s, createIfDoesNotExist: false);
                if (character != null)
                    characters.Add(character);
            }

            if (characters.Count == 0)
                yield break;

            // Convert the data array to a parameter container
            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(Param_Immediate, out immediate, defaultValue: false);
            parameters.TryGetValue(Param_Speed, out speed, defaultValue: 1f);

            foreach (Character character in characters)
            {
                if (immediate)
                    character.isVisible = false;
                else
                    character.Hide(speed);
            }

            if (!immediate)
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() =>
                {
                    foreach (Character character in characters)
                        character.isVisible = false;
                });

                while (characters.Any(c => c.isHiding))
                    yield return null;
            }
        }
        #endregion
    }
}
