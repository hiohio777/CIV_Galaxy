using UnityEngine;
/// <summary>
/// Ускорение восстановления индустрии
/// </summary>
public class IndustryAcceleration : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(-5, 5)] private float accelerationIndustry;

    public void ExecuteStudy(ICivilizationBase civilization, string nameDiscovery)
    {
        civilization.IndustryCiv.AccelerationBonus += accelerationIndustry;
    }
}
