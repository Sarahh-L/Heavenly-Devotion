using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DL_Speaker
{
    public string name, castName;
    public Vector2 castPosition;
    public List<(int layer, string expression)> CastExpression { get; set; }
}

