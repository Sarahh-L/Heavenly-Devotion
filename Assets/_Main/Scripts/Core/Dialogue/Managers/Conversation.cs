using System.Collections.Generic;

namespace Dialogue
{ 
    public class Conversation
    {
        private List<string> lines = new List<string>();
        private int progress = 0;

        public Conversation(List<string> lines, int progress = 0)
        {
            this.lines = lines;
            this.progress = progress;
        }

        public int Getprogress() => progress;
        public void Setprogress(int value) => progress = value;

        public void IncrementProgress() => progress++;
        public int Count => lines.Count;
        public List<string> Getlines() => lines;
        public string Currentlines() => lines[progress];
        public bool HasReachedEnd() => progress >= lines.Count;

    }
}
