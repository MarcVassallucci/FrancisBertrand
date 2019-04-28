using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Transform _head = null;

    [SerializeField] SkinnedMeshRenderer _renderer = null;
    [SerializeField] Texture[] _textures = null;
    [SerializeField] int _materialID = 4;
    
    public void SetWordNormalizedTime(float NormalizedTime)
    {   
        _head.localScale = Vector3.one * 
            (1f + Mathf.Sin((Mathf.PI * 2f) * NormalizedTime) * 0.1f);
    }

    public void SetFace(int FaceIndex)
    {
        Debug.Assert(_renderer != null);

        foreach (Material mat in _renderer.materials)
        {
            if (mat.name.StartsWith("Face"))
            {
                mat.mainTexture = _textures[FaceIndex];
                break;
            }
        }
    }
}
