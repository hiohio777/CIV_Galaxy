using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyUITimer : MonoBehaviour
{
    [SerializeField] private Button buttonPause;
    [SerializeField] private Text textTimer;
    [SerializeField, Space(10)] private float lengthOfYear = 2;

    private Image imageButtonPause;
    private int years; 
    private bool IsPause;
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

    public void OnPause() => SetColorButtonPause(IsPause = !IsPause);
    private void SetColorButtonPause(bool active)
    {
        if (active)
            imageButtonPause.color = new Color(1, 1, 1, 1);
        else
            imageButtonPause.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    private IEnumerator RunTimer()
    {
        float timeSecond = 0;

        while (true)
        {
            if (IsPause == false)
            {
                timeSecond += Time.deltaTime;
                if (timeSecond > lengthOfYear)
                {
                    timeSecond = 0;
                    textTimer.text = (years++).ToString();
                }

                execute.Invoke(Time.deltaTime);
            }

            yield return null;
        }
    }

    private void Awake()
    {
        imageButtonPause = buttonPause.GetComponent<Image>();
        buttonPause.onClick.AddListener(OnPause);

        textTimer.text = "0";
    }
}

