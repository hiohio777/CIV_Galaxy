using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    string Name { get; }
    bool IsActive { get; set; }
    Sprite Art { get; }
    Sprite Fon { get; }
    Sprite Frame { get; }
    int CountUses { get; }

    void Initialize(ICivilization civilization);
    bool ApplyAl(Diplomacy diplomacyCiv);
    void SelectedApplayPlayer(List<ICivilizationAl> civilizationsTarget); // Выделение доступных целей
    bool Apply(ICivilization civilizationTarget);
    string GetInfo(bool isPlayer = true);
}