using System;

public class Industry
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private float _progress = 0; // Прогресс

    private ICivilizationBase _civilization;
    private IndustryData _industryData;

    public Industry() { }

    public void Initialize(ICivilizationBase civilization)
    {
        this._civilization = civilization;
        _industryData = this._civilization.DataBase.Industry;

        _civilization.ExecuteOnTimeEvent += Civilization_ExecuteOnTimeEvent;

        Points = _industryData.Points;
        civilization.CivData.GetIndustryPoints += () => Points;
    }

    public void ExicuteIndustryAl()
    {
        // Действия Al
    }

    public bool IsActive { get; set; } = true; // Активен ли

    public int Points { get; set; }

    //Бонусы
    public float MaxPointBonus { get; set; } = 0; // Бонус максимального количества очков индустрии, которые можно накопить
    public float AccelerationBonus { get; set; } = 0; // Бонус к скорости роста индустрии(уменьшает интервал между добавлением очков индустрии)

    private float GetTime => _industryData.Acceleration + AccelerationBonus; // Получить Интервал
    private float PointsProc => Points / ((_industryData.MaxPoints + MaxPointBonus) / 100); // Размер индустрии в процентах от максимального количества поинтов
    private bool GetMaxIndustry => Points >= _industryData.MaxPoints + MaxPointBonus;

    // Рост индустрии
    private void Civilization_ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false || GetMaxIndustry) return;

        _progress += deltaTime;

        if (_progress > GetTime)
        {
            _progress = 0;

            Points++;
            _civilization.ExicuteIndustryPoints(Points, PointsProc);
        }
    }
}
