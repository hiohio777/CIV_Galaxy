using UnityEngine;
/// <summary>
/// Рост доминирования от индустрии
/// </summary>
public class GrowthDominanceIndustryBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int growthDominanceIndustryBonus;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.GDIndustry += growthDominanceIndustryBonus;
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("gd_industry_bonus")}: +{growthDominanceIndustryBonus}%\r\n";
        return info;
    }
}
