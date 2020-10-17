using UnityEngine;
/// <summary>
/// Одномоментное увеличение доминирования
/// </summary>
public class IncreaseDominance : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField] private int increaseDominance;
    [SerializeField] private Sprite effectSprite;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.CivData.AddDominance(increaseDominance);
        civilization.ExicuteSpecialEffect(effectSprite, EffectEnum.Standart);
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("increase_dominance")}: +{increaseDominance}\r\n";
        return info;
    }
}
