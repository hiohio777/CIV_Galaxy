using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GalaxyUITimer : MonoBehaviour, IGalaxyUITimer
{
    [SerializeField] private Button buttonPause;
    [SerializeField] private Text textTimer;
    [SerializeField, Space(10)] private float lengthOfYear = 4;
    [SerializeField] private float speedGame = 1;

    private int years;
    private ICivilizationPlayer _civilizationPlayer;

    public bool IsPause { get; private set; }
    public event Action ExecuteYears; // События происходящие каждый год
    public event Action<float> ExecuteOfTime = delegate { }; // Постоянный апдейт

    public float GetYears => years;
    public void SetPause(bool isPause) =>  SetColorButtonPause(IsPause = isPause);

    private void OnPause() 
    {
        if (_civilizationPlayer.SelectedAbility != null)
        {
            _civilizationPlayer.SelectedAbility.Select(false);
            _civilizationPlayer.SelectedAbility = null;
        }

        SetColorButtonPause(IsPause = !IsPause);
    }
    private void SetColorButtonPause(bool active)
    {
        if (active)
            buttonPause.image.color = new Color(1, 1, 1, 1);
        else
            buttonPause.image.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    [Inject]
    public void InjectCivilizationPlayer(ICivilizationPlayer civilizationPlayer)
    {
        this._civilizationPlayer = civilizationPlayer;
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

    private void Awake()
    {
        buttonPause.onClick.AddListener(OnPause);

        SetColorButtonPause(IsPause = true);
        StartCoroutine(RunTimer());
        textTimer.text = (years = 0).ToString();
    }
}

public interface IGalaxyUITimer
{
    event Action ExecuteYears; // События происходящие каждый год
    event Action<float> ExecuteOfTime;
    bool IsPause { get;}
    float GetYears { get; }
    void SetPause(bool isPause);
}