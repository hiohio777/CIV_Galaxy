using System;
using UnityEngine;

public class Scanner
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private float _progress = 0;

    private GalaxyData _galaxyData;
    private PlanetsFactory _planetsFactory;
    private ICivilizationBase _civilization;
    private ScanerData _scanerData;

    public Scanner(GalaxyData galaxyData, PlanetsFactory planetsFactory)
    {
        (this._galaxyData, this._planetsFactory) = (galaxyData, planetsFactory);
    }

    public void Initialize(ICivilizationBase civilization)
    {
        this._civilization = civilization;
        _scanerData = this._civilization.DataBase.Scaner;
        _civilization.ExecuteOnTimeEvent += Civilization_ExecuteOnTimeEvent;

        ProgressEvent?.Invoke(ProgressProc);
    }

    public bool IsActive { get; set; } = true; // Активен ли

    //Бонусы
    public float AccelerationBonus { get; set; } = 0; // Бонус скорости работы
    public int MinimumDiscoveredPlanetsBonus { get; set; } = 0; // Минимальное количество открываемых планет
    public int RandomDiscoveredPlanetsBonus { get; set; } = 0; // Рандомное количество открываемых планет

    private float GetTime => _scanerData.Acceleration + AccelerationBonus; // Получить Интервал между сканированиями галактики в поиске планет
    private float ProgressProc => _progress / (GetTime / 100); // Прогресс сканирования в процентах
    
    // Сканирование
    private void Civilization_ExecuteOnTimeEvent(float deltaTime)
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
        int countNewPlanet = _scanerData.MinimumDiscoveredPlanets 
        + MinimumDiscoveredPlanetsBonus 
        + UnityEngine.Random.Range(0, (_scanerData.RandomDiscoveredPlanets + RandomDiscoveredPlanetsBonus));

        if (countNewPlanet < 0)
            return;

        for (int i = 0; i < countNewPlanet; i++)
        {
            if (_galaxyData.CountPlanet <= 0)
            {
                _civilization.ExecuteOnTimeEvent -= Civilization_ExecuteOnTimeEvent;
                return;
            }

            var planet = _planetsFactory.GetNewPlanet(_galaxyData.GetTypePlanet());
            planet.OpenPlanet(_civilization.PositionCiv, !_civilization.IsOpen, () => _civilization.CivData.AddPlanet(planet));
        }
    }
}
