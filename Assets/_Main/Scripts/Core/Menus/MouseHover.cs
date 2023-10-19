using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseOver()
    {
        Debug.Log ("hee hoo peanut");
    }

    // Update is called once per frame
    void OnMouseExit()
    {
        Debug.Log ("nuh uh");
    }
}
