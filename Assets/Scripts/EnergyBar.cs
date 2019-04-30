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
    private Vector2 _initialPos;

    private void Start()
    {
        _initialPos = GetComponent<RectTransform>().anchoredPosition;
    }
    
    public void SetRatio(float Ratio)
    {
        _innerRect.sizeDelta = new Vector2(Mathf.Lerp(-GetComponent<RectTransform>().rect.width, 0f, Ratio), 0f);
    }

    public void SetIsInDanger(bool IsIndanger)
    {
        _innerImage.color = Color.Lerp(_innerImage.color,
            IsIndanger ? Color.Lerp(_dangerColor, _safeColor, (Time.time * 5f) % 1f) : _safeColor, 
            Time.deltaTime * 10f);

        if (IsIndanger)
        {
            float ShakeAmplitude = 1f;
            GetComponent<RectTransform>().anchoredPosition = _initialPos + new Vector2(
                Random.Range(-ShakeAmplitude, ShakeAmplitude),
                Random.Range(-ShakeAmplitude, ShakeAmplitude));
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = _initialPos;
        }
    }
}
