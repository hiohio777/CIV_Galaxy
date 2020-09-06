using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class CivilizationBase : MonoBehaviour
{
    [SerializeField] protected CivilizationUI civilizationUI;
    protected bool isAssign = false;
    private bool isOpen = false;
    protected Vector2 _positionCiv;

    protected CivilizationData civilizationData; // Данные цивилизации
    protected ScannerPlanets scanerPlanets; // Сканер планет

    protected List<ICivilization> anotherCiv; // Другие цивилизации

    [Inject]
    public void Inject(List<ICivilization> anotherCivilization, CivilizationData civilizationData,
        ScannerPlanets scanerPlanets)
    {
        this.anotherCiv = anotherCivilization.Where(x => x != this as ICivilization).ToList();
        (this.civilizationData, this.scanerPlanets) = (civilizationData, scanerPlanets);
    }

    public CivilizationScriptable CivData { get; set; }
    public bool IsOpen { get => isOpen; set => isOpen = scanerPlanets.IsOpen = value; }

    public virtual void Assign(CivilizationScriptable civData)
    {
        this.CivData = civData;

        // Инициализация данных
        civilizationData.Initialize(civData, civilizationUI);
        scanerPlanets.Initialize(civData.TimeScan, _positionCiv, ExicuteScanning, civilizationData);

        isAssign = true;
    }

    public void ExecuteOnTime(float deltaTime)
    {
        if (isAssign == false) return;

        scanerPlanets.Scan(deltaTime);
        ExecuteOnTimeProcess();
    }

    protected abstract void ExecuteOnTimeProcess();
    protected abstract void ExicuteScanning();
}


public class ScannerPlanets
{
    private Action _exicuteScanningPlanets;

    private float _timeScan; // Интервал между сканированиями галактики в поиске планет
    private float _scanProgress = 0; // Прогресс сканирования

    private GalaxyData _galaxyData;
    private PlanetsFactory _planetsFactory;
    private CivilizationData _civilizationData;
    private Vector2 _positionCiv; 

    public ScannerPlanets(GalaxyData galaxyData, PlanetsFactory planetsFactory)
    {
        (this._galaxyData, this._planetsFactory) = (galaxyData, planetsFactory);
    }

    public bool IsOpen { get; set; } = false;
    public float ScanProgressProc => _scanProgress / (_timeScan / 100); // Прогресс сканирования в процентах
    public bool IsActive { get; set; } = true; // Активен ли сканер

    public void Initialize(float timeScan, Vector2 positionCiv, Action exicuteScanning, CivilizationData civilizationData)
    {
        (this._timeScan, this._positionCiv, this._exicuteScanningPlanets, this._civilizationData)
        = (timeScan, positionCiv, exicuteScanning, civilizationData);
    }

    public void Scan(float deltaTime)
    {
        if (IsActive == false) return;

        _scanProgress += deltaTime;
        if (_scanProgress > _timeScan)
        {
            _scanProgress = 0;
            DiscoverPlanet();
            _exicuteScanningPlanets.Invoke();
        }
    }

    private void DiscoverPlanet()
    {
        if (_galaxyData.CountPlanet <= 0)
        {
            Debug.Log("Планет не осталось!!!");

            IsActive = false; // Остановить сканер
            return;
        }

        // Открыть планету
        var planet = _planetsFactory.GetNewPlanet(_galaxyData.GetTypePlanet());
        planet.Hide(!IsOpen).SetPosition(_positionCiv, UnityEngine.Random.Range(1f,2f)).Run(()=> _civilizationData.AddPlanet(planet));
    }
}

public class CivilizationData
{
    private int _planets;
    private ICivilizationDataUI _dataUI;

    public int Planets { get => _planets; set { _planets = value; _dataUI.SetCountPlanet(_planets); } }

    public void Initialize(CivilizationScriptable civData, ICivilizationDataUI dataUI)
    {
        this._dataUI = dataUI;
        Planets = civData.Planets;
    }

    public void AddPlanet(IPlanet planet)
    {
        Planets++;
        planet.Destroy();
    }
}
