using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    [TextArea]
    public string Question = "Est-ce que tu approuves ?";

    public float Duration = 5f;
}
