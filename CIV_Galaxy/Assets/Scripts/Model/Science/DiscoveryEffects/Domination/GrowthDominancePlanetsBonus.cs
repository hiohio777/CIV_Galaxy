using UnityEngine;
/// <summary>
/// Рост доминирования от планет(от размера цивилизации - чистый рост)
/// </summary>
public class GrowthDominancePlanetsBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int growthDominancePlanetsBonus;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.GDPlanets += growthDominancePlanetsBonus;
    }
}
