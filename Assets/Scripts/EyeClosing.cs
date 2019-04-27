using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeClosing : MonoBehaviour
{
    [SerializeField] float _totalTime = 5f;
    [SerializeField] float _fadeSpeed = 7f;
    [SerializeField] float _eyeShakeAmplitude = 0.0025f;
    [SerializeField] EnergyBar _energyBar = null;
    [SerializeField] Game _game = null;
    [SerializeField] Image _blackOverlay = null;
    [SerializeField] SkinnedMeshRenderer _eye = null;

    float _usedTime = 0;

    bool _forcedShut = false;
    public bool ForcedShut { get => _forcedShut; set => _forcedShut = value; }

    void Update()
    {
        bool ShouldBeClosed = _game.State != GameState.Scene
            || Input.GetKey(KeyCode.Space) == false;
        
        _blackOverlay.color = Color.Lerp(_blackOverlay.color,
            ShouldBeClosed ? Color.black : new Color(0f, 0f, 0f, 0f),
            Time.deltaTime * _fadeSpeed);
        
        _eye.SetBlendShapeWeight(0, Mathf.Lerp(_eye.GetBlendShapeWeight(0), 
            ShouldBeClosed ? 100f : 0f, 
            Time.deltaTime * _fadeSpeed));
        
        _eye.transform.localPosition = new Vector3(
            Random.Range(-_eyeShakeAmplitude, _eyeShakeAmplitude), 
            Random.Range(-_eyeShakeAmplitude, _eyeShakeAmplitude), 
            _eye.transform.localPosition.z);

        //_eye.transform.localScale = Vector3.one * Mathf.Lerp(_eye.transform.localScale.x, ShouldBeClosed ? 1f : 1f, Time.deltaTime * _fadeSpeed);

        if (_game.State == GameState.Scene && Input.GetKey(KeyCode.Space))
        {
            _usedTime += Time.deltaTime;
            _energyBar.SetRatio(1f - (_usedTime/_totalTime));

            if (_usedTime >= _totalTime)
            {
                _game.LoadFinalScene();
            }
        }
    }
}
