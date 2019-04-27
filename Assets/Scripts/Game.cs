using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text = null;
    
    int _index = 1;

    private void Start()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return StartCoroutine(PlayNextScene());

        while (true)
        { 
            SceneManager.UnloadSceneAsync("Scene" + _index);

            ++_index;

            if (!Application.CanStreamedLevelBeLoaded("Scene" + _index))
            {
                break;
                
            }

            yield return StartCoroutine(PlayNextScene());
        }

        LoadFinalScene();
    }
    
    IEnumerator PlayNextScene()
    {
        SceneManager.LoadSceneAsync("Scene" + _index, LoadSceneMode.Additive);

        Dialog SceneDialog = Resources.Load<Dialog>("Scene" + _index);
        Text.text = SceneDialog.Question;
        yield return new WaitForSeconds(SceneDialog.Duration);
    }

    public void LoadFinalScene()
    {
        StopAllCoroutines();

        // unload current scene

        if (SceneManager.GetSceneByName("Scene" + _index).isLoaded)
        {
            SceneManager.UnloadSceneAsync("Scene" + _index);
        }

        // load final scene

        SceneManager.LoadSceneAsync("FinalScene", LoadSceneMode.Additive);
        Text.text = "";
        _index = -1;
    }
}