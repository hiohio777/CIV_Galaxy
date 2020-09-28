using System;
using UnityEngine;

public class SpaceFleet : AttackerAbility
{
    [SerializeField, Header("Минимум"), Range(0, 500)] private int minConquestPlanets;
    [SerializeField, Header("Рандомное количество"), Range(0, 500)] private int randomConquestPlanets;

    private PlanetsFactory _planetsFactory;

    public void Initialize(PlanetsFactory planetsFactory, UnitAbilityFactory unitFactory)
    {
        Initialize(unitFactory);
        this._planetsFactory = planetsFactory;
    }

    public void AddBonus_minConquestPlanets(int bonus)
    {
        minConquestPlanets += bonus;
        if (minConquestPlanets < 0) minConquestPlanets = 0;
    }
    public void AddBonus_randomConquestPlanets(int bonus)
    {
        randomConquestPlanets += bonus;
        if (randomConquestPlanets < 0) randomConquestPlanets = 0;
    }

    protected override void Finall(IUnitAbility unit, ICivilization civilizationTarget)
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
            var planet = _planetsFactory.GetNewUnit(SpriteUnitEnum.Ideal);
            planet.ConquestPlanets(civilizationTarget.PositionCiv, ThisCivilization.PositionCiv, () => { ThisCivilization.CivData.Planets++; planet.Destroy(); });
        }
    }
}