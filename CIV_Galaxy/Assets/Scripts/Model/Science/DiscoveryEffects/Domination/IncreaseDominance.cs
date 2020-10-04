using UnityEngine;
/// <summary>
/// Одномоментное увеличение доминирования
/// </summary>
public class IncreaseDominance : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField] private int increaseDominance;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.DominationPoints += increaseDominance;
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("increase_dominance")}: +{increaseDominance}\r\n";
        return info;
    }
}
