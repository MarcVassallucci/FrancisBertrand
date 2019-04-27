using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] RectTransform _innerImage = null;
    
    public void SetRatio(float Ratio)
    {
        _innerImage.sizeDelta = new Vector2(Mathf.Lerp(-GetComponent<RectTransform>().rect.width, 0f, Ratio), 0f);
    }
}
