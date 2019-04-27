using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeClosing : MonoBehaviour
{
    [SerializeField] EnergyBar _energyBar = null;
    [SerializeField] Game _game = null;
    [SerializeField] Image _blackOverlay = null;
    [SerializeField] float _totalTime = 5f;

    float _usedTime = 0;
    
    void Update()
    {
        _blackOverlay.enabled = !Input.GetKey(KeyCode.Space);

        if (Input.GetKey(KeyCode.Space))
        {
            _usedTime += Time.deltaTime;
            _energyBar.SetRatio(1f - (_usedTime/_totalTime));

            if (_usedTime >= _totalTime)
            {
                _game.LoadFinalScene();
                enabled = false;
            }
        }
    }
}
