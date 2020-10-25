using System;
using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSourceMusic1, audioSourceMusic2, audioSourceUI;
    private AudioSource _currentAudioSourceMusic;
    private float fadeInSec = 2f;

    private void Awake()
    {
        _currentAudioSourceMusic = audioSourceMusic1 = gameObject.AddComponent<AudioSource>();
        audioSourceMusic2 = gameObject.AddComponent<AudioSource>();
        audioSourceMusic1.loop = audioSourceMusic2.loop = true;

        audioSourceUI = gameObject.AddComponent<AudioSource>();
        audioSourceUI.loop = false;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayUISound(AudioClip soundEffect, float volume = 1f) => audioSourceUI.PlayOneShot(soundEffect, volume);

    public void PlayNewMusic(AudioClip clip)
    {
        StopAllCoroutines();
        StartCoroutine(StartNewMusicClip(clip));
    }

    private IEnumerator StartNewMusicClip(AudioClip clip)
    {
        AudioSource newMainAudio = _currentAudioSourceMusic == audioSourceMusic1 ? audioSourceMusic2 : audioSourceMusic1;
        newMainAudio.clip = clip;
        newMainAudio.volume = 0;
        newMainAudio.Play();
        newMainAudio.loop = true;

        float volume;
        while (true)
        {
            volume = 1f / fadeInSec * Time.deltaTime;

            _currentAudioSourceMusic.volume -= volume;
            newMainAudio.volume += volume;

            if (newMainAudio.volume >= 0.7f) break;
            yield return null;
        }
        _currentAudioSourceMusic = newMainAudio;
    }
}
