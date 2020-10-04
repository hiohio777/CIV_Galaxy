using UnityEngine;
/// <summary>
/// Стоимость перезарядки скилов
/// </summary>
public class SkillRechargeCost : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int skillRechargeCost;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.AbilityCiv.AccelerationBonus += skillRechargeCost;
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("skill_recharge_cost")}: +{skillRechargeCost}%\r\n";
        return info;
    }
}
