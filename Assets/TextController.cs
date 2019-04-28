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

    public void SetText(Character Speaker, string Text)
    {
        StopAllCoroutines();

        if (Speaker != null && Text != "")
            StartCoroutine(WriteText(Speaker, Text));
        else
            InnerText.text = "";
    }

    IEnumerator WriteText(Character Speaker, string Text)
    {
        InnerText.text = "";
        
        foreach (var c in Text.Split(' '))
        {
            InnerText.text += c + " ";
            
            AudioSource.clip = AudioClips[Random.Range(0, AudioClips.Length - 1)];
            AudioSource.Play();
            
            // head animation

            float ElapsedTime = 0f;
            while (ElapsedTime < _delay)
            {
                if (Speaker != null)
                    Speaker.SetWordNormalizedTime(ElapsedTime / _delay);
                
                yield return null;
                ElapsedTime += Time.deltaTime;
            }
        }
    }
}
