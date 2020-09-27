using UnityEngine;
/// <summary>
/// Рандомное число планет открываемых помимо фиксированного(при срабатывании галактического сканера)
/// </summary>
public class RandomDiscoveredPlanets : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(-100, 100)] private int randomDiscoveredPlanets;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.ScanerPlanets.RandomDiscoveredPlanetsBonus += randomDiscoveredPlanets;
    }
}
