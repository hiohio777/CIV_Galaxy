using System;
using UnityEngine;

public class Science
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private float _progress = 0; // Прогресс получения нового поинта SciencePoints

    private ICivilizationBase _civilization;
    private DiscoveryCell _currentDiscovery;
    private ScienceData _scienceData;

    public Science() { }

    public void Initialize(ICivilizationBase civilization)
    {
        this._civilization = civilization;
        _scienceData = this._civilization.DataBase.Science;
        _civilization.ExecuteOnTimeEvent += Сivilization_ExecuteOnTimeEvent;

        TreeOfScienceCiv = UnityEngine.Object.Instantiate(_scienceData.TreeOfSciencePrefab);
        TreeOfScienceCiv.Name = $"Science_{civilization.DataBase.Name}";

        Points = _scienceData.SciencePoints;
        civilization.CivData.GetSciencePoints += () => Points;
    }

    public bool IsActive { get; set; } = true; // Активен ли
    public int Points { get; set; }

    //Бонусы
    public float AccelerationBonus { get; set; } = 0; // Бонус скорости работы

    public ITreeOfScience TreeOfScienceCiv { get; private set; }
    private float ProgressProc => _progress / (GetTime / 100); // Прогресс в процентах
    private float GetTime => _scienceData.Acceleration + AccelerationBonus; // Получить Интервал

    public void ExicuteSciencePointsPlayer(DiscoveryCell discoveryCell)
    {
        Points -= discoveryCell.ResearchCost;
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

    private void Сivilization_ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        _progress += deltaTime;
        ProgressEvent?.Invoke(ProgressProc);

        if (_progress > GetTime)
        {
            _progress = 0;

            Points++;
            _civilization.ExicuteSciencePoints(Points);
        }
    }
}