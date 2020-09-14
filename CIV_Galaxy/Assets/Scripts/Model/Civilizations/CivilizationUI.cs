using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class CivilizationUI: ICivilizationDataUI
{
    [SerializeField] private Image art;
    [SerializeField] private Sprite spriteEmpty;

    [SerializeField, Space(10)] private Text name;
    [SerializeField] private Text countPlanet;
    [SerializeField] private Text countDominationPoints;

    private CivilizationScriptable civData;

    public void SetCountPlanet(int count)
    {
        countPlanet.text = count.ToString();
    }

    public void SetCountDominationPoints(float dominationPoints)
    {
        countDominationPoints.text = ((int)dominationPoints).ToString();
    }

    /// <summary>
    /// Закрыть(скрыть) цивилизацию - Неизвесна игроку
    /// </summary>
    public void Close()
    {
        art.sprite = spriteEmpty;
        name.gameObject.SetActive(false);
    }

    /// <summary>
    ///  Открыть(показать) цивилизацию на экране - Извесна игроку
    /// </summary>
    public void Assign(CivilizationScriptable civData)
    {
        this.civData = civData;

        art.sprite = this.civData.Icon;
        name.gameObject.SetActive(true);
        name.text = this.civData.Name;
    }
}

public interface ICivilizationDataUI
{
    void SetCountDominationPoints(float dominationPoints);
    void SetCountPlanet(int count);
}