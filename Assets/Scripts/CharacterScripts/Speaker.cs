using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumFlagsAttribute : PropertyAttribute
{
    public EnumFlagsAttribute() { }
}

public class Speaker : MonoBehaviour
{
    [System.Flags]
    public enum SpeakPhase
    {
        Question,
        ReactionYes,
        ReactionNo,
        ReactionNoAnswer
    }

    [SerializeField] [EnumFlags] SpeakPhase _phase;

    public void TrySpeak(SpeakPhase Phase)
    {
        if ((_phase & Phase) == Phase)
        {

        }
    }
}