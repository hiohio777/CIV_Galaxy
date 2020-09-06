using UnityEngine;

[CreateAssetMenu(fileName = "CivilizationScriptable", menuName = "Data/Civilization", order = 51)]
public class CivilizationScriptable : ScriptableObject
{
    [SerializeField, Space(10)] private Sprite icon = null;

    [SerializeField, Space(10)] private bool isScan = true; // Разрешение сканировать галактику(искать планеты сканером)
    [SerializeField, Range(1, 60)] private float timeScan = 5; // Интервал между сканированиями галактики в поиске планет
    [SerializeField, Range(1, 100)] private int planets = 1;

    public Sprite Icon => icon;
    public string Name => name;
    public string Description => $"{name}_description";

    public bool IsOpen { get; private set; }

    public float TimeScan => timeScan;
    public int Planets => planets;

    private void OnEnable()
    {
        if (timeScan == 0)
            timeScan = Random.Range(5,10);
    }
}
