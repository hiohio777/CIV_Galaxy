using System;
using System.Collections;
using UnityEngine;

public class LoaderDataGame
{
    public bool IsLoad { get; private set; }

    public void Load(Action startGame)
    {
        if (IsLoad)
        {
            Debug.Log("Data already loaded!");
            return;
        }

        // Загрузка данных
        Debug.Log("Data Loaded!");
        IsLoad = true;
        startGame.Invoke();
    }

    public void Save(Action quit)
    {
        Debug.Log("Data saved!");
        quit.Invoke();
    }
}
