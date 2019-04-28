using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _main = null;
    [SerializeField] AudioSource[] _tracks = null;
    [SerializeField] float _fadeSpeed = 2f;

    int _currentTrackIndex = -1;
    
    public void SetActiveTrack(int TrackIndex)
    {
        _currentTrackIndex = TrackIndex;
    }
    
    void Update()
    {
        int i = 0;
        foreach (AudioSource Track in _tracks)
        {
            Track.volume = Mathf.Lerp(
                Track.volume, 
                i == _currentTrackIndex ? 1f : 0f,
                Time.deltaTime * _fadeSpeed);

            ++i;
        }
    }
}
