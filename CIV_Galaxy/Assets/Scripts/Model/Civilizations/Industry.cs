using System;

public class Industry : CivilizationStructureBase
{
    public event Action<float> ProgressEvent; // Отображение на экране

    private const float maxPoint = 1; // Максимальное количество поинтов индустрии
    private const float _progressInterval = 2; // Интервал
    private float _progress = 0; // Прогресс
    private float _point = 0;

    private ICivilization _civilization;
    private IndustryData _industryData;

    public Industry() { }

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        _industryData = this._civilization.DataBase.Industry;

        Points = _industryData.Points / 100f;
        Acceleration = _industryData.Acceleration;

        _civilization.ExicuteIndustryPoints(Points);
    }

    public void AddPoints(float count)
    {
        Points += count / 100;
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
    private float _acceleration;
    public int Acceleration { get => (int)(_acceleration * 100); set => _acceleration = value / 100f; }
    private int _shields = 0; // Щиты(защищают индустрию)
    public int Shields {
        get => _shields; set {
            _shields = value;
            if (_shields > 100) _shields = 100;
            if (_shields < 0) _shields = 0;
        }
    }

    // Рост индустрии
    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false || Points >= maxPoint) return;

        _progress += deltaTime * _acceleration;

        if (_progress > _progressInterval)
        {
            _progress -= _progressInterval;
            Points += 0.01f;
        }
    }

    public string GetInfo(bool isPlayer = true)
    {
        string info = string.Empty;

        if (isPlayer)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("efficiency")}: <color=lime>{(int)(Points * 100)}%</color>\r\n";
            info += $"{LocalisationGame.Instance.GetLocalisationString("acceleration")}:<color=lime> {Acceleration}%</color>\r\n";
            info += $"{LocalisationGame.Instance.GetLocalisationString("shields")}: <color=lime>{Shields}</color>\r\n";
        }

        return info;
    }
}