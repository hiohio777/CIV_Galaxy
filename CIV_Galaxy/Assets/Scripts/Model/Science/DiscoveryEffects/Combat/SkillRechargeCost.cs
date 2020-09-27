using UnityEngine;
/// <summary>
/// Стоимость перезарядки скилов
/// </summary>
public class SkillRechargeCost : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 5)] private float skillRechargeCost;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.AbilityCiv.AccelerationBonus += skillRechargeCost;
    }
}
