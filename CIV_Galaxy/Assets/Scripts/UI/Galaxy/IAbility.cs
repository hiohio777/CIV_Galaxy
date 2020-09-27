using System;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    event Action<float> ProgressEvent;
    string Name { get; }
    bool IsActive { get; set; }
    bool IsReady { get; set; }
    Sprite Art { get; }
    Sprite Fon { get; }
    Sprite Frame { get; }
    float GetCost { get; }

    void Initialize(ICivilization civilization);
    bool ApplyAl(Diplomacy diplomacyCiv);
    void SelectedApplayPlayer(List<ICivilizationAl> civilizationsTarget); // Выделение доступных целей
    bool Apply(ICivilization civilizationTarget);
    void ExecuteOnTimeEvent(float deltaTime);
}