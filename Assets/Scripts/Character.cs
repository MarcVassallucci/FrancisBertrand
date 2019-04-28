using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Transform _head;

    public void SetWordNormalizedTime(float NormalizedTime)
    {
        _head.localScale = Vector3.one * 
            (1f + Mathf.Sin((Mathf.PI * 2f) * NormalizedTime) * 0.1f);
    }
}
