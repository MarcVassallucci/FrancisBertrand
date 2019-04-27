using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] RectTransform _innerRect = null;
    [SerializeField] Image _innerImage = null;
    [SerializeField] Color _safeColor = Color.blue;
    [SerializeField] Color _dangerColor = Color.red;

    public void SetRatio(float Ratio)
    {
        _innerRect.sizeDelta = new Vector2(Mathf.Lerp(-GetComponent<RectTransform>().rect.width, 0f, Ratio), 0f);
    }

    public void SetIsInDanger(bool IsIndanger)
    {
        _innerImage.color = Color.Lerp(_innerImage.color,
            IsIndanger ? _dangerColor : _safeColor, 
            Time.deltaTime * 10f);
    }
}
