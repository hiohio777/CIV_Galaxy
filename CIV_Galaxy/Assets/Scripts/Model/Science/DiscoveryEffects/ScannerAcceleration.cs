using UnityEngine;

/// <summary>
/// Ускорение работы сканера
/// </summary>
public class ScannerAcceleration : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(-5, 5)] private float scannerAcceleration;

    public void ExecuteStudy(ICivilizationBase civilization, string nameDiscovery)
    {
        civilization.ScanerPlanets.AccelerationBonus += scannerAcceleration;
    }
}