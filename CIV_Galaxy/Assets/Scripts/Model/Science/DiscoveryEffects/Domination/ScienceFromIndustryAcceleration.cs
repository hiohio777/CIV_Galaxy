using UnityEngine;
/// <summary>
/// Ускорение развития наук от индустрии
/// </summary>
public class ScienceFromIndustryAcceleration : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int accelerationBonus;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.ScienceCiv.Acceleration += accelerationBonus;
    }
}