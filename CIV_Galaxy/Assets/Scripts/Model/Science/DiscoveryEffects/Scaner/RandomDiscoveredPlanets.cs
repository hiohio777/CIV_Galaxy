﻿using UnityEngine;
/// <summary>
/// Рандомное число планет открываемых помимо фиксированного(при срабатывании галактического сканера)
/// </summary>
public class RandomDiscoveredPlanets : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int randomDiscoveredPlanets;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.ScanerCiv.RandomDiscoveredPlanetsBonus += randomDiscoveredPlanets;
    }

    public string GetInfo()
    {
        string info = $"{LocalisationGame.Instance.GetLocalisationString("random_discovered_planets")}: +{randomDiscoveredPlanets}\r\n";
        return info;
    }
}
