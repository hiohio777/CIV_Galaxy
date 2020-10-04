using UnityEngine;
/// <summary>
/// Увеличение минимума открываемых планет(при срабатывании галактического сканера)
/// </summary>
public class MinimumDiscoveredPlanets : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int discoveredPlanets;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.ScanerPlanets.MinimumDiscoveredPlanetsBonus += discoveredPlanets;
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("minimum_discovered_planets")}: +{discoveredPlanets}\r\n";
        return info;
    }
}
