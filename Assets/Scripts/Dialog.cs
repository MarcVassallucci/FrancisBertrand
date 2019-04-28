using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    [TextArea]
    public string Question = "Est-ce que tu approuves ?";
    
    public float Duration = 5f;

    [Range(0, 4)]
    public int AudioTrackIndex = 0;

    public int FaceIndex = 0;

    public DialogOption Yes = new DialogOption("Je savais que tu comprendrais.");
    public DialogOption No = new DialogOption("Tu es un monstre. Je te deteste.");
    public DialogOption NoAnswer = new DialogOption("Je ne pense pas qu'il puisse nous entendre...");
}

[System.Serializable]
public class DialogOption
{
    [TextArea]
    public string Reaction = "Je le savais...";

    public int FaceIndex = 0;

    public DialogOption(string Reaction)
    {
        this.Reaction = Reaction;
    }
}
