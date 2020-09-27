using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICivilization
{
    CivilizationUI CivUI { get; }
    CivilizationScriptable DataBase { get; }
    Vector2 PositionCiv { get; }
    CivilizationData CivData { get; }
    Scanner ScanerPlanets { get; }
    Science ScienceCiv { get; }
    Industry IndustryCiv { get; }
    Ability AbilityCiv { get; }

    void Assign(CivilizationScriptable civData);
    bool IsOpen { get; }
    LeaderEnum IsLider { get; }

    void ExicuteScanning();
    void ExicuteSciencePoints(int sciencePoints);
    void ExicuteIndustryPoints(float points);
    void ExicuteAbility(IAbility ability);
    void DefineLeader(LeaderEnum leaderEnum);
}

public interface ICivilizationAl : ICivilization
{
    Diplomacy DiplomacyCiv { get; }
    void Open();
    void SetSetDiplomaticRelations(DiplomaticRelationsEnum relations);
}

public interface ICivilizationPlayer : ICivilization
{
    AbilityUI SelectedAbility { get; set; }
}