using System;
using UnityEngine;

public class LoaderDataGame
{
    private static bool isLoad = false;

    public void Load(Action startGame, Action restartMainScene)
    {
        if (isLoad)
        {
            restartMainScene.Invoke();
            return;
        }

        // Загрузка данных
        isLoad = true;
        startGame.Invoke();
    }

    public void Save(Action quit)
    {
        quit.Invoke();
    }
}
