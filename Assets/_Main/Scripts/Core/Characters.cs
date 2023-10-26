using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;
using stuff;
using Unity.VisualScripting;

public class Characters
{
    public string shortName;
    public string fullName;
    public Color color;
    public string image;
    public Characters(string shortNameInput, string fullNameInput, Color colorInput, string imageInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = Color.white;
        this.image = imageInput;

        CheckNames();
    }

    public Characters(string shortNameInput, string fullNameInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = Color.white;
        this.image = null;

        CheckNames();
    }
    public void CheckNames()
    {
        if (this.fullName == null)
        {
            throw new InvalidPropertyException("Full name must contain a string");
        }

        if (this.shortName == null)
        {
            throw new InvalidPropertyException("Short name must contain a string");
        }
    }
}
