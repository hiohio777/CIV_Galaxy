using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICivilization
{
    TypeCivEnum TypeCiv { get; }

    event Action<float> ExecuteOnTimeEvent;

    CivilizationScriptable DataBase { get; }
    Vector2 PositionCiv { get; }
    CivilizationData CivData { get; }
    Scanner ScanerPlanets { get; }
    Science ScienceCiv { get; }
    Industry IndustryCiv { get; }

    void Assign(CivilizationScriptable civData);
    bool IsOpen { get; }
    void ExecuteOnTime(float deltaTime);

    void ExicuteScanning();
    void ExicuteSciencePoints(int sciencePoints);
    void ExicuteIndustryPoints(float points);
    void ExicuteAbility(IAbility ability);
    void DefineLeader();
}

public interface ICivilizationAl : ICivilization
{
    void Open();
    void SetSetDiplomaticRelations(DiplomaticRelationsEnum relations);
}

public interface ICivilizationPlayer : ICivilization
{
    AbilityUI SelectedAbility { get; set; }
}