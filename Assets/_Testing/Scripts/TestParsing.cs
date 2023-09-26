using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

namespace Testing
{
    public class TestParsing : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            string line = "Speaker \"Dialogue Goes here!!!!\" Command(arguments here)";

            DialogueParser.Parse(line);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}