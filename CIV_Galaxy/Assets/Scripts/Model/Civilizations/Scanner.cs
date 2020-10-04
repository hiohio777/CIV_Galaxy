using System;
using UnityEngine;
using Zenject;

public class Scanner : CivilizationStructureBase
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private const float _progressInterval = 15; // Интервал
    private float _progress = 0;

    private GalaxyData _galaxyData;
    private PlanetsFactory _planetsFactory;
    private ICivilization _civilization;
    private ScanerData _scanerData;


    public Scanner(GalaxyData galaxyData, PlanetsFactory planetsFactory)
    {
        (this._galaxyData, this._planetsFactory) = (galaxyData, planetsFactory);
    }

    public void AddProgress(float count)
    {
        _progress += _progressInterval / 100 * count; ;
        ProgressEvent?.Invoke(ProgressProc);
    }

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        _scanerData = this._civilization.DataBase.Scaner;

        Acceleration = _scanerData.Acceleration;
        MinimumDiscoveredPlanetsBonus = _scanerData.MinimumDiscoveredPlanets;
        RandomDiscoveredPlanetsBonus = _scanerData.RandomDiscoveredPlanets;

        ProgressEvent?.Invoke(ProgressProc);
    }

    public bool IsActive { get; set; } = true; // Активен ли

    //Бонусы
    private float _acceleration;
    public int Acceleration { get => (int)(_acceleration * 100); set => _acceleration = value / 100f; }
    public int MinimumDiscoveredPlanetsBonus { get; set; } = 0; // Минимальное количество открываемых планет
    public int RandomDiscoveredPlanetsBonus { get; set; } = 0; // Рандомное количество открываемых планет

    private float ProgressProc => _progress / (_progressInterval / 100); // Прогресс сканирования в процентах

    // Сканирование
    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        _progress += deltaTime * _acceleration;

        if (_progress > _progressInterval)
        {
            _progress -= _progressInterval;
            DiscoverPlanet();

            _civilization.ExicuteScanning();
        }

        ProgressEvent?.Invoke(ProgressProc);
    }

    private void DiscoverPlanet()
    {
        // Открыть планеты
        int countNewPlanet = MinimumDiscoveredPlanetsBonus + UnityEngine.Random.Range(0, RandomDiscoveredPlanetsBonus + 1);

        if (countNewPlanet < 0)
            return;

        for (int i = 0; i < countNewPlanet; i++)
        {
            if (_galaxyData.CountAllPlanet <= 0)
            {
                IsActive = false;
                return;
            }

            var planet = _planetsFactory.GetNewUnit(_galaxyData.GetTypePlanet());
            if (_civilization.IsOpen == false) _civilization.CivData.AddPlanet(planet);
            else planet.OpenPlanet(_civilization.PositionCiv, () => _civilization.CivData.AddPlanet(planet));
        }
    }
}

public abstract class CivilizationStructureBase
{
    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        galaxyUITimer.ExecuteOfTime += ExecuteOnTimeEvent;
    }

    protected abstract void ExecuteOnTimeEvent(float deltaTime);
}