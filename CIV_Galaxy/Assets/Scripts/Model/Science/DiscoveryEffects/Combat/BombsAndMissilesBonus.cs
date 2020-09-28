using System.Linq;
using UnityEngine;
/// <summary>
/// Бонусы Ракет и бомб
/// </summary>
public class BombsAndMissilesBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Header("Урон инустрии(%)"), Range(0, 500)] private int minAttackIndustry, randomAttackIndustry;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        var abilities = civilization.AbilityCiv.Abilities.Where(x => x is BombsAndMissiles).ToList();

        foreach (var item in abilities)
        {
            var abil = item as BombsAndMissiles;
            // Бонусы
            abil.AddBonus_minAttackIndustry(minAttackIndustry);
            abil.AddBonus_randomAttackIndustry(randomAttackIndustry);
        }
    }
}