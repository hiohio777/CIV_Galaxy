using UnityEngine;
/// <summary>
/// Ускорение восстановления индустрии
/// </summary>
public class IndustryAcceleration : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int accelerationIndustry;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.IndustryCiv.Acceleration += accelerationIndustry;
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("industry_acceleration")}: +{accelerationIndustry}%\r\n";
        return info;
    }
}
