using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyUITimer : MonoBehaviour, IGalaxyUITimer
{
    [SerializeField] private Button buttonPause;
    [SerializeField] private Text textTimer;
    [SerializeField, Space(10)] private float lengthOfYear = 2;
    [SerializeField] private float speedGame = 1;

    private int years;
    public bool IsPause { get; set; }
    private Action<float> execute;

    public void StopTimer()
    {
        SetColorButtonPause(IsPause = true);
        StopAllCoroutines();
        execute = null;
    }

    public void StartTimer(Action<float> execute)
    {
        this.execute = execute;
        SetColorButtonPause(IsPause = true);

        StartCoroutine(RunTimer());

        textTimer.text = (years = 0).ToString();
    }

    public void SetPause(bool isPause) => SetColorButtonPause(IsPause = isPause);
    private void OnPause() => SetColorButtonPause(IsPause = !IsPause);
    private void SetColorButtonPause(bool active)
    {
        if (active)
            buttonPause.image.color = new Color(1, 1, 1, 1);
        else
            buttonPause.image.color = new Color(0.5f, 0.5f, 0.5f, 1);
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
                }

                execute.Invoke(current);
            }

            yield return null;
        }
    }

    private void Awake()
    {
        buttonPause.onClick.AddListener(OnPause);
        textTimer.text = "0";
    }
}

public interface IGalaxyUITimer
{
    bool IsPause { get; set; }
    void SetPause(bool isPause);
    void StopTimer();
    void StartTimer(Action<float> execute);
}