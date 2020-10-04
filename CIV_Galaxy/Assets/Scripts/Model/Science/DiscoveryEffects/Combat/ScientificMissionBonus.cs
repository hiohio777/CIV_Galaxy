using System.Linq;
using UnityEngine;
/// <summary>
/// Бонусы научным экспедициям
/// </summary>
public class ScientificMissionBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 500)] private int scientificMissionBonus, countUnits;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        var abilities = civilization.AbilityCiv.Abilities.Where(x => x is ScientificMission).ToList();

        foreach (var item in abilities)
        {
            var abil = item as ScientificMission;
            // Бонусы
            abil.AddBonus_ProgressScienty(scientificMissionBonus);
            abil.CountUnits += countUnits;
        }
    }

    public string GetInfo()
    {
        string info = string.Empty;

        if (scientificMissionBonus > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("scientific_mission_bonus")}: +{scientificMissionBonus}%\r\n";
        }
        if (scientificMissionBonus > 0)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("scientific_mission_countUnits")}: +{countUnits}\r\n";
        }

        return info;
    }
}
