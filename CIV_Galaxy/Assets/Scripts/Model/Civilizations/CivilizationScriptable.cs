﻿using UnityEngine;
using System;

[Serializable]
public class BaseData
{
    [SerializeField, Space(10), Range(1, 100)] private int planets = 1; // Начальное количество планет
    [SerializeField, Space(10)] private int dominationPoints = 0; // Начальные поинты доминирования
    [SerializeField] private float growthDominanceOverall = 0; // роста доминирования(общий в процентах к годовому приросту)
    [SerializeField] private float growthDominancePlanets = 0; // роста доминирования от планет(от одной планеты)
    [SerializeField] private float growthDominanceIndustry = 0; // роста доминирования от Индустрии(процент)
    [SerializeField] private float growthDominanceScience = 0; // роста доминирования от науки(процент)

    public float GrowthDominanceOverall => growthDominanceOverall; 
    public float GrowthDominancePlanets => growthDominancePlanets;  
    public float GrowthDominanceIndustry => growthDominanceIndustry;
    public float GrowthDominanceScience => growthDominanceScience;

    public int Planets => planets;
    public int DominationPoints => dominationPoints;

    public void Validate()
    {
        if (planets <= 0) planets = 1;

        if (growthDominancePlanets <= 0) growthDominancePlanets = 1;
    }
}

[Serializable]
public class ScanerData
{
    [SerializeField, Space(10), Space(10)] private bool isScan = true; // Разрешение сканировать галактику(искать планеты сканером)
    [SerializeField, Range(0, 60)] private float acceleration = 0; // Интервал между сканированиями галактики в поиске планет
    [SerializeField, Range(0, 20)] private int minimumDiscoveredPlanets = 0; // Минимальное количество открываемых планет
    [SerializeField, Range(0, 20)] private int randomDiscoveredPlanets = 0; // Рандомное количество открываемых планет

    public float Acceleration => acceleration;
    public int MinimumDiscoveredPlanets => minimumDiscoveredPlanets;
    public int RandomDiscoveredPlanets => randomDiscoveredPlanets;

    public void Validate()
    {
        if (acceleration <= 0)
            acceleration = UnityEngine.Random.Range(9f, 10f);
        if (minimumDiscoveredPlanets <= 0)
            minimumDiscoveredPlanets = 1;
    }
}

[Serializable]
public class ScienceData
{
    [SerializeField, Space(10)] private TreeOfScience treeOfSciencePrefab; // Префаб дерева науки
    [SerializeField, Range(0, 100)] private int Points = 0; // Начальные очки науки
    [SerializeField, Range(0, 100)] private float acceleration = 0; // Скорость накопления очков исследований

    public TreeOfScience TreeOfSciencePrefab => treeOfSciencePrefab;
    public int SciencePoints => Points;
    public float Acceleration => acceleration;

    public void Validate(string name)
    {
        if (acceleration <= 0)
            acceleration = UnityEngine.Random.Range(18f, 20f);
        if (treeOfSciencePrefab == null)
            Debug.Log($"{name}: Не назначено дерево наук!");
    }
}

[Serializable]
public class IndustryData
{
    [SerializeField, Space(10), Range(0, 60)] private float acceleration = 0; // Интервал между между ростом индустрии(добавление поинта)
    [SerializeField, Range(0, 100)] private int points = 0; // Начальные очки индустрии
    [SerializeField, Range(0, 100)] private int maxPoints = 0; // Максимальное количество очков индустрии

    public float Acceleration => acceleration;
    public int Points => points;
    public int MaxPoints => maxPoints;

    public void Validate()
    {
        if (acceleration <= 0)
            acceleration = UnityEngine.Random.Range(2f, 3f);
        if (maxPoints <= 0)
            maxPoints = UnityEngine.Random.Range(18, 20);
    }
}

[CreateAssetMenu(fileName = "CivilizationScriptable", menuName = "Data/Civilization", order = 51)]
public class CivilizationScriptable : ScriptableObject
{
    [SerializeField, Space(10)] private Sprite icon = null;

    [SerializeField] private BaseData baseData;
    [SerializeField] private ScanerData scanerData;
    [SerializeField] private ScienceData scienceData;
    [SerializeField] private IndustryData industryData;

    public Sprite Icon => icon;
    public string Name => name;
    public string Description => $"{name}_description";

    public BaseData Base => baseData;
    public ScanerData Scaner => scanerData;
    public ScienceData Science => scienceData;
    public IndustryData Industry => industryData;

    private void OnEnable()
    {
        baseData.Validate();
        scanerData.Validate();
        scienceData.Validate(Name);
        industryData.Validate();
    }
}
