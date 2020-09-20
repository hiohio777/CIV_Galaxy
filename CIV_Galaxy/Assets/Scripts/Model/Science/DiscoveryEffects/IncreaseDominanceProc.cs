using UnityEngine;
/// <summary>
/// Одномоментное увеличение доминирования(в процентах от уже существующего)
/// </summary>
public class IncreaseDominanceProc : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField] private float increaseDominanceProc;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.DominationPoints += civilization.CivData.DominationPoints / 100 * increaseDominanceProc;
    }
}