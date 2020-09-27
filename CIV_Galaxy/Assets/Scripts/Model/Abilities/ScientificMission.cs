using System;
using UnityEngine;

public class ScientificMission : AbilityBase
{
    [SerializeField, Range(0, 100)] private int addProgressScienty = 10;

    private UnitAbilityFactory _unitFactory;

    public void Initialize(UnitAbilityFactory unitFactory)
    {
        (this._unitFactory) = (unitFactory);
    }

    public override bool ApplyAl(Diplomacy diplomacyCiv)
    {
        var target = diplomacyCiv.FindFriend();
        if (target != null)
        {
            StartMission(target); // Цель найдена
            return true;
        }
        
        return false;
    }

    public override bool Apply(ICivilization civilizationTarget)
    {
        var civ = civilizationTarget as ICivilizationAl;
        if (civ.DiplomacyCiv.GetRelationsPlayer() == DiplomaticRelationsEnum.Friendship)
        {
            StartMission(civilizationTarget);
            return true;
        }

        return false;
    }

    private void StartMission(ICivilization civilizationTarget)
    {
        Ready();

        var unit = _unitFactory.GetNewUnit(this, ThisCivilization, civilizationTarget);
        unit.TtransformUnit.up = new Vector2(0, 0) - ThisCivilization.PositionCiv;

        unit.SetScale(1.5f, 0.3f).Run(() => StartAct(unit, civilizationTarget));
    }

    private void StartAct(IUnitAbility unit, ICivilization civilizationTarget)
   => unit.SetScale(1f, 0.2f).Run(() => MoveAct(unit, civilizationTarget));
    private void MoveAct(IUnitAbility unit, ICivilization civilizationTarget)
   => unit.SetPositionBezier(civilizationTarget.PositionCiv, new Vector3(0, 50), new Vector3(0, 50), 5f).Run(() => EndAttack(unit, civilizationTarget));
    private void EndAttack(IUnitAbility unit, ICivilization civilizationTarget)
    => unit.SetScale(0f, 0.2f).Run(() => Finall(unit, civilizationTarget));

    private void Finall(IUnitAbility unit, ICivilization civilizationTarget)
    {
        // Получение очков исследований
        ThisCivilization.ScienceCiv.AddProgress(addProgressScienty);
        civilizationTarget.ScienceCiv.AddProgress(addProgressScienty);

        // Визуализация

        unit.Destroy();
    }
}
