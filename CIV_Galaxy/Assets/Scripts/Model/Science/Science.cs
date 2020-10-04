using System;
using UnityEngine;

public class Science : CivilizationStructureBase
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private const float _progressInterval = 30; // Интервал
    private float _progress = 0; // Прогресс получения нового поинта SciencePoints

    private ICivilization _civilization;
    private DiscoveryCell _currentDiscovery;
    private ScienceData _scienceData;

    public Science() { }

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        _scienceData = this._civilization.DataBase.Science;

        TreeOfScienceCiv = UnityEngine.Object.Instantiate(_scienceData.TreeOfSciencePrefab);
        TreeOfScienceCiv.Name = $"Science_{civilization.DataBase.Name}";

        Points = _scienceData.SciencePoints;
        _acceleration = _scienceData.Acceleration / 100;

        ProgressEvent?.Invoke(ProgressProc);
        _civilization.ExicuteSciencePoints(Points);
    }

    public bool IsActive { get; set; } = true; // Активен ли
    public int Points { get; set; }
    private float _acceleration;

    //Бонусы
    private float _accelerationBonus = 1;
    public int Acceleration { get => (int)(_accelerationBonus * 100); set => _accelerationBonus = value / 100f; } // ссумируется с уровнем индстрии

    public ITreeOfScience TreeOfScienceCiv { get; private set; }
    private float ProgressProc => _progress / (_progressInterval / 100); // Прогресс в процентах

    public void AddProgress(float count)
    {
        _progress += _progressInterval / 100 * count;
        ProgressEvent?.Invoke(ProgressProc);
    }

    public void ExicuteSciencePointsPlayer(int researchCost)
    {
        Points -= researchCost;
        _civilization.ExicuteSciencePoints(Points);
    }


    // Изучение науки для Al
    public bool ExicuteSciencePointsAl()
    {
        // Назначить желанную науку
        if (_currentDiscovery == null)
        {
            // Назначить науку, если есть неизученные
            TreeOfScienceCiv.CreatAvailableDiscoveriesList();
            var count = TreeOfScienceCiv.AvailableDiscoveries.Count;
            if (count <= 0) return false;

            var index = UnityEngine.Random.Range(0, count);
            _currentDiscovery = TreeOfScienceCiv.AvailableDiscoveries[index];
        }

        // Изучение науки
        if (_currentDiscovery.ResearchCost <= Points)
        {
            _currentDiscovery.Study(_civilization);
            TreeOfScienceCiv.AvailableDiscoveries.Remove(_currentDiscovery);
            Points = 0;
            _currentDiscovery = null;
            ExicuteSciencePointsAl(); // Рекруссивное назначение нового желанного открытия для Al
            return true;
        }

        return false;
    }

    public bool IsAvailableForStudy()
    {
        TreeOfScienceCiv.CreatAvailableDiscoveriesList();

        foreach (var item in TreeOfScienceCiv.AvailableDiscoveries)
            if (item.ResearchCost <= Points) return true;

        return false;
    }

    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        _progress += deltaTime * (_acceleration + (_civilization.IndustryCiv.Points / 2f * _accelerationBonus));
        ProgressEvent?.Invoke(ProgressProc);

        if (_progress > _progressInterval)
        {
            _progress -= _progressInterval;

            Points++;
            _civilization.ExicuteSciencePoints(Points);
        }
    }
}