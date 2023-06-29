using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;

public class RandomTrack : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    private AudioSource _audioSource;
    private AudioClip _currentTrack;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentTrack = _audioManager.GetRandomSound;
        _audioSource.clip = _currentTrack;
        _audioSource.spatialize = true;
        _audioSource.spatialBlend = 1f;
        _audioSource.Play();

    }
    void Update()
    {
        if (_audioSource.isPlaying){ return; }
        _currentTrack = _audioManager.GetRandomSound;
        _audioSource.clip = _currentTrack;
        _audioSource.Play();
    }

}
