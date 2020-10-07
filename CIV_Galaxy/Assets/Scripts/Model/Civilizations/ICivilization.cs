using UnityEngine;

public interface ICivilization
{
    CivilizationUI CivUI { get; }
    CivilizationScriptable DataBase { get; }
    Vector2 PositionCiv { get; }
    CivilizationData CivData { get; }
    Scanner ScanerCiv { get; }
    Science ScienceCiv { get; }
    Industry IndustryCiv { get; }
    Ability AbilityCiv { get; }
    GalacticEventGenerator EventGenerator { get; }

    void Assign(CivilizationScriptable civData);
    bool IsOpen { get; }
    LeaderEnum IsLider { get; }

    void ExicuteScanning();
    void ExicuteSciencePoints(int sciencePoints);
    void ExicuteIndustryPoints(float points);
    void DefineLeader(LeaderEnum leaderEnum);
}

public interface ICivilizationAl : ICivilization
{
    Diplomacy DiplomacyCiv { get; }
    void Open();
    void SetSetDiplomaticRelations(DiplomaticRelationsEnum relations);

    void EnableFrame(Color color);
    void TurnOffFrame();
}

public interface ICivilizationPlayer : ICivilization
{
    AbilityUI SelectedAbility { get; set; }
}