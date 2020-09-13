﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class SciencePanelUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image indicator;
    [SerializeField] private Text points;

    private SciencePlayerUI _sciencePlayerUI;

    [Inject]
    public void Inject(SciencePlayerUI sciencePlayerUI)
    {
        this._sciencePlayerUI = sciencePlayerUI;

        points.text = "0";
    }

    public void SetSciencePoints(int points)
    {
        this.points.text = points.ToString();
    }

    public void ProgressEvent(float rogress)
    {
        indicator.fillAmount = rogress / 100;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _sciencePlayerUI.Enable();
    }
}
