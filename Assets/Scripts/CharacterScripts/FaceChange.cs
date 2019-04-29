using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChange : MonoBehaviour
{
    [SerializeField] Texture _yesTexture = null;
    [SerializeField] Texture _noTexture = null;
    
    public void SetFace(bool IsYes)
    {
        SkinnedMeshRenderer Renderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (IsYes == true && _yesTexture == null)
            return;

        if (IsYes == false && _noTexture == null)
            return;

        foreach (Material mat in Renderer.materials)
        {
            if (mat.name.StartsWith("Face"))
            {
                mat.mainTexture = IsYes ? _yesTexture : _noTexture;
                break;
            }
        }
    }
}
