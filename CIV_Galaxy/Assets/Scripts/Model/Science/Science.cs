using System;
using UnityEngine;

public class Science
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private const float _progressInterval = 30; // Интервал
    private float _progress = 0; // Прогресс получения нового поинта SciencePoints

    private ICivilization _civilization;
    private DiscoveryCell _currentDiscovery;
    private ScienceData _scienceData;
    Industry _industry;

    public Science() { }

    public void Initialize(ICivilization civilization, Industry industry)
    {
        this._industry = industry;
        this._civilization = civilization;
        _scienceData = this._civilization.DataBase.Science;
        _civilization.ExecuteOnTimeEvent += ExecuteOnTimeEvent;

        TreeOfScienceCiv = UnityEngine.Object.Instantiate(_scienceData.TreeOfSciencePrefab);
        TreeOfScienceCiv.Name = $"Science_{civilization.DataBase.Name}";

        Points = _scienceData.SciencePoints;

        ProgressEvent?.Invoke(ProgressProc);
        _civilization.ExicuteSciencePoints(Points);
    }

    public bool IsActive { get; set; } = true; // Активен ли
    public int Points { get; set; }

    //Бонусы
    public float AccelerationBonus { get; set; } = 0f; // Бонус скорости работы( уменьшает интервал получения нового поинта - зависит от индустрии)

    public ITreeOfScience TreeOfScienceCiv { get; private set; }
    private float ProgressProc => _progress / (_progressInterval / 100); // Прогресс в процентах

    public void AddPoints(int point) 
    {
        Points += point;
        _civilization.ExicuteSciencePoints(Points);
    }

    public void AddProgress(float count)
    {
        _progress += count;
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

    private void ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        _progress += deltaTime * (_scienceData.Acceleration + (_industry.Points / 2 * AccelerationBonus));
        ProgressEvent?.Invoke(ProgressProc);

        if (_progress > _progressInterval)
        {
            _progress = 0;

            Points++;
            _civilization.ExicuteSciencePoints(Points);
        }
    }
}