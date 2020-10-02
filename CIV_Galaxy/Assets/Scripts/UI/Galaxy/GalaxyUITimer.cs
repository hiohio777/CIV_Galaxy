using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GalaxyUITimer : MonoBehaviour, IGalaxyUITimer
{
    [SerializeField] private Button buttonPause, buttonAssingSpeed;
    [SerializeField] private Image speedIcon1, speedIcon2;
    [SerializeField] private Text textTimer, TextSpeed;
    [SerializeField, Space(10)] private float lengthOfYear = 4;
    [SerializeField] private Sprite playIcon, pauseIcon;

    private float speedGame = 0.5f;
    private int years;
    private ICivilizationPlayer _civilizationPlayer;

    public bool IsPause { get; private set; }
    public event Action ExecuteYears; // События происходящие каждый год
    public event Action<float> ExecuteOfTime = delegate { }; // Постоянный апдейт
    public event Action<bool> PauseAct; // События вызываются когда игра ставится или снимается с паузы(нужно для контроля за анимациями)

    public float GetYears => years;
    public float GetSpeed => speedGame;

    [Inject]
    public void InjectCivilizationPlayer(ICivilizationPlayer civilizationPlayer)
    {
        this._civilizationPlayer = civilizationPlayer;

        buttonPause.onClick.AddListener(OnPause);
        buttonAssingSpeed.onClick.AddListener(OnAssingSpeed);

        TextSpeed.text = $"{speedGame}x";
        SetPause(true);
        StartCoroutine(RunTimer());
        textTimer.text = (years = 0).ToString();
        OnAssingSpeed();
    }

    public void SetPause(bool active)
    {
        if (IsPause = active)
            buttonPause.image.sprite = playIcon;
        else
            buttonPause.image.sprite = pauseIcon;

        PauseAct?.Invoke(IsPause);
    }

    private void OnAssingSpeed()
    {
        speedIcon1.enabled = speedIcon2.enabled = false;
        switch (speedGame)
        {
            case 0.5f: speedGame = 1f; speedIcon1.enabled = true; break;
            case 1f: speedGame = 2f; speedIcon1.enabled = speedIcon2.enabled = true; break;
            case 2f: speedGame = 0.5f; break;
        }
        TextSpeed.text = $"{speedGame}x";
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
    bool IsPause { get; }
    float GetYears { get; }
    float GetSpeed { get; }
    void SetPause(bool isPause);
}