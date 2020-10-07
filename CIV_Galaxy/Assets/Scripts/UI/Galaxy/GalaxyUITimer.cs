using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GalaxyUITimer : MonoBehaviour, IGalaxyUITimer
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

    public float GetYears => years;
    public float GetSpeed => speedGame;

    [Inject]
    public void InjectCivilizationPlayer(ICivilizationPlayer civilizationPlayer)
    {
        this._civilizationPlayer = civilizationPlayer;

        buttonPause.onClick.AddListener(OnPause);
        buttonUpSpeed.onClick.AddListener(OnUpSpeed);
        buttonDownSpeed.onClick.AddListener(OnDownSpeed);

        textSpeed.text = $"{speedGame}x";
        SetPause(true);
        StartCoroutine(RunTimer());
        textTimer.text = (years = 0).ToString();
    }

    public void SetPause(bool active, string message = "pause")
    {
        if (IsPause = active)
        {
            if(message != string.Empty)
            {
                messagePause.SetActive(true);
                messagePause.SetKey(message);
            }
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
        switch (speedGame)
        {
            case 1f: speedGame = 0.5f; break;
            case 2f: speedGame = 1f; break;
        }

        textSpeed.text = $"{speedGame}x";
    }
    private void OnUpSpeed()
    {

        switch (speedGame)
        {
            case 1f: speedGame = 2f; break;
            case 0.5f: speedGame = 1f; break;
        }

        textSpeed.text = $"{speedGame}x";
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
    void SetPause(bool isPause, string message = "pause");
}