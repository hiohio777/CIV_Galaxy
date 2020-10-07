using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CounterEndGame : MonoBehaviour
{
    private int _endGametime;
    private float _secunds = 0;
    private IGalaxyUITimer _galaxyUITimer;
    private EndGameUI _endGameUI;
    private Text _messadgeTime;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer, EndGameUI endGameUI)
    {
        this._galaxyUITimer = galaxyUITimer;
        this._endGameUI = endGameUI;

        _messadgeTime = GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void Show(int endGametime)
    {
        gameObject.SetActive(true);
        this._galaxyUITimer.ExecuteOfTime += ExecuteOnTimeEvent;

        _endGametime = endGametime;
        _secunds = 0;
        _messadgeTime.text = TimeSpan.FromSeconds(_endGametime).ToString(@"mm\:ss");
    }

    private void ExecuteOnTimeEvent(float deltaTime)
    {
        _secunds += deltaTime;

        if (_secunds >= 1)
        {
            _secunds = 0;
            _messadgeTime.text = TimeSpan.FromSeconds(_endGametime -= 1).ToString(@"mm\:ss");

            if (_endGametime <= 0)
            {
                // Время истекло, конец игры
                _messadgeTime.text = string.Empty;
                _galaxyUITimer.SetPause(true);
                _endGameUI.Show();
            }
        }
    }


}
