using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceGame : NoRegisterMonoBehaviour, IAudioSourceGame
{
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField, Space(10)] private bool isGalaxyTimer = false;
    private AudioSource _audioSource;
    private IGalaxyUITimer _galaxyUITimer;
    private bool isPause = false;
    private Action actFromPause = delegate { };

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (isGalaxyTimer)
        {
            _galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
            _galaxyUITimer.PauseAct += _galaxyUITimer_PauseAct;
        }
    }

    // Воспроизвести звук
    public void PlayOneShot(int idClip = 0, float volumeScale = 1.0F)
    {
        if (idClip < audioClips.Count)
        {
            if (isPause)
                actFromPause = () => _audioSource.PlayOneShot(audioClips[idClip], volumeScale);
            else _audioSource.PlayOneShot(audioClips[idClip], volumeScale);
        }
    }

    // Воспроизвести звук
    public void PlayOneShot(AudioClip audioClip, float volumeScale = 1.0F)
    {
        if (isPause)
            actFromPause = () => _audioSource.PlayOneShot(audioClip, volumeScale);
        else _audioSource.PlayOneShot(audioClip, volumeScale);
    }

    // Воспроизвести звук
    public void Play(AudioClip audioClip, float volumeScale = 1.0F, bool isLoop = false)
    {
        _audioSource.clip = audioClip;
        if (isPause)
            actFromPause = () => _audioSource.Play();
        else _audioSource.Play();

        _audioSource.volume = volumeScale;
        _audioSource.loop = isLoop;
    }

    public void Play()
    {
        if (isPause)
            actFromPause = () => _audioSource.Play();
        else _audioSource.Play();
    }

    public void Pause() => _audioSource.Pause();

    public void Stop() => _audioSource.Stop();

    private void _galaxyUITimer_PauseAct(bool isPause)
    {
        this.isPause = isPause;
        if (isPause == true)
            _audioSource.Pause();
        else
        {
            actFromPause.Invoke();
            actFromPause = delegate { };
        }

        _audioSource.Play();
    }

    private void OnDestroy()
    {
        if (isGalaxyTimer)
            _galaxyUITimer.PauseAct -= _galaxyUITimer_PauseAct;
    }
}
