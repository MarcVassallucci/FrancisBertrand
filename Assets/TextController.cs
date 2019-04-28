using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField] float _delay = 0.05f;
    [SerializeField] TextMeshProUGUI InnerText = null;
    [SerializeField] AudioSource AudioSource = null;
    [SerializeField] AudioClip[] AudioClips = null;

    public void SetText(string Text)
    {
        StopAllCoroutines();
        StartCoroutine(WriteText(Text));
    }

    IEnumerator WriteText(string Text)
    {
        InnerText.text = "";
        
        foreach (var c in Text.Split(' '))
        {
            InnerText.text += c + " ";
            
            AudioSource.clip = AudioClips[Random.Range(0, AudioClips.Length - 1)];
            AudioSource.Play();

            yield return new WaitForSeconds(_delay);
        }
    }
}
