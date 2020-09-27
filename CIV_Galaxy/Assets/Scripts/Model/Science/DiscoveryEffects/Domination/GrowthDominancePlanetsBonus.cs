using UnityEngine;
/// <summary>
/// Рост доминирования от планет(от размера цивилизации - чистый рост)
/// </summary>
public class GrowthDominancePlanetsBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(-60, 60)] private float growthDominancePlanetsBonus;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.GrowthDominancePlanetsBonus += growthDominancePlanetsBonus;
    }
}
