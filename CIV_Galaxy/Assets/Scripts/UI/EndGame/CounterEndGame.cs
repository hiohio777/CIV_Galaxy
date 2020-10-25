using System;
using UnityEngine;
using UnityEngine.UI;

public class CounterEndGame : RegisterMonoBehaviour
{
    [SerializeField] private EndGameUI endGameUIPrefab;
    private int _endGametime;
    private float _secunds = 0;
    private IGalaxyUITimer _galaxyUITimer;
    private Text _messadgeTime;

    public void Start()
    {
        _galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();

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
                _galaxyUITimer.ExecuteOfTime -= ExecuteOnTimeEvent;
                _messadgeTime.text = string.Empty;
                Instantiate(endGameUIPrefab).Show();
            }
        }
    }


}
