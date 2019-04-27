using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    GameState _state = GameState.Scene;
    public GameState State { get => _state; private set => _state = value; }

    int _index = 1;

    private void Start()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        while (true)
        {
            State = GameState.Scene;
            yield return StartCoroutine(PlayNextScene());

            State = GameState.Transition;
            yield return new WaitForSeconds(_timeBetweenScenes);

            SceneManager.UnloadSceneAsync("Scene" + _index);

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

        Dialog SceneDialog = Resources.Load<Dialog>("Scene" + _index);
        _text.text = SceneDialog.Question;
        yield return new WaitForSeconds(SceneDialog.Duration);
        _text.text = "";
    }

    public void LoadFinalScene()
    {
        // only run once

        if (_index < 0)
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

        SceneManager.LoadSceneAsync("FinalScene", LoadSceneMode.Additive);
        _text.text = "";
        _index = -1;
    }
}