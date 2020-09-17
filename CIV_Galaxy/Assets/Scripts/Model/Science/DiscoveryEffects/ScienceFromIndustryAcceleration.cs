using UnityEngine;
/// <summary>
/// Ускорение развития наук от индустрии
/// </summary>
public class ScienceFromIndustryAcceleration : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(-5, 5)] private float accelerationBonus;

    public void ExecuteStudy(ICivilizationBase civilization, string nameDiscovery)
    {
        civilization.ScienceCiv.AccelerationBonus += accelerationBonus;
    }
}