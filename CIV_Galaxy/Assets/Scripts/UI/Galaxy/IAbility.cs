using System;
using UnityEngine;

public interface IAbility
{
    event Action<float> ProgressEvent;
    int Id { get; }
    bool IsActive { get; set; }
    bool IsReady { get; set; }
    Sprite Art { get; }
    Sprite Fon { get; }
    Sprite Frame { get; }
    float AccelerationBonus { get; set; }
    void Initialize(int id, ICivilization civilization);
    void ApplyAl();
    void Apply();
}