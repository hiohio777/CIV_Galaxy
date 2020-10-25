using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if (_instance != null)
            {
                return _instance;
            }

            _instance = new GameObject("(singleton) SoundManager").AddComponent<GameManager>().Initialize();
            return _instance;
        }
    }

    private AudioPlayer _audioPlayer;
    private List<object> registrObject = new List<object>();

    public void PlayNewMusic(AudioClip clip) => _audioPlayer.PlayNewMusic(clip);
    public void PlayUISound(AudioClip soundEffect, float volume = 1f) => _audioPlayer.PlayUISound(soundEffect, volume);

    private GameManager Initialize()
    {
        _audioPlayer = Instantiate(Resources.Load<AudioPlayer>("AudioPlayer"));

        DontDestroyOnLoad(gameObject);

        return this;
    }

    public void Clear() => registrObject.Clear();

    public void Register(object obj) => registrObject.Add(obj);

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
