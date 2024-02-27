using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class VariableStoreTesting : MonoBehaviour
    {
        void Start()
        {
            VariableStore.CreateDatabase("DB_Numbers");
            VariableStore.CreateDatabase("DB_Booleans");

            VariableStore.CreateVariable("DB_Numbers.num1", 1);
            VariableStore.CreateVariable("DB_Numbers.num5", 5);
            VariableStore.CreateVariable("DB_Booleans.lightIsOn", true);
            VariableStore.CreateVariable("DB_Numbers.float1", 7.5f);
            VariableStore.CreateVariable("str1", "hello");
            VariableStore.CreateVariable("str2", "world");

            VariableStore.PrintAllDatabases();

            VariableStore.PrintAllVariables();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                VariableStore.PrintAllVariables();  
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                string variable = "DB_Numbers.num1";
                VariableStore.TryGetValue(variable, out object v);
                VariableStore.TrySetValue(variable, (int)v + 5 );
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                VariableStore.TryGetValue("DB_Numbers.num1", out object num1);
                VariableStore.TryGetValue("DB_Numbers.num5", out object num2);

                Debug.Log($"num1 + num2 = {(int)num1 + (int)num2}");
            }
        }
    }
}
