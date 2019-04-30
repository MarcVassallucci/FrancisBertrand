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
    [SerializeField] float _timeToAnswer = 4f;
    [SerializeField] AudioManager _audio = null;
    [SerializeField] TextController _textController = null;
    [SerializeField] AudioSource _voiceAudioSource = null;
    GameState _state = GameState.Transition;
    public GameState State { get => _state; private set => _state = value; }

    int _index = 1;
    Dialog _currentDialog = null;   
    bool _currentDialogHasAnswer = false;
    Character _currentSpeaker;
    float _answerDuration;

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
            _textController.SetText(null, "");
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
        // BEGIN HACK
        if (_index == 2)
            GetComponent<EyeClosing>()._paused = false;
        // END HACK

        var Async = SceneManager.LoadSceneAsync("Scene" + _index, LoadSceneMode.Additive);
        while (Async.isDone == false)
            yield return null;

        _currentDialog = Resources.Load<Dialog>("Scene" + _index);
        PlayVoiceAudio(_currentDialog.Voice);
        _currentSpeaker = GameObject.FindObjectOfType<Character>();
        _audio.SetActiveTrack(_currentDialog.AudioTrackIndex);
        _textController.SetText(_currentSpeaker, _currentDialog.Question);

        float TimeSinceQuestion = 0f;
        float Duration = _currentDialog.Voice != null ? _currentDialog.Voice.length + _timeToAnswer : _currentDialog.Duration;
        while (true)
        {
            if (TimeSinceQuestion > Duration)
            {
                _textController.SetText(_currentSpeaker, _currentDialog.NoAnswer.Reaction);
                PlayVoiceAudio(_currentDialog.NoAnswer.Voice);

                if (_currentDialog.NoAnswer.Voice != null)
                    yield return new WaitForSeconds(_currentDialog.NoAnswer.Voice.length);
                else
                    yield return new WaitForSeconds(2f);

                break;
            }

            if (_currentDialogHasAnswer == true)
            {
                // BEGIN HACK
                if (_index == 1)
                {
                    yield return new WaitForSeconds(6f);
                    break;
                }
                // END HACK

                yield return new WaitForSeconds(_answerDuration);
                break;
            }

            yield return null;
            TimeSinceQuestion += Time.deltaTime;
        }
    }

    private void PlayVoiceAudio(AudioClip voice)
    {
        if (voice != null)
        {
            _voiceAudioSource.clip = voice;
            _voiceAudioSource.Play();
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
        Debug.Assert(_currentSpeaker != null);
        UpdateCharacters(IsYes);

        if (IsYes == true && _currentDialog.Yes.Voice != null)
            _answerDuration = _currentDialog.Yes.Voice.length;
        else if (IsYes == false && _currentDialog.No.Voice != null)
            _answerDuration = _currentDialog.No.Voice.length;
        else
            _answerDuration = 2f;

        _textController.SetText(_currentSpeaker, IsYes ? _currentDialog.Yes.Reaction : _currentDialog.No.Reaction);
        PlayVoiceAudio(IsYes ? _currentDialog.Yes.Voice : _currentDialog.No.Voice);
    }

    private void UpdateCharacters(bool IsYes)
    {
        foreach (Character Character in GameObject.FindObjectsOfType<Character>())
        {
            foreach (FaceChange FaceChange in Character.GetComponents<FaceChange>())
                FaceChange.SetFace(IsYes);
        }
    }
}