using UnityEngine;

[CreateAssetMenu(fileName = "DifficultSettingsScriptable", menuName = "Data/DifficultSettings", order = 54)]
public class DifficultSettingsScriptable : ScriptableObject
{
    [SerializeField, Range(0, 50), Header("Агрессивность по отношению к игроку")] private int dangerPlayer = 0;
    [SerializeField, Range(50, 200), Header("Время получения поинта")] private int scienceSecund = 0;
    [SerializeField, Range(50, 200), Header("Время сканирования")] private int scanerSecund = 0;
    [SerializeField, Range(0, 200), Header("Бонус к общему росту доминирования")] private int gdOverall = 0;

    public int DangerPlayer => dangerPlayer;
    public int ScienceProc => scienceSecund;
    public int ScanerProc => scanerSecund;
    public int GDOverall => gdOverall;
}