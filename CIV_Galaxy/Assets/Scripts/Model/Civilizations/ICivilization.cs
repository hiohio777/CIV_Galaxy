using UnityEngine;

public interface ICivilization
{
    CivilizationScriptable CivData { get; set; }
    void Assign(CivilizationScriptable civData);
    void ExecuteOnTime(float deltaTime);
}

public interface ICivilizationAl : ICivilization
{
    void Open();
}

public interface ICivilizationPlayer : ICivilization
{
    string CurrentCivilization { get; set; }
}