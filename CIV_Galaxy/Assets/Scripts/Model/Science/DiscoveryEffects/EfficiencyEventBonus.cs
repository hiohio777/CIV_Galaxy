using UnityEngine;
/// <summary>
/// Бонус к эффективности событий
/// </summary>
public class EfficiencyEventBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int growthDominanceIndustryBonus;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.EventGenerator.AddBonusEfficiency(growthDominanceIndustryBonus);
    }
}