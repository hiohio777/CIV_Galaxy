using UnityEngine;

[CreateAssetMenu(fileName = "CivilizationScriptable", menuName = "Data/Civilization", order = 51)]
public class CivilizationScriptable : ScriptableObject
{
    [SerializeField, Space(10)] private Sprite icon = null;

    [SerializeField, Space(10), Range(1, 100)] private int planets = 1; // Начальное количество планет

    [SerializeField, Space(10), Space(10)] private bool isScan = true; // Разрешение сканировать галактику(искать планеты сканером)
    [SerializeField, Range(1, 60)] private float scannerAcceleration = 10; // Интервал между сканированиями галактики в поиске планет
    [SerializeField, Range(1, 20)] private int minimumDiscoveredPlanets = 1; // Минимальное количество открываемых планет
    [SerializeField, Range(0, 20)] private int randomDiscoveredPlanets = 0; // Рандомное количество открываемых планет

    [SerializeField, Space(10)] private TreeOfScience treeOfSciencePrefab; // Префаб дерева науки
    [SerializeField, Range(1, 100)] private int sciencePoints = 0; // Начальные очки науки
    [SerializeField, Range(1, 100)] private float scienceRateResearch = 20; // Скорость накопления очков исследований


    public Sprite Icon => icon;
    public string Name => name;
    public string Description => $"{name}_description";

    public bool IsOpen { get; private set; }

    public int Planets => planets;

    public float ScannerAcceleration => scannerAcceleration;
    public int MinimumDiscoveredPlanets => minimumDiscoveredPlanets;
    public int RandomDiscoveredPlanets => randomDiscoveredPlanets;


    public TreeOfScience TreeOfSciencePrefab => treeOfSciencePrefab;
    public int SciencePoints => sciencePoints;
    public float ScienceRateResearch => scienceRateResearch;

    private void OnEnable()
    {
        if (scannerAcceleration == 0)
            scannerAcceleration = Random.Range(5,10);

        if (scienceRateResearch == 0)
            scannerAcceleration = Random.Range(15, 20);

        if (planets == 0) planets = 1;

        if (treeOfSciencePrefab == null) 
        {
            Debug.Log($"{Name}: Не назначено дерево наук!");
        }
    }
}
