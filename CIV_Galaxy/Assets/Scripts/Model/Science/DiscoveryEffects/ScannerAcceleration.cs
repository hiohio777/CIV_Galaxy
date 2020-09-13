using UnityEngine;

/// <summary>
/// Ускорение работы сканера
/// </summary>
public class ScannerAcceleration : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(-60, 60)] private float scannerAcceleration;

    public void ExecuteStudy(ICivilizationBase civilization, string nameDiscovery)
    {
        Debug.Log($"{civilization.CivDataBase.Name}: Discovery({nameDiscovery}) Effect -> {name}");

        civilization.ScanerPlanets.ScannerAccelerationBonus += scannerAcceleration;
    }
}
