using UnityEngine;

public interface IAudioSourceGame
{
    void Pause();
    void Play();
    void Play(AudioClip audioClip, float volumeScale = 1, bool isLoop = false);
    void PlayOneShot(AudioClip audioClip, float volumeScale = 1);
    void Stop();
}