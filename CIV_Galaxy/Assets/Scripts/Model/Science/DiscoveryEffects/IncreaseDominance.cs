using UnityEngine;
/// <summary>
/// Одномоментное увеличение доминирования
/// </summary>
public class IncreaseDominance : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField] private int increaseDominance;

    public void ExecuteStudy(ICivilizationBase civilization, string nameDiscovery)
    {
        civilization.CivData.DominationPoints += increaseDominance;
    }
}
