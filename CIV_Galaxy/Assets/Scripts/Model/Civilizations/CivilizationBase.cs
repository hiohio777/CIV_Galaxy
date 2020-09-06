using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class CivilizationBase : MonoBehaviour
{
    [SerializeField] protected CivilizationUI civilizationUI;
    protected bool isAssign = false;

    // Данные цивилизации
    protected ScanPlanets scanPlanets;
    protected CivilizationData civilizationData;

    // Другие цивилизации
    protected List<ICivilization> anotherCivilization;

    [Inject]
    public void Inject(List<ICivilization> anotherCivilization)
    {
        Debug.Log(anotherCivilization.Count);
        this.anotherCivilization = anotherCivilization.Where(x => x != this as ICivilization).ToList();
    }

    public CivilizationScriptable CivData { get; set; }

    public virtual void Assign(CivilizationScriptable civData)
    {
        this.CivData = civData;
        scanPlanets = new ScanPlanets(civData.TimeScan, ExicuteScanning);
        civilizationData = new CivilizationData(civData, civilizationUI);

        isAssign = true;
    }

    public void ExecuteOnTime(float deltaTime)
    {
        if (isAssign == false) return;

        scanPlanets.Scan(deltaTime);
        ExecuteOnTimeProcess();
    }

    protected abstract void ExecuteOnTimeProcess();
    protected abstract void ExicuteScanning();
}


public class ScanPlanets
{
    private Action exicuteScanning;
    private float timeScan; // Интервал между сканированиями галактики в поиске планет
    private float scanProgress = 0; // Прогресс сканирования

    public float ScanProgressProc => scanProgress / (timeScan / 100); // Прогресс сканирования в процентах

    public ScanPlanets(float timeScan, Action exicuteScanning)
    {
        (this.timeScan, this.exicuteScanning) = (timeScan, exicuteScanning);
    }

    public void Scan(float deltaTime)
    {
        scanProgress += deltaTime;
        if (scanProgress > timeScan)
        {
            scanProgress = 0;
            exicuteScanning.Invoke();
        }
    }
}

public class CivilizationData
{
    private int planets;
    private ICivilizationDataUI dataUI;

    public CivilizationData(CivilizationScriptable civData, ICivilizationDataUI dataUI)
    {
        planets = civData.Planets;

        this.dataUI = dataUI;
    }

    public int Planets { get => planets; set { planets = value; dataUI.SetCountPlanet(planets); } }
}