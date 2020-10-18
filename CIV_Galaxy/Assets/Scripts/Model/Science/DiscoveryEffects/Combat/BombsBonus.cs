using System.Linq;
using UnityEngine;
/// <summary>
/// Бонусы Ракет и бомб
/// </summary>
public class BombsBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 5)] private int countUnits;
    [SerializeField, Header("Урон инустрии(%)"), Range(0, 500)] private int minAttackIndustry, randomAttackIndustry;
    [SerializeField, Range(0, 100)] private float attackScaner, attackScience, attackAbility;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        var abilities = civilization.AbilityCiv.Abilities.Where(x => x is Bombs).ToList();

        foreach (var item in abilities)
        {
            var abil = item as Bombs;
            // Бонусы
            abil.AddBonus_minAttackIndustry(minAttackIndustry);
            abil.AddBonus_randomAttackIndustry(randomAttackIndustry);
            abil.CountUnits += countUnits;

            abil.AddBonus_attackScaner(attackScaner);
            abil.AddBonus_attackScience(attackScience);
            abil.AddBonus_attackAbility(attackAbility);
        }
    }

    public string GetInfo()
    {
        string info = string.Empty;

        if (minAttackIndustry > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("bombs_bonus_minAttack_industry")}: +{minAttackIndustry}%\r\n";
        }
        if (randomAttackIndustry > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("bombs_bonus_random_attack_industry")}: +{randomAttackIndustry}%\r\n";
        }
        if (countUnits > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("bombs_bonus_countUnits")}: +{countUnits}\r\n";
        }
        if (attackScaner > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("bombs_damage_ability")}: +{attackScaner}\r\n";
        }
        if (attackScience > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("bombs_damage_science")}: +{attackScience}\r\n";
        }
        if (attackAbility > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("bombs_damage_ability")}: +{attackAbility}\r\n";
        }

        return info;
    }
}