using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputControl
{
    public string action;
    public KeyCode key;

    public InputControl()
    {

    }

    public InputControl(string action, KeyCode key)
    {
        this.action = action;
        this.key = key;
    }
}

public class InputActionNames
{
    public const string X = "X";
    public const string O = "0";
    public const string DODGE = "DODGE";
    public const string JUMP = "JUMP";
    public const string SHOWSWORD = "SHOWSWORD";
}