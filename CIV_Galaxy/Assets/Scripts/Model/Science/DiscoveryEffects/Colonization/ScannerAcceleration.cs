﻿using UnityEngine;

/// <summary>
/// Ускорение работы сканера
/// </summary>
public class ScannerAcceleration : MonoBehaviour, IDiscoveryEffects
{
    [SerializeField, Range(0, 100)] private int scannerAcceleration;

    public void ExecuteStudy(ICivilization civilization, string nameDiscovery)
    {
        civilization.ScanerPlanets.Acceleration += scannerAcceleration;
    }
}