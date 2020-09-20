using UnityEngine;
/// <summary>
/// Увеличение минимума открываемых планет(при срабатывании галактического сканера)
/// </summary>
public class MinimumDiscoveredPlanets : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(-100, 100)] private int discoveredPlanets;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.ScanerPlanets.MinimumDiscoveredPlanetsBonus += discoveredPlanets;
    }
}
