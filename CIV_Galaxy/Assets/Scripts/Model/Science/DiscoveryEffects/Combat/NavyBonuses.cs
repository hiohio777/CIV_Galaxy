using System.Linq;
using UnityEngine;
/// <summary>
/// Стоимость перезарядки скилов
/// </summary>
public class NavyBonuses : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 1)] private float minAttackIndustry, randomAttackIndustry; // Атака по индустрии
    [SerializeField, Range(0, 500)] private int minConquestPlanets, randomConquestPlanets; // Завоевание планет

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        var abilities = civilization.AbilityCiv.Abilities.Where(x => x is SpaceFleet).ToList();

        foreach (var item in abilities)
        {
            var abil = item as SpaceFleet;
            // Бонусы
            abil.AddBonus_minAttackIndustry(minAttackIndustry);
            abil.AddBonus_randomAttackIndustry(randomAttackIndustry);
            abil.AddBonus_minConquestPlanets(minConquestPlanets);
            abil.AddBonus_randomConquestPlanets(randomConquestPlanets);
        }
    }
}