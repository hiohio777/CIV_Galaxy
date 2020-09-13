using System;
using UnityEngine;

public interface ICivilization
{
    CivilizationScriptable CivDataBase { get; }
    void Assign(CivilizationScriptable civData);
    void ExecuteOnTime(float deltaTime);
}

public interface ICivilizationBase
{
    event Action<float> ExecuteOnTimeEvent;
    bool IsOpen { get; }
    Vector2 PositionCiv { get; }
    CivilizationScriptable CivDataBase { get; }
    CivilizationData CivData { get; }
    ScannerPlanets ScanerPlanets { get; }
    Science ScienceCiv { get; }

    void ExicuteScanning();
    void ExicuteSciencePoints(int sciencePoints);
}

public interface ICivilizationAl : ICivilization
{
    void Open();
}

public interface ICivilizationPlayer : ICivilization
{
    Science ScienceCiv { get; }
}