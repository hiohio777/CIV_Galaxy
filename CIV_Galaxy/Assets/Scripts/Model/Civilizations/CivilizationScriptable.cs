using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseData
{
    [SerializeField, Space(10), Range(1, 100)] private int planets = 1; // Начальное количество планет
    [SerializeField, Space(10)] private int dominationPoints = 0; // Начальные поинты доминирования
    [SerializeField, Range(0, 300)] private int gdOverall = 0; // роста доминирования(общий в процентах к годовому приросту)
    [SerializeField, Range(0, 300)] private int gdPlanets = 0; // роста доминирования от планет(от одной планеты)
    [SerializeField, Range(0, 300)] private int gdIndustry = 0; // роста доминирования от Индустрии(процент)
    [SerializeField, Range(0, 300)] private int shields = 0; // Щиты

    public int GDOverall => gdOverall;
    public int GDPlanets => gdPlanets;
    public int GDIndustry => gdIndustry;

    public int Planets => planets;
    public int DominationPoints => dominationPoints;
    public int Shields => shields;

    public void Validate()
    {
        if (planets <= 0) planets = 1;

        if (gdPlanets == 0) gdPlanets = 100;
        if (gdIndustry == 0) gdIndustry = 50;
    }
}

[Serializable]
public class ScanerData
{
    [SerializeField, Range(0, 300)] private int acceleration = 0; // Интервал между сканированиями галактики в поиске планет
    [SerializeField, Range(0, 20)] private int minimumDiscoveredPlanets = 0; // Минимальное количество открываемых планет
    [SerializeField, Range(0, 20)] private int randomDiscoveredPlanets = 0; // Рандомное количество открываемых планет

    public int Acceleration => acceleration;
    public int MinimumDiscoveredPlanets => minimumDiscoveredPlanets;
    public int RandomDiscoveredPlanets => randomDiscoveredPlanets;

    public void Validate()
    {
        if (acceleration <= 0)
            acceleration = 100;
        if (minimumDiscoveredPlanets <= 0)
            minimumDiscoveredPlanets = 1;
    }
}

[Serializable]
public class ScienceData
{
    [SerializeField, Space(10)] private TreeOfScience treeOfSciencePrefab; // Префаб дерева науки
    [SerializeField, Range(0, 100)] private int Points = 0; // Начальные очки науки
    [SerializeField, Range(0, 300)] private int acceleration = 0; // Скорость накопления очков исследований

    public TreeOfScience TreeOfSciencePrefab => treeOfSciencePrefab;
    public int SciencePoints => Points;
    public int Acceleration => acceleration;

    public void Validate(string name)
    {
        if (acceleration <= 0)
            acceleration = 100;
        if (treeOfSciencePrefab == null)
            Debug.Log($"{name}: Не назначено дерево наук!");
    }
}

[Serializable]
public class IndustryData
{
    [SerializeField, Space(10), Range(0, 300)] private int acceleration = 0; // Скорость в процентах
    [SerializeField, Range(0, 100)] private int points = 0; // Начальные очки индустрии

    public int Acceleration => acceleration;
    public int Points => points;

    public void Validate()
    {
        if (acceleration <= 0)
            acceleration = 100;
        if (points <= 0)
            points = UnityEngine.Random.Range(0, 50);
    }
}

[CreateAssetMenu(fileName = "CivilizationScriptable", menuName = "Data/Civilization", order = 51)]
public class CivilizationScriptable : ScriptableObject
{
    [SerializeField, Space(10)] private Sprite icon = null;
    [SerializeField] private Color color = Color.red;

    [SerializeField, Space(10)] private BaseData baseData;
    [SerializeField] private ScanerData scanerData;
    [SerializeField] private ScienceData scienceData;
    [SerializeField] private IndustryData industryData;

    [SerializeField] private List<GameObject> abilities;

    public Sprite Icon => icon;
    public Color ColorCiv => color;
    public string Name => name;
    public string Description => $"{name}_description";

    public BaseData Base => baseData;
    public ScanerData Scaner => scanerData;
    public ScienceData Science => scienceData;
    public IndustryData Industry => industryData;

    public List<GameObject> Abilities => abilities;

    private void OnEnable()
    {
        baseData.Validate();
        scanerData.Validate();
        scienceData.Validate(Name);
        industryData.Validate();
    }
}
