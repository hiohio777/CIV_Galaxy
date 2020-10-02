using UnityEngine;
using System;
using UnityEngine.UI;
using System.Globalization;

[Serializable]
public class CivilizationUI
{
    [SerializeField] private Image art;
    [SerializeField] private Sprite spriteEmpty;

    [SerializeField, Space(10)] private GameObject panel;
    [SerializeField, Space(10)] private Text name;
    [SerializeField] private Image dominatorIcon;
    [SerializeField] private Text countPlanet;
    [SerializeField] private Text countDominationPoints;

    [SerializeField] private Image indicator;
    [SerializeField] private Animator animatorScanerEffect;

    public ValueChangeEffectFactory valueChangeEffectFactory;

    public void SetIndustryPoints(float points)
    {
        indicator.fillAmount = points;

        if (points >= 1) indicator.color = Color.green;
        else indicator.color = Color.red;
    }

    public void SetCountPlanet(int count)
    {
        countPlanet.text = count.ToString();
    }

    public void SetCountDominationPoints(float dominationPoints, float count)
    {
        Action act = () => countDominationPoints.text = ((int)dominationPoints).ToString("#,#");
        valueChangeEffectFactory.GetEffect().Display(countDominationPoints.transform, ((int)count).ToString("#,#"), count > 0, act);
    }

    public void SetAdvancedDomination(LeaderEnum leaderEnum)
    {

        switch (leaderEnum)
        {
            case LeaderEnum.Lagging:
                dominatorIcon.enabled = false;
                countDominationPoints.color = new Color(0.6f, 0.6f, 0, 1);
                break;
            case LeaderEnum.Advanced:
                dominatorIcon.enabled = true;
                countDominationPoints.color = new Color(1f, 1f, 0, 1);
                dominatorIcon.color = new Color(1, 1, 0, 0.15f);
                break;
            case LeaderEnum.Leader:
                dominatorIcon.enabled = true;
                countDominationPoints.color = new Color(1, 1, 0, 1);
                dominatorIcon.color = new Color(1, 1, 0, 0.75f);
                break;
        }
    }

    public void ScanerEffect() => animatorScanerEffect.SetTrigger("ScanerEffect");

    /// <summary>
    /// Закрыть(скрыть) цивилизацию - Неизвесна игроку
    /// </summary>
    public void Close()
    {
        art.sprite = spriteEmpty;
        art.color = new Color(1, 1, 1, 0.4f);
        if (panel != null) panel.SetActive(false);
    }

    /// <summary>
    ///  Открыть(показать) цивилизацию на экране - Извесна игроку
    /// </summary>
    public void Assign(ICivilization civilization)
    {
        art.sprite = civilization.DataBase.Icon;
        art.color = new Color(1, 1, 1, 1);
        if (panel != null) panel.SetActive(true);
        name.text = civilization.DataBase.Name;

        countDominationPoints.text = ((int)civilization.CivData.DominationPoints).ToString("#,#");

        SetCountPlanet(civilization.CivData.Planets);
        SetIndustryPoints(civilization.IndustryCiv.Points);
        SetAdvancedDomination(civilization.IsLider);
    }
}

public enum LeaderEnum { Advanced, Lagging, Leader }