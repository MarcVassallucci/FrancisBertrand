using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    Scene,
    Transition,
    Over
}

public class Game : MonoBehaviour
{
    [SerializeField] float _timeBetweenScenes = 3f;
    [SerializeField] TextMeshProUGUI _text = null;

    GameState _state = GameState.Transition;
    public GameState State { get => _state; private set => _state = value; }

    int _index = 1;
    Dialog _currentDialog = null;   
    bool _currentDialogHasAnswer = false;

    void Start()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(_timeBetweenScenes);

        while (true)
        {
            State = GameState.Scene;
            yield return StartCoroutine(PlayNextScene());

            yield return new WaitForSeconds(2f);
            State = GameState.Transition;
            yield return new WaitForSeconds(.5f);
            SceneManager.UnloadSceneAsync("Scene" + _index);
            _currentDialogHasAnswer = false;
            _text.text = "";
            yield return new WaitForSeconds(_timeBetweenScenes);

            ++_index;

            if (!Application.CanStreamedLevelBeLoaded("Scene" + _index))
            {
                break;
            }
        }

        LoadFinalScene();
    }
    
    IEnumerator PlayNextScene()
    {
        SceneManager.LoadSceneAsync("Scene" + _index, LoadSceneMode.Additive);
        
        _currentDialog = Resources.Load<Dialog>("Scene" + _index);
        _text.text = _currentDialog.Question;

        float TimeSinceQuestion = 0f;
        while (true)
        {
            if (TimeSinceQuestion > _currentDialog.Duration)
            {
                _text.text = _currentDialog.NoAnswer.Reaction;
                break;
            }

            if (_currentDialogHasAnswer == true)
            {
                break;
            }

            yield return null;
            TimeSinceQuestion += Time.deltaTime;
        }
    }

    public void LoadFinalScene()
    {
        // only run once

        if (State == GameState.Over)
        {
            return;
        }
        
        // interrupt possibly running scene

        StopAllCoroutines();

        // unload current scene

        if (SceneManager.GetSceneByName("Scene" + _index).isLoaded)
        {
            SceneManager.UnloadSceneAsync("Scene" + _index);
        }

        // load final scene

        State = GameState.Over;
        SceneManager.LoadSceneAsync("FinalScene");
    }

    public void OnAnswer(bool IsYes)
    {
        if (State != GameState.Scene || _currentDialogHasAnswer == true)
            return;

        _currentDialogHasAnswer = true;
        _text.text = IsYes ? _currentDialog.Yes.Reaction : _currentDialog.No.Reaction;
    }
}