using System;
using UnityEngine;
using Zenject;

public class Scanner : CivilizationStructureBase
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private const float _progressInterval = 10; // Интервал
    private float _progress = 0;

    private GalaxyData _galaxyData;
    private PlanetsFactory _planetsFactory;
    private ICivilization _civilization;
    private ScanerData _scanerData;


    public Scanner(GalaxyData galaxyData, PlanetsFactory planetsFactory)
    {
        (this._galaxyData, this._planetsFactory) = (galaxyData, planetsFactory);
    }

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        _scanerData = this._civilization.DataBase.Scaner;

        ProgressEvent?.Invoke(ProgressProc);
    }

    public bool IsActive { get; set; } = true; // Активен ли

    //Бонусы
    public float AccelerationBonus { get; set; } = 0; // Бонус скорости работы
    public int MinimumDiscoveredPlanetsBonus { get; set; } = 0; // Минимальное количество открываемых планет
    public int RandomDiscoveredPlanetsBonus { get; set; } = 0; // Рандомное количество открываемых планет

    private float ProgressProc => _progress / (_progressInterval / 100); // Прогресс сканирования в процентах

    // Сканирование
    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        _progress += deltaTime * (_scanerData.Acceleration + AccelerationBonus);
        ProgressEvent?.Invoke(ProgressProc);

        if (_progress > _progressInterval)
        {
            _progress = 0;
            DiscoverPlanet();

            _civilization.ExicuteScanning();
        }
    }

    private void DiscoverPlanet()
    {
        // Открыть планеты
        int countNewPlanet = _scanerData.MinimumDiscoveredPlanets
        + MinimumDiscoveredPlanetsBonus
        + UnityEngine.Random.Range(0, (_scanerData.RandomDiscoveredPlanets + RandomDiscoveredPlanetsBonus + 1));

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