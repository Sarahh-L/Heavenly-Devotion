using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dialogue.LogicalLines
{
    public static class LogicalLineUtilities
    {
        public static class Encapsulation
        {
            public struct EncapsulatedData
            {
                public List<string> lines;
                public int startingIndex;
                public int endingIndex;
            }

            private const char encapsulation_start = '{';
            private const char encapsulation_end = '}';

            public static bool IsEncapsulationStart(string line) => line.Trim().StartsWith(encapsulation_start);
            public static bool IsEncapsulationEnd(string line) => line.Trim().StartsWith(encapsulation_end);

            public static EncapsulatedData RipEncapsulationData(Conversation conversation, int startingIndex, bool ripHeaderandEncapsulators = false)
            { 
                int encapsulateDepth = 0;
                EncapsulatedData data = new EncapsulatedData { lines = new List<string>(), startingIndex = startingIndex, endingIndex = 0 };
                for (int i = startingIndex; i < conversation.Count; i++)
                {
                    string line = conversation.Getlines()[i];

                    if (ripHeaderandEncapsulators || encapsulateDepth > 0 && !IsEncapsulationEnd(line))
                        data.lines.Add(line);

                    if (IsEncapsulationStart(line))
                    {
                        encapsulateDepth++;
                        continue;
                    }

                    if (IsEncapsulationEnd(line))
                    {
                        encapsulateDepth--;
                        if (encapsulateDepth == 0)
                        {
                            data.endingIndex = i;
                            break;
                        }
                    }
                }

                return data;
            }

        }
    }
}
