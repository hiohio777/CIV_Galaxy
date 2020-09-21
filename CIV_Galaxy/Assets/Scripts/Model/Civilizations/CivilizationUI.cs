using UnityEngine;
using System;
using UnityEngine.UI;
using System.Globalization;

[Serializable]
public class CivilizationUI : ICivilizationDataUI
{
    [SerializeField] private Image art;
    [SerializeField] private Sprite spriteEmpty;

    [SerializeField, Space(10)] private GameObject panel;
    [SerializeField, Space(10)] private Text name;
    [SerializeField] private Text countPlanet;
    [SerializeField] private Text countDominationPoints;

    [SerializeField] private Image indicator, imageFullIndustry;

    private CivilizationScriptable civData;

    public void SetIndustryPoints(float points)
    {
        indicator.fillAmount = points;

        if (imageFullIndustry != null)
        {
            // Для Игрока
            if (points >= 1) imageFullIndustry.gameObject.SetActive(true);
            else imageFullIndustry.gameObject.SetActive(false);
        }
        else
        {
            // Для Al
            if (points >= 1) indicator.color = Color.blue;
            else indicator.color = Color.red;
        }
    }

    public void SetCountPlanet(int count)
    {
        countPlanet.text = count.ToString();
    }

    public void SetCountDominationPoints(float dominationPoints)
    {
        countDominationPoints.text = ((int)dominationPoints).ToString("#,#", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Закрыть(скрыть) цивилизацию - Неизвесна игроку
    /// </summary>
    public void Close()
    {
        art.sprite = spriteEmpty;
        if (panel != null) panel.SetActive(false);
    }

    /// <summary>
    ///  Открыть(показать) цивилизацию на экране - Извесна игроку
    /// </summary>
    public void Assign(CivilizationScriptable civData)
    {
        this.civData = civData;

        art.sprite = this.civData.Icon;
        if (panel != null) panel.SetActive(true);
        name.text = this.civData.Name;
    }
}

public interface ICivilizationDataUI
{
    void SetCountDominationPoints(float dominationPoints);
    void SetCountPlanet(int count);
}