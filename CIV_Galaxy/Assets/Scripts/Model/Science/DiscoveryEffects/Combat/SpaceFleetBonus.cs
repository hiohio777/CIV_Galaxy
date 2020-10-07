using System.Linq;
using UnityEngine;
/// <summary>
/// Бонусы военного флота
/// </summary>
public class SpaceFleetBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 5)] private int countUnits;
    [SerializeField, Range(0, 500)] private int minConquestPlanets, randomConquestPlanets;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        var abilities = civilization.AbilityCiv.Abilities.Where(x => x is SpaceFleet).ToList();

        foreach (var item in abilities)
        {
            var abil = item as SpaceFleet;
            // Бонусы
            abil.AddBonus_minConquestPlanets(minConquestPlanets);
            abil.AddBonus_randomConquestPlanets(randomConquestPlanets);
            abil.CountUnits += countUnits;
        }
    }

    public string GetInfo()
    {
        string info = string.Empty;

        if (minConquestPlanets > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("fleet_bonus_min_conquest_planets")}: +{minConquestPlanets}\r\n";
        }
        if (randomConquestPlanets > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("fleet_bonus_random_conquest_planets")}: +{randomConquestPlanets}\r\n";
        }
        if (countUnits > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("fleet_bonus_countUnits")}: +{countUnits}\r\n";
        }

        return info;
    }
}
