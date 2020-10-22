﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyUITimer : RegisterMonoBehaviour, IGalaxyUITimer
{
    [SerializeField] private Button buttonPause, buttonUpSpeed, buttonDownSpeed;
    [SerializeField] private Text textTimer, textSpeed;
    [SerializeField] private LocalisationText messagePause;
    [SerializeField, Space(10)] private float lengthOfYear = 4;
    [SerializeField] private Sprite playIcon, pauseIcon;

    private float speedGame = 1f;
    private int years;
    private ICivilizationPlayer _civilizationPlayer;

    public bool IsPause { get; private set; }
    public event Action ExecuteYears; // События происходящие каждый год
    public event Action<float> ExecuteOfTime = delegate { }; // Постоянный апдейт
    public event Action<bool> PauseAct; // События вызываются когда игра ставится или снимается с паузы(нужно для контроля за анимациями)
    public event Action<float> SpeedAct;

    public float GetYears => years;
    public float GetSpeed => speedGame;

    public void Start()
    {
        this._civilizationPlayer = GetRegisterObject<ICivilizationPlayer>();

        buttonPause.onClick.AddListener(OnPause);
        buttonUpSpeed.onClick.AddListener(OnUpSpeed);
        buttonDownSpeed.onClick.AddListener(OnDownSpeed);

        textSpeed.text = $"{speedGame}x";
        SetPause(true);
        StartCoroutine(RunTimer());
        textTimer.text = (years = 0).ToString();
    }

    public void SetPause(bool active, string message = "pause") => SetPause(active, Color.red, message);
    public void SetPause(bool active, Color color, string message = "pause")
    {
        if (IsPause = active)
        {
            if (message != string.Empty)
            {
                messagePause.SetActive(true);
                messagePause.SetKey(message);
                messagePause.Color = color;
            }
            else messagePause.SetActive(false);

            buttonPause.image.sprite = playIcon;
        }
        else
        {
            messagePause.SetActive(false);
            buttonPause.image.sprite = pauseIcon;
        }

        PauseAct?.Invoke(IsPause);
    }

    private void OnDownSpeed()
    {
        buttonUpSpeed.image.enabled = true;
        switch (speedGame)
        {
            case 1f: speedGame = 0.5f; buttonDownSpeed.image.enabled = false; break;
            case 2f: speedGame = 1f; break;
        }

        textSpeed.text = $"{speedGame}x";
        SpeedAct?.Invoke(speedGame);
    }
    private void OnUpSpeed()
    {
        buttonDownSpeed.image.enabled = true;
        switch (speedGame)
        {
            case 1f: speedGame = 2f; buttonUpSpeed.image.enabled = false; break;
            case 0.5f: speedGame = 1f; break;
        }

        textSpeed.text = $"{speedGame}x";
        SpeedAct?.Invoke(speedGame);
    }

    private void OnPause()
    {
        if (_civilizationPlayer.SelectedAbility != null)
        {
            _civilizationPlayer.SelectedAbility.Select(false);
            _civilizationPlayer.SelectedAbility = null;
        }

        SetPause(!IsPause);
    }

    private IEnumerator RunTimer()
    {
        float timeSecond = 0;

        while (true)
        {
            if (IsPause == false)
            {
                float current = Time.deltaTime * speedGame;
                timeSecond += current;
                if (timeSecond > lengthOfYear)
                {
                    timeSecond = 0;
                    textTimer.text = (years++).ToString();
                    ExecuteYears.Invoke();
                }

                ExecuteOfTime.Invoke(current);
            }

            yield return null;
        }
    }
}

public interface IGalaxyUITimer
{
    event Action ExecuteYears; // События происходящие каждый год
    event Action<float> ExecuteOfTime;
    event Action<bool> PauseAct;
    event Action<float> SpeedAct;
    bool IsPause { get; }
    float GetYears { get; }
    float GetSpeed { get; }

    void SetPause(bool isPause, string message = "pause");
    void SetPause(bool active, Color color, string message = "pause");
}