using System;
using UnityEngine;

public interface ICivilization
{
    CivilizationScriptable DataBase { get; }
    void Assign(CivilizationScriptable civData);
    void ExecuteOnTime(float deltaTime);
}

public interface ICivilizationBase
{
    event Action<float> ExecuteOnTimeEvent;
    bool IsOpen { get; }
    Vector2 PositionCiv { get; }
    CivilizationScriptable DataBase { get; }
    CivilizationData CivData { get; }
    ScannerPlanets ScanerPlanets { get; }
    Science ScienceCiv { get; }
    Industry IndustryCiv { get; }

    void ExicuteScanning();
    void ExicuteSciencePoints(int sciencePoints);
    void ExicuteIndustryPoints(int points, float pointProc);
}

public interface ICivilizationAl : ICivilization
{
    void Open();
}

public interface ICivilizationPlayer : ICivilization
{
    Science ScienceCiv { get; }
}