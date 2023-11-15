using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor.Profiling;



    public class DL_SpeakerData
    {
        public string name, castName;

        // name that will display in the dialogue box to show who is speaking
        public string displayName => (castName != string.Empty ? castName : name);

        public Vector2 castPosition;
        public List <(int layer, string expression)> CastExpressions { get; set; }

        private const string NameCastID = " as ";
        private const string PositionCastID = " at ";
        private const string ExpressionCastID = " [";
        private const char AxisDelimiter = ':';                 // separates position
        private const char ExpressionLayerDelimiter = ':';      // separates expressions from one another
        private const char ExpressionLayerJoiner = ',';         // joins expressions

        public DL_SpeakerData(string rawSpeaker)
        {
            string pattern = @$"{NameCastID}|{PositionCastID}|{ExpressionCastID.Insert(ExpressionCastID.Length-1, @"\")}";
            MatchCollection matches = Regex.Matches(rawSpeaker, pattern);

           // populate this data to avoid null references to values
            castName = "";
            castPosition = Vector2.zero;
            CastExpressions = new List<(int layer, string expression)>();


           // if no matches, the entire line is the speaker name
            if (matches.Count == 0)
            {
                name = rawSpeaker;
                return;
            }

           // otherwise, isolate speakername from casting data
            int index = matches[0].Index;

            name = rawSpeaker.Substring(0, index);

            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];

                int startIndex = 0, endIndex = 0;
                if (match.Value == NameCastID)
                {
                    startIndex = match.Index + NameCastID.Length;
                    endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                    castName = rawSpeaker.Substring(startIndex, endIndex - startIndex);
                }

                else if (match.Value == PositionCastID)
                {
                    startIndex = match.Index + PositionCastID.Length;
                    endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                    string castPos = rawSpeaker.Substring(startIndex, endIndex - startIndex);

                    string[] axis = castPos.Split(AxisDelimiter, System.StringSplitOptions.RemoveEmptyEntries);

                    float.TryParse(axis[0], out castPosition.x);

                    if (axis.Length > 1)
                        float.TryParse(axis[1], out castPosition.y);
                }

                else if (match.Value == ExpressionCastID)
                {
                    startIndex = match.Index +ExpressionCastID.Length;
                    endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;
                    string castExp = rawSpeaker.Substring(startIndex, endIndex - (startIndex + 1));

                    CastExpressions = castExp.Split(ExpressionLayerJoiner)
                        .Select(x =>
                        {
                            var parts = x.Trim().Split(ExpressionLayerDelimiter);
                            return (int.Parse(parts[0]), parts[1]);
                        }).ToList();
                }
            }
        }
    }

