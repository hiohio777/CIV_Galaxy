using UnityEngine;
/// <summary>
/// Бонус к эффективности событий
/// </summary>
public class EfficiencyEventBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int efficiencyEventBonus;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.EventGenerator.AddBonusEfficiency(efficiencyEventBonus);
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("efficiency_event_bonus")}: +{efficiencyEventBonus}%\r\n";
        return info;
    }
}