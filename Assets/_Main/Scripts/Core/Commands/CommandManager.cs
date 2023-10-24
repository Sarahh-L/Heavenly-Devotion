using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Dialogue;

public class CommandManager : MonoBehaviour
{
    public static CommandManager instance { get; private set; }
    private CommandDatabase database;
    private void Awake()
    {
        if (instance != null)
        {
            instance = this;

            database = new CommandDatabase();
        }
        else
            DestroyImmediate(gameObject);
    }
}
