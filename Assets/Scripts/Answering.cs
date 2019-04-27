using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answering : MonoBehaviour
{
    [SerializeField] Game _game = null;
    [SerializeField] float _maxTimeBetweenClicks = 0.2f;

    bool _hasClicked = false;
    float _timeSinceLastClick = 0f;

    void Update()
    {
        if (_game.State == GameState.Transition)
        {
            _hasClicked = false;
            _timeSinceLastClick = 0f;
        }

        _timeSinceLastClick += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (_timeSinceLastClick <= _maxTimeBetweenClicks)
            {
                _game.OnAnswer(false);
            }

            _timeSinceLastClick = 0f;
            _hasClicked = true;
        }
        else if (_hasClicked && _timeSinceLastClick > _maxTimeBetweenClicks)
        {
            _game.OnAnswer(true);
            _hasClicked = false;
        }
    }
}
