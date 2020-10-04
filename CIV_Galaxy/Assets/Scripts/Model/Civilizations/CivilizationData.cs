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
            _dominationPoints = value;
            _leaderQualifier.DefineLeader(_civilization);
        }
    }

    //Бонусы
    private float _gdPlanets = 0;
    public int GDPlanets { get => (int)(_gdPlanets * 100); set => _gdPlanets = value / 100f; }
    private float _gdIndustry = 0;
    public int GDIndustry { get => (int)(_gdIndustry * 100); set => _gdIndustry = value / 100f; }
    private float _gdOverall = 0;
    public int GDOverall { get => (int)(_gdOverall * 100); set => _gdOverall = value / 100f; }

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        _planets = this._civilization.DataBase.Base.Planets;

        DominationPoints = _civilization.DataBase.Base.DominationPoints;
        GDPlanets = _civilization.DataBase.Base.GDPlanets;
        GDIndustry = _civilization.DataBase.Base.GDIndustry;
        GDOverall = _civilization.DataBase.Base.GDOverall;
    }

    public void AddPlanet(IPlanet planet)
    {
        Planets++;
        planet.Destroy();
    }

    public void AddDominance(float count)
    {
        DominationPoints += count;
        _civilization.CivUI.SetCountDominationPoints(DominationPoints, count);
    }

    public float GetPointsFromPlanets => _planets * _gdPlanets;
    public float GetPointsFromIndustry => _planets * (_civilization.IndustryCiv.Points * _gdIndustry);
    public float GetPointsFromBonus => GetPointsFromPlanets + GetPointsFromIndustry;
    public float GetPointsAll => GetPointsFromBonus + GetPointsFromBonus * _gdOverall;

    private void ProgressDominance()
    {
        // подсчёт
        float count = GetPointsAll;
        DominationPoints += count;

        _civilization.CivUI.SetCountDominationPoints(DominationPoints, count);
    }
}
