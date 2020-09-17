using System;
using UnityEngine;

public class CivilizationData
{
    public BaseData baseData { get; private set; }
    private int _planets;

    private float _dominationPoints;

    private ICivilizationDataUI _dataUI;

    public CivilizationData(IGalaxyUITimer galaxyUITimer)
    {
        galaxyUITimer.ExecuteYears += ProgressDominance;
    }

    public int Planets { get => _planets; set { _planets = value; _dataUI.SetCountPlanet(_planets); } }
    public float DominationPoints { get => _dominationPoints; set { _dominationPoints = value; _dataUI.SetCountDominationPoints(_dominationPoints); } }

    public event Func<float> GetIndustryPoints;

    //Бонусы
    public float GrowthDominancePlanetsBonus { get; set; } = 0; // Бонус роста доминирования от планет
    public float GrowthDominanceIndustryBonus { get; set; } = 0; // Бонус роста доминирования от Индустрии
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
        float dominanceIndustry = _planets * (GetIndustryPoints.Invoke() * (baseData.GrowthDominanceIndustry + GrowthDominanceIndustryBonus));

        float dominance = dominancePlanets + dominanceIndustry; // Общий годовой рост
        float dominanceOverall = dominance * (baseData.GrowthDominanceOverall + GrowthDominanceOverallBonus); // Бонус к годовому росту

        // Финальный подсчёт
        DominationPoints += (dominance + dominanceOverall);
    }
}
