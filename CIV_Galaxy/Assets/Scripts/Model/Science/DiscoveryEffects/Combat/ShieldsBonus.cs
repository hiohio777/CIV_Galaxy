using UnityEngine;
/// <summary>
/// Щиты(защищают индустрию)
/// </summary>
public class ShieldsBonus : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int shieldsBonus;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.Shields += shieldsBonus;
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("shields_bonus")}: +{shieldsBonus}%\r\n";
        return info;
    }
}