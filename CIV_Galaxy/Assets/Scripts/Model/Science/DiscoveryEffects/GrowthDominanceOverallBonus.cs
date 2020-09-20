using UnityEngine;
/// <summary>
/// Бонус к годовому росту доминирования(общему)
/// </summary>
public class GrowthDominanceOverallBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(-10, 10)] private float growthDominanceOverallBonus;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.GrowthDominanceOverallBonus += growthDominanceOverallBonus;
    }
}
