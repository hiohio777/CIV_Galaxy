using System;

public class CivilizationData
{
    private int _planets;
    private float _dominationPoints;

    private ICivilization _civilization;
    private LeaderQualifier _leaderQualifier;
    private DifficultSettingsScriptable _difficultSettings; // Настройки сложности

    public CivilizationData(IGalaxyUITimer galaxyUITimer, LeaderQualifier leaderQualifier)
    {

        galaxyUITimer.ExecuteYears += ProgressDominance;
        this._leaderQualifier = leaderQualifier;
        _difficultSettings = PlayerSettings.Instance.GetDifficultSettings;
    }

    public int Planets { get => _planets; set { _civilization.AddCountPlanet(value, _planets < value); _planets = value; } }
    public float DominationPoints {
        get => _dominationPoints; set {
            _dominationPoints = value;
            _leaderQualifier.DefineLeader(_civilization);
        }
    }

    private int _shields = 0; // Щиты
    public int Shields {
        get => _shields; set {
            _shields = value;
            if (_shields > 100) _shields = 100;
            if (_shields < 0) _shields = 0;
        }
    }

    //Бонусы
    private float _gdPlanets = 0;
    public int GDPlanets { get => (int)Math.Round(_gdPlanets * 100f, 0); set => _gdPlanets = value / 100f; }
    private float _gdIndustry = 0;
    public int GDIndustry { get => (int)Math.Round(_gdIndustry * 100f, 0); set => _gdIndustry = value / 100f; }
    private float _gdOverall = 0;
    public int GDOverall { get => (int)Math.Round(_gdOverall * 100f, 0); set => _gdOverall = value / 100f; }

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        _planets = this._civilization.DataBase.Base.Planets;

        DominationPoints = _civilization.DataBase.Base.DominationPoints;
        GDPlanets = _civilization.DataBase.Base.GDPlanets;
        GDIndustry = _civilization.DataBase.Base.GDIndustry;
        GDOverall = _civilization.DataBase.Base.GDOverall;

        Shields = _civilization.DataBase.Base.Shields;

        // Установка сложности
        if (civilization is ICivilizationAl)
            GDOverall += _difficultSettings.GDOverall;
    }

    public void AddPlanet(IPlanet planet)
    {
        Planets++;
        planet.Destroy();
    }

    public void AddDominance(float count)
    {
        DominationPoints += count;
        _civilization.CivUI.SetCountDominationPoints(DominationPoints, count);
    }

    public float GetPointsFromPlanets => _planets * _gdPlanets;
    public float GetPointsFromIndustry => _planets * (_civilization.IndustryCiv.Points * _gdIndustry);
    public float GetPointsFromBonus => GetPointsFromPlanets + GetPointsFromIndustry;
    public float GetPointsAll => GetPointsFromBonus + GetPointsFromBonus * _gdOverall;

    private void ProgressDominance()
    {
        // подсчёт
        float count = GetPointsAll;
        DominationPoints += count;

        _civilization.CivUI.SetCountDominationPoints(DominationPoints, count);
    }

    public string GetInfo(bool isPlayer = true)
    {
        string info = string.Empty;

        if (isPlayer)
        {
            info += $"<size=50><color=yellow>+{(int)GetPointsAll} ({GDOverall + 100}%)</color></size>\r\n\r\n";
            info += $"{LocalisationGame.Instance.GetLocalisationString("dominance_from_planets")}\r\n";
            info += $"<color=lime>+{(int)GetPointsFromPlanets} ({GDPlanets}%)</color>\r\n";
            info += $"{LocalisationGame.Instance.GetLocalisationString("dominance_from_industry")}\r\n";
            info += $"<color=lime>+{(int)GetPointsFromIndustry} ({(int)(_civilization.IndustryCiv.Points * GDIndustry)}%)</color>\r\n";
        }

        return info;
    }
}
