using Systems.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue.LogicalLines
{
    public interface ILogicalLine
    {
        bool Matches(Dialogue_Line line);
        IEnumerator Execute();
    }
}