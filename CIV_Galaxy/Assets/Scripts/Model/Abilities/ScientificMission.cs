using System.Collections.Generic;
using UnityEngine;

public class ScientificMission : AttackerAbility
{
    [SerializeField, Header("Прогресс науки"), Range(0, 500)] private int addProgressScienty;

    public override bool ApplyAl(Diplomacy diplomacyCiv)
    {
        var target = diplomacyCiv.FindFriend();
        if (target != null)
        {
            StartAbility(target); // Цель найдена
            return true;
        }

        return false;
    }
    public void AddBonus_ProgressScienty(int bonus)
    {
        addProgressScienty += bonus;
        if (addProgressScienty < 0) addProgressScienty = 0;
    }

    public override void SelectedApplayPlayer(List<ICivilizationAl> civilizationsTarget)
    {
        foreach (var item in civilizationsTarget)
        {
            if (item.IsOpen == false) continue;

            if (item.DiplomacyCiv.GetRelationsPlayer() == DiplomaticRelationsEnum.Friendship)
                item.EnableFrame(Color.green);
        }
    }

    public override bool Apply(ICivilization civilizationTarget)
    {
        var civ = civilizationTarget as ICivilizationAl;
        if (civ.DiplomacyCiv.GetRelationsPlayer() == DiplomaticRelationsEnum.Friendship)
        {
            StartAbility(civilizationTarget);
            return true;
        }

        return false;
    }

    protected override void Finall(IUnitAbility unit, ICivilization civilizationTarget)
    {
        // Получение очков исследований
        ThisCivilization.ScienceCiv.AddProgress(addProgressScienty);
        civilizationTarget.ScienceCiv.AddProgress(addProgressScienty / 2f);
    }

    public override string GetInfo(bool isPlayer = true)
    {
        string info = string.Empty;

        if (isPlayer)
        {
            info += GetInfoCountUnits;
            info += $"{LocalisationGame.Instance.GetLocalisationString("science_progress")}: <color=lime>{addProgressScienty}%</color>";
        }

        return info;
    }
}