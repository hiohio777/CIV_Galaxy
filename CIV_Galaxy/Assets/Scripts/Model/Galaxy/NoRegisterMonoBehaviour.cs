using System.Collections.Generic;
using UnityEngine;

public abstract class NoRegisterMonoBehaviour : MonoBehaviour
{
    public T GetRegisterObject<T>() => GameManager.Instance.GetRegisterObject<T>();
    public List<T> GetRegisterObjects<T>() => GameManager.Instance.GetRegisterObjects<T>();
    public void PlayNewMusic(AudioClip clip) => GameManager.Instance.PlayNewMusic(clip);
    public void PlayUISound(AudioClip soundEffect, float volume = 1f) => GameManager.Instance.PlayUISound(soundEffect, volume);
}