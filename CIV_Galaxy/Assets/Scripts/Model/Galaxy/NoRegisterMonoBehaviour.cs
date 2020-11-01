using System.Collections.Generic;
using UnityEngine;

public abstract class NoRegisterMonoBehaviour : MonoBehaviour
{
    public StatisticsPurchase StatisticsPurchasePlayer => GameMenegerRegister.Instance.StatisticsPlayer;

    public T GetRegisterObject<T>() => GameMenegerRegister.Instance.GetRegisterObject<T>();
    public void ClearRegisteScene() => GameMenegerRegister.Instance.Clear();
    public void Register(object obj) => GameMenegerRegister.Instance.Register(obj); 
    public void Register(params object[] obj) => GameMenegerRegister.Instance.Register(obj);
    public List<T> GetRegisterObjects<T>() => GameMenegerRegister.Instance.GetRegisterObjects<T>();

    public void PlayNewMusic(AudioClip clip) => GameMenegerRegister.Instance.PlayNewMusic(clip);
    public void PlaySoundEffect(AudioClip soundEffect, float volume = 1f) => GameMenegerRegister.Instance.PlaySoundEffect(soundEffect, volume);
}