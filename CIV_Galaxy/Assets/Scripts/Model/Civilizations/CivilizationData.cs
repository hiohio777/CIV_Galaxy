using System;
using UnityEngine;

public class CivilizationData
{
    private int _planets;
    private float _dominationPoints;

    private ICivilization _civilization;
    private LeaderQualifier _leaderQualifier;

    public CivilizationData(IGalaxyUITimer galaxyUITimer, LeaderQualifier leaderQualifier)
    {
        galaxyUITimer.ExecuteYears += ProgressDominance;
        this._leaderQualifier = leaderQualifier;
    }

    public int Planets { get => _planets; set { _planets = value; _civilization.CivUI.SetCountPlanet(_planets); } }
    public float DominationPoints {
        get => _dominationPoints; set {
            _civilization.CivUI.SetCountDominationPoints(_dominationPoints = value);
            _leaderQualifier.DefineLeader(_civilization);
        }
    }

    public event Func<float> GetIndustryPoints;

    //Бонусы
    public float GrowthDominancePlanetsBonus { get; set; } = 0; // Бонус роста доминирования от планет
    public float GrowthDominanceIndustryBonus { get; set; } = 0; // Бонус роста доминирования от Индустрии
    public float GrowthDominanceOverallBonus { get; set; } = 0; // Бонус роста доминирования(общий в процентах к годовому приросту)

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        _planets = this._civilization.DataBase.Base.Planets;
        DominationPoints = this._civilization.DataBase.Base.DominationPoints;

    }

    public void AddPlanet(IPlanet planet)
    {
        Planets++;
        planet.Destroy();
    }

    public float GetPointsFromPlanets => _planets * (_civilization.DataBase.Base.GrowthDominancePlanets + GrowthDominancePlanetsBonus);
    public float GetPointsFromIndustry => _planets * (GetIndustryPoints.Invoke() * (_civilization.DataBase.Base.GrowthDominanceIndustry + GrowthDominanceIndustryBonus));
    public float GetPointsFromBonus => GetPointsFromPlanets + GetPointsFromIndustry;
    public float GetPointsAll => GetPointsFromBonus + GetPointsFromBonus * (_civilization.DataBase.Base.GrowthDominanceOverall + GrowthDominanceOverallBonus);

    private void ProgressDominance()
    {
        // подсчёт
        DominationPoints += GetPointsAll;
    }
}
