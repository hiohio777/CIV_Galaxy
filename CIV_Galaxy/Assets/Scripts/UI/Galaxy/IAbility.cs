using System;
using UnityEngine;

public interface IAbility
{
    event Action<float> ProgressEvent;
    int Id { get; }
    string Name { get; }
    bool IsActive { get; set; }
    bool IsReady { get; set; }
    Sprite Art { get; }
    Sprite Fon { get; }
    Sprite Frame { get; }
    float GetCost { get; }

    void Initialize(int id, ICivilization civilization);
    bool ApplyAl(Diplomacy diplomacyCiv);
    bool Apply(ICivilization civilizationTarget);
    void ExecuteOnTimeEvent(float deltaTime);
}