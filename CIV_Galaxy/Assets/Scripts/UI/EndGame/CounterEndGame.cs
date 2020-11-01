using System;
using UnityEngine;
using UnityEngine.UI;

public class CounterEndGame : NoRegisterMonoBehaviour
{
    [SerializeField, Space(10)] private AudioClip musicEndGameTime;
    [SerializeField] private EndGameUI endGameUIPrefab;
    private int _endGametime;
    private float _secunds = 0;
    private IGalaxyUITimer _galaxyUITimer;
    private Text _messadgeTime;

    private bool isActive = false; // Был ли обект активирован

    public void Start() => gameObject.SetActive(isActive);

    public void Show(int endGametime)
    {
        if (isActive == false)
        {
            _galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
            _messadgeTime = GetComponent<Text>();
            isActive = true;
        }

        gameObject.SetActive(true);
        this._galaxyUITimer.ExecuteOfTime += ExecuteOnTimeEvent;

        _endGametime = endGametime;
        _secunds = 0;
        _messadgeTime.text = TimeSpan.FromSeconds(_endGametime).ToString(@"mm\:ss");
        PlayNewMusic(musicEndGameTime);
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
