using System;
using UnityEngine;

public interface ICivilization
{
    void Assign(CivilizationScriptable civData);
    bool IsOpen { get; }

    event Action<float> ExecuteOnTimeEvent;
    void ExecuteOnTime(float deltaTime);

    CivilizationScriptable DataBase { get; }
    Vector2 PositionCiv { get; }
    CivilizationData CivData { get; }
    Scanner ScanerPlanets { get; }
    Science ScienceCiv { get; }
    Industry IndustryCiv { get; }

    void ExicuteScanning();
    void ExicuteSciencePoints(int sciencePoints);
    void ExicuteIndustryPoints(float points);
    void ExicuteAbility(IAbility ability);
    void DefineLeader();
}

public interface ICivilizationAl : ICivilization
{
    void Open();
}

public interface ICivilizationPlayer : ICivilization
{
    AbilityUI SelectAbility { get; set; }
}