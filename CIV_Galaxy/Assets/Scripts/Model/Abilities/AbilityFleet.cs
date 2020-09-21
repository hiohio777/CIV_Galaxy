using System;
using UnityEngine;

public enum TypeCivEnum
{
    Player,
    Al,
}

public class AbilityFleet : AbilityBase
{
    [SerializeField] private float minAttackIndustry, randomAttackIndustry; // Атака по индустрии
    [SerializeField] private int minConquestPlanets, randomConquestPlanets; // Завоевание планет
    private IUnitAbility unit;
    private PlanetsFactory _planetsFactory;
    protected UnitAbilityFactory _unitFactory;

    public void Initialize(PlanetsFactory planetsFactory, UnitAbilityFactory unitFactory)
    {
        (this._planetsFactory, this._unitFactory) = (planetsFactory, unitFactory);
    }

    public override void ApplyAl(Diplomacy diplomacyCiv)
    {
        Debug.Log($"{ThisCivilization.DataBase.Name} --> AbilityFleet");

        var enemy = diplomacyCiv.FindEnemy();
        if (enemy != null)
            StartAttack(enemy); // Враг найден
        else
        {
            // Враг не найден(отсрочка выполнения(чуть сбрасывается готовность скилла))
            DelayExecutionAl();
        }
    }

    public override void Apply(ICivilization civilizationTarget)
    {
        base.Apply(civilizationTarget);
        StartAttack(civilizationTarget);
    }

    // Отправить флот в атаку
    private void StartAttack(ICivilization civilizationTarget)
    {
        IsReady = false;
        unit = _unitFactory.GetNewUnit(this, ThisCivilization.PositionCiv, ThisCivilization.TypeCiv, civilizationTarget.TypeCiv);

        var centrGlaxy = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), 0);

        Action battelAct = () => unit.SetScale(0f, 0.2f).Run(() => EndAttack(civilizationTarget));
        Action toMoveAttackAct = () => unit.SetScale(1.3f, 4.5f).SetPosition(civilizationTarget.PositionCiv, 5f).Run(battelAct);
        Action toMoveGalaxyAct = () => unit.SetScale(1f, 4.5f).SetPosition(centrGlaxy, 5f).Run(toMoveAttackAct);
        Action startAct = () => unit.SetScale(1.3f, 0.2f).Run(toMoveGalaxyAct);
        unit.SetScale(1.5f, 0.3f).Run(startAct);
    }

    private void EndAttack(ICivilization civilizationTarget)
    {
        AttackIndustry(civilizationTarget);
        ConquestPlanets(civilizationTarget);

        unit.Destroy();
        unit = null;
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
