using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory
{
}

[System.Serializable]
public class GameStateModification
{
    public enum ModificationType
    {
        Increment,
        Decrement
    }

    public string Key = "Mom love";
    public ModificationType Type = ModificationType.Increment;
    public int Value = 1;
}