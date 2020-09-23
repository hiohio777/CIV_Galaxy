using System;

public class Industry : CivilizationStructureBase
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private const float maxPoint = 1; // Максимальное количество поинтов индустрии
    private const float _progressInterval = 5; // Интервал
    private float _progress = 0; // Прогресс
    private float _point = 0;

    private ICivilization _civilization;
    private IndustryData _industryData;

    public Industry() { }

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        _industryData = this._civilization.DataBase.Industry;

        Points = _industryData.Points;
        civilization.CivData.GetIndustryPoints += () => Points;

        _civilization.ExicuteIndustryPoints(Points);
    }

    public void ExicuteIndustryAl()
    {
        // Действия Al
    }

    public bool IsActive { get; set; } = true; // Активен ли
    public float Points {
        get => _point;
        set {
            _point = value; if (_point > 1) _point = 1;
            else if (_point < 0) _point = 0;

            _civilization.ExicuteIndustryPoints(_point);
        }
    }

    //Бонусы
    public float AccelerationBonus { get; set; } = 0; // Бонус к скорости роста индустрии(уменьшает интервал между добавлением очков индустрии)

    // Рост индустрии
    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false || Points >= maxPoint) return;

        _progress += deltaTime * (_industryData.Acceleration + AccelerationBonus);

        if (_progress > _progressInterval)
        {
            _progress = 0;

            Points += 0.01f;
            _civilization.ExicuteIndustryPoints(Points);
        }
    }
}
