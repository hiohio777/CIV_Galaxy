using System;
using System.Collections;
using UnityEngine;

public class LoaderDataGame
{
    private static bool isLoad = false;

    public void Load(Action startGame, Action restartMainScene)
    {
        if (isLoad)
        {
            Debug.Log("Data already loaded!");
            restartMainScene.Invoke();
            return;
        }

        // Загрузка данных
        Debug.Log("Data Loaded!");
        isLoad = true;
        startGame.Invoke();
    }

    public void Save(Action quit)
    {
        Debug.Log("Data saved!");
        quit.Invoke();
    }
}
