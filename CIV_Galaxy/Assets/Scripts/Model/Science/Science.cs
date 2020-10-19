using System;

public class Science : CivilizationStructureBase
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private float _progressInterval = 33; // Интервал
    private float _progress = 0; // Прогресс получения нового поинта SciencePoints
    private float _repairTime = 0; // Время на которое приостановлена работа

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
        _acceleration = _scienceData.Acceleration / 100f;

        ProgressEvent?.Invoke(ProgressProc);
        _civilization.ExicuteSciencePoints(Points);

        // Установка сложности
        if (civilization is ICivilizationAl)
            _progressInterval = _progressInterval / 100f * _difficultSettings.ScienceProc;
    }

    public bool IsActive { get; set; } = true; // Активен ли
    public int Points { get; set; }
    private float _acceleration;

    public void Damage(float damage) => _progress -= damage;
    //Бонусы
    public int AccelerationBonus { get; set; } = 0;
    public int CountDiscoveries { get; set; } = 0;

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

        // Подсчёт для статистики
        CountDiscoveries++;
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

            CountDiscoveries++;
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

        _progress += deltaTime * ((_acceleration + _civilization.IndustryCiv.Points / 2f) * (AccelerationBonus / 100f + 1));
        ProgressEvent?.Invoke(ProgressProc);

        if (_progress > _progressInterval)
        {
            _progress -= _progressInterval;

            Points++;
            _civilization.ExicuteSciencePoints(Points);
        }
    }

    public string GetInfo(bool isPlayer = true)
    {
        string info = string.Empty;

        if (isPlayer)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("acceleration")}: <color=lime>{(int)((_acceleration + _civilization.IndustryCiv.Points / 2f) * (AccelerationBonus / 100f + 1f) * 100)}%</color>\r\n";
            info += $"  <color=#add8e6ff>{LocalisationGame.Instance.GetLocalisationString("base")}:</color> <color=orange>{(int)Math.Round(_acceleration * 100, 0) + AccelerationBonus}%</color>\r\n";
            info += $"  <color=#add8e6ff>{LocalisationGame.Instance.GetLocalisationString("industry")}:</color> <color=orange>{(int)(_civilization.IndustryCiv.Points / 2f * 100)}%</color>\r\n";
        }

        return info;
    }
}
