using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Text = null;

    int _index = 1;

    private void Start()
    {
        LoadNewScene();

        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        while (Application.CanStreamedLevelBeLoaded("Scene" + (_index + 1)))
        { 
            yield return new WaitForSeconds(2f);
            SceneManager.UnloadSceneAsync("Scene" + _index);

            ++_index;

            LoadNewScene();
        }
    }

    void LoadNewScene()
    {
        SceneManager.LoadSceneAsync("Scene" + _index, LoadSceneMode.Additive);
        Text.text = Resources.Load<Dialog>("Scene" + _index).Question;
    }
}