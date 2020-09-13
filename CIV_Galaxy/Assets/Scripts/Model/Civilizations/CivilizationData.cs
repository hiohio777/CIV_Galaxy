using System;
using UnityEngine;

public class CivilizationData
{
    public BaseData baseData { get; private set; }
    private int _planets;

    private int _dominationPoints;

    private ICivilizationDataUI _dataUI;

    public CivilizationData(IGalaxyUITimer galaxyUITimer)
    {
        galaxyUITimer.ExecuteYears += ProgressDominance;
    }

    public int Planets { get => _planets; set { _planets = value; _dataUI.SetCountPlanet(_planets); } }
    public int DominationPoints { get => _dominationPoints; set { _dominationPoints = value; _dataUI.SetCountDominationPoints(_dominationPoints); } }

    public event Func<float> GetSciencePoints, GetIndustryPoints;

    //Бонусы
    public float GrowthDominancePlanetsBonus { get; set; } = 0; // Бонус роста доминирования от планет
    public float GrowthDominanceIndustryBonus { get; set; } = 0; // Бонус роста доминирования от Индустрии
    public float GrowthDominanceScienceBonus { get; set; } = 0; // Бонус роста доминирования от науки
    public float GrowthDominanceOverallBonus { get; set; } = 0; // Бонус роста доминирования(общий в процентах к годовому приросту)

    public void Initialize(CivilizationScriptable civData, ICivilizationDataUI dataUI)
    {
        (this.baseData, this._dataUI) = (civData.Base, dataUI);

        Planets = this.baseData.Planets;
        DominationPoints = this.baseData.DominationPoints;
    }

    public void AddPlanet(IPlanet planet)
    {
        Planets++;
        planet.Destroy();
    }

    private void ProgressDominance()
    {
        float dominancePlanets = _planets * (baseData.GrowthDominancePlanets + GrowthDominancePlanetsBonus);

        float dominanceIndustry = _planets * (GetIndustryPoints.Invoke() / 100);

        float dominance = dominancePlanets + dominanceIndustry; // Общий годовой рост
        float dominanceOverall = dominance * (baseData.GrowthDominanceOverall + GrowthDominanceOverallBonus); // Бонус к годовому росту

        // Финальный подсчёт
        DominationPoints += (int)(dominance + dominanceOverall);

        Debug.Log($"Planets: {dominancePlanets}, Industry: {dominanceIndustry}({GetIndustryPoints.Invoke() / 10})," +
        $"All: {dominance}, Bonus: {dominanceOverall}, Final: {(int)(dominance + dominanceOverall)}");
    }
}
