using System;
using UnityEngine;

public class SpaceFleet : AbilityBase
{
    [SerializeField] private float minAttackIndustry, randomAttackIndustry; // Атака по индустрии
    [SerializeField] private int minConquestPlanets, randomConquestPlanets; // Завоевание планет
    private PlanetsFactory _planetsFactory;
    private UnitAbilityFactory _unitFactory;

    public void Initialize(PlanetsFactory planetsFactory, UnitAbilityFactory unitFactory)
    {
        (this._planetsFactory, this._unitFactory) = (planetsFactory, unitFactory);
    }

    public override bool ApplyAl(Diplomacy diplomacyCiv)
    {
        var target = diplomacyCiv.FindEnemy();
        if (target != null)
        {
            StartAttack(target); // Цель найдена
            return true;
        }
        else return false;
    }

    public override bool Apply(ICivilization civilizationTarget)
    {
        StartAttack(civilizationTarget);
        return true;
    }

    // Отправить флот в атаку
    private void StartAttack(ICivilization civilizationTarget)
    {
        Ready();

        var unit = _unitFactory.GetNewUnit(this, ThisCivilization, civilizationTarget);
        unit.TtransformUnit.up = new Vector2(0, 0) - ThisCivilization.PositionCiv;

        unit.SetScale(1.5f, 0.3f).Run(() => StartAct(unit, civilizationTarget));
    }

    private void StartAct(IUnitAbility unit, ICivilization civilizationTarget)
    => unit.SetScale(1f, 0.2f).Run(() => MoveAct(unit, civilizationTarget));
    private void MoveAct(IUnitAbility unit, ICivilization civilizationTarget)
   => unit.SetPositionBezier(civilizationTarget.PositionCiv, new Vector3(0, 0), new Vector3(0, 0), 5f).Run(() => EndAttack(unit, civilizationTarget));
    private void EndAttack(IUnitAbility unit, ICivilization civilizationTarget)
    => unit.SetScale(0f, 0.2f).Run(() => Finall(unit, civilizationTarget));

    private void Finall(IUnitAbility unit, ICivilization civilizationTarget)
    {
        AttackIndustry(civilizationTarget);
        ConquestPlanets(civilizationTarget);

        // Ухудшить отношения, если напал игрок, а отношения были "дружба"

        if(ThisCivilization is ICivilizationPlayer)
            (civilizationTarget as ICivilizationAl).DiplomacyCiv.ChangeRelations(ThisCivilization, +1);

        unit.Destroy();
    }

    // Урон по индустрии
    private void AttackIndustry(ICivilization civilizationTarget)
    {
        civilizationTarget.IndustryCiv.Points -= minAttackIndustry + UnityEngine.Random.Range(0, randomAttackIndustry);
    }

    // Завоевание планет
    private void ConquestPlanets(ICivilization civilizationTarget)
    {
        // Определить количество завоёванных планет(-1 так как нельзя забрать последнюю планету)
        int planets = minConquestPlanets + UnityEngine.Random.Range(0, randomConquestPlanets + 1);

        if (planets <= 0) return; // Нет завоеваний 

        if (planets >= civilizationTarget.CivData.Planets)
            planets = civilizationTarget.CivData.Planets - 1; // Нет завоеваний

        civilizationTarget.CivData.Planets -= planets;
        // Визуализация завоеваний
        for (int i = 0; i < planets; i++)
        {
            var planet = _planetsFactory.GetNewUnit(TypePlanetEnum.Ideal);
            planet.ConquestPlanets(civilizationTarget.PositionCiv, ThisCivilization.PositionCiv, () => { ThisCivilization.CivData.Planets++; planet.Destroy(); });
        }
    }
}
