using System;
using UnityEngine;
using Zenject;

/// <summary>
/// Одномоментное открытие определённого количества планет
/// </summary>
public class OpenPlanets : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 1000)] private int minPlanet, maxPlanet;
    [SerializeField] private Sprite effectSprite;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        // Открыть планеты
        int countNewPlanet = minPlanet + UnityEngine.Random.Range(0, maxPlanet + 1);
        if (civilization.ScanerCiv.DiscoverPlanetEvent(countNewPlanet) > 0)
        {
            // Специфект открытия
            civilization.ExicuteSpecialEffect(effectSprite, EffectEnum.SpecialEffect_0);
        }
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("if_galaxy_has_not_been_fully")}\r\n+{minPlanet}";
        if(maxPlanet > 0) info = $" - {minPlanet + maxPlanet}\r\n";
        return info;
    }
}