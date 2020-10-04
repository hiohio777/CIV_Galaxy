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
}
