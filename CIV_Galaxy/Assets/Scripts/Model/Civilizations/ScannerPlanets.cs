using System;
using UnityEngine;

public class ScannerPlanets
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private float _progress = 0; // Прогресс сканирования

    private GalaxyData _galaxyData;
    private PlanetsFactory _planetsFactory;
    private ICivilizationBase _civilization;

    public ScannerPlanets(GalaxyData galaxyData, PlanetsFactory planetsFactory)
    {
        (this._galaxyData, this._planetsFactory) = (galaxyData, planetsFactory);
    }

    public void Initialize(ICivilizationBase civilization)
    {
        this._civilization = civilization;
        _civilization.ExecuteOnTimeEvent += _civilization_ExecuteOnTimeEvent;
    }

    public bool IsActive { get; set; } = true; // Активен ли сканер

    //Бонусы
    public float ScannerAccelerationBonus { get; set; } = 0; // Бонус скорости работы сканера
    public int MinimumDiscoveredPlanetsBonus { get; set; } = 0; // Минимальное количество открываемых планет
    public int RandomDiscoveredPlanetsBonus { get; set; } = 0; // Рандомное количество открываемых планет

    private float GetTime => _civilization.CivDataBase.ScannerAcceleration + ScannerAccelerationBonus; // Получить Интервал между сканированиями галактики в поиске планет
    private float ProgressProc => _progress / (GetTime / 100); // Прогресс сканирования в процентах
    
    // Сканирование
    private void _civilization_ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        _progress += deltaTime;
        ProgressEvent?.Invoke(ProgressProc);

        if (_progress > GetTime)
        {
            _progress = 0;
            DiscoverPlanet();

            _civilization.ExicuteScanning();
        }
    }

    private void DiscoverPlanet()
    {
        // Открыть планеты
        int countNewPlanet = _civilization.CivDataBase.MinimumDiscoveredPlanets 
        + MinimumDiscoveredPlanetsBonus 
        + UnityEngine.Random.Range(0, (_civilization.CivDataBase.RandomDiscoveredPlanets + RandomDiscoveredPlanetsBonus));

        if (countNewPlanet < 0)
            return;

        for (int i = 0; i < countNewPlanet; i++)
        {
            if (_galaxyData.CountPlanet <= 0)
            {
                _civilization.ExecuteOnTimeEvent -= _civilization_ExecuteOnTimeEvent;
                return;
            }

            var planet = _planetsFactory.GetNewPlanet(_galaxyData.GetTypePlanet());
            planet.OpenPlanet(_civilization.PositionCiv, !_civilization.IsOpen, () => _civilization.CivData.AddPlanet(planet));
        }
    }
}
