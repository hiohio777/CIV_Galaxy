using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMenegerRegister : MonoBehaviour
{
    private static GameMenegerRegister _instance;
    public static GameMenegerRegister Instance {
        get {
            if (_instance != null) return _instance;

            _instance = Instantiate(Resources.Load<GameMenegerRegister>("GameManager"));
            return _instance;
        }
    }

    private AudioPlayer _audioPlayer;
    private List<object> registrObject = new List<object>();

    public StatisticsPurchase StatisticsPlayer { get; private set; }
    public void PlayNewMusic(AudioClip clip) => _audioPlayer.PlayNewMusic(clip);
    public void PlaySoundEffect(AudioClip soundEffect, float volume = 1f) => _audioPlayer.PlaySoundEffect(soundEffect, volume);

    private void Awake()
    {
        _audioPlayer = GetComponent<AudioPlayer>();
        StatisticsPlayer = GetComponent<StatisticsPurchase>();
        DontDestroyOnLoad(gameObject);
    }

    public void Clear() => registrObject.Clear();

    public void Register(object obj) => registrObject.Add(obj);
    public void Register(params object[] obj)
    {
        foreach (var item in obj)
            registrObject.Add(item);
    }

    public T GetRegisterObject<T>()
    {
        foreach (var item in registrObject)
        {
            if (item is T obj)
                return obj;
        }

        throw new ArgumentNullException(typeof(T).FullName);
    }
    public List<T> GetRegisterObjects<T>()
    {
        var listObject = new List<T>();
        foreach (var item in registrObject)
        {
            if (item is T obj)
                listObject.Add(obj);
        }

        return listObject;
    }
}
