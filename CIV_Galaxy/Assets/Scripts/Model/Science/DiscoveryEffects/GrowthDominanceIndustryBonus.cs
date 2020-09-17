using UnityEngine;
/// <summary>
/// Рост доминирования от индустрии
/// </summary>
public class GrowthDominanceIndustryBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(-60, 60)] private float growthDominanceIndustryBonus;

    public void ExecuteStudy(ICivilizationBase civilization, string nameDiscovery)
    {
        civilization.CivData.GrowthDominanceIndustryBonus += growthDominanceIndustryBonus;
    }
}
