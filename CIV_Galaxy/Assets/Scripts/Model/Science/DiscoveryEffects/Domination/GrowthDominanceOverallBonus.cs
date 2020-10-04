using UnityEngine;
/// <summary>
/// Бонус к годовому росту доминирования(общему)
/// </summary>
public class GrowthDominanceOverallBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int growthDominanceOverallBonus;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.GDOverall += growthDominanceOverallBonus;
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("gd_overall_bonus")}: +{growthDominanceOverallBonus}%\r\n";
        return info;
    }
}
