using UnityEngine;
/// <summary>
/// Одномоментное увеличение доминирования(в процентах от уже существующего)
/// </summary>
public class IncreaseDominanceProc : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField] private float increaseDominanceProc;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.AddDominance(civilization.CivData.DominationPoints / 100 * increaseDominanceProc);
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("increase_dominance_proc")}: +{increaseDominanceProc}%\r\n";
        return info;
    }
}