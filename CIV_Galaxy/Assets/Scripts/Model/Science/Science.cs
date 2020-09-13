using System;
using UnityEngine;

public class Science
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private float _timeBonus = 0; // Бонус уменьшающий или увеличивающий время получения нового поинта SciencePoints
    private float _progress = 0; // Прогресс получения нового поинта SciencePoints

    private ICivilizationBase _civilization;
    private int _sciencePoints; // Очки науки
    private DiscoveryCell _currentDiscovery;

    public Science() { }

    public void Initialize(ICivilizationBase civilization)
    {
        this._civilization = civilization;
        _civilization.ExecuteOnTimeEvent += _civilization_ExecuteOnTimeEvent;

        TreeOfScienceCiv = UnityEngine.Object.Instantiate(_civilization.CivDataBase.TreeOfSciencePrefab);
        TreeOfScienceCiv.Name = $"Science_{civilization.CivDataBase.Name}";

        _sciencePoints = _civilization.CivDataBase.SciencePoints;
    }

    public bool IsActive { get; set; } = true; // Активен ли сканер
    public int SciencePoints => _sciencePoints;
    public ITreeOfScience TreeOfScienceCiv { get; private set; }
    private float ProgressProc => _progress / (GetTime / 100); // Прогресс в процентах
    private float GetTime => _civilization.CivDataBase.ScienceRateResearch + _timeBonus; // Получить Интервал между сканированиями галактики в поиске планет

    public void ExicuteSciencePointsPlayer(DiscoveryCell discoveryCell)
    {
        _sciencePoints -= discoveryCell.ResearchCost;
        _civilization.ExicuteSciencePoints(_sciencePoints);
    }

    public bool ExicuteSciencePointsAl()
    {
        // Назначить желанную науку
        if (_currentDiscovery == null)
        {
            // Назначить науку, если есть неизученные
            TreeOfScienceCiv.CreatAvailableDiscoveriesList();
            var count = TreeOfScienceCiv.AvailableDiscoveries.Count;
            if (count <= 0)
            {
                Debug.Log("Нет доступных наук!");
                return false;
            }

            var index = UnityEngine.Random.Range(0, count);
            _currentDiscovery = TreeOfScienceCiv.AvailableDiscoveries[index];
        }

        // Изучение науки для Al
        if (_currentDiscovery.ResearchCost <= _sciencePoints)
        {
            _currentDiscovery.Study(_civilization);
            TreeOfScienceCiv.AvailableDiscoveries.Remove(_currentDiscovery);
            _sciencePoints = 0;
            _currentDiscovery = null;
            ExicuteSciencePointsAl(); // Рекруссивное назначение нового желанного открытия для Al
            return true;
        }

        return false;
    }

    private void _civilization_ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        _progress += deltaTime;
        ProgressEvent?.Invoke(ProgressProc);

        if (_progress > GetTime)
        {
            _progress = 0;

            _sciencePoints++;
            _civilization.ExicuteSciencePoints(_sciencePoints);
        }
    }
}