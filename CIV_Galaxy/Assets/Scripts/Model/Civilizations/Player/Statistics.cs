using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Statistics
{
    private static readonly Statistics _instance = new Statistics();
    public static Statistics Instance => _instance;

    private static string PATH = $"{Application.dataPath}/StreamingAssets/Statistics.dat";
    private Dictionary<string, List<StatisticsData>> data;

    private Statistics()
    {
        LoadDate();
    }

    public void SaveDate()
    {
        var formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
        using (var fs = new FileStream(PATH, FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, data);
        }
    }

    public StatisticsData GetStatistics(string nameCiv, DifficultEnum difficult, OpponentsEnum opponents) =>
        data[nameCiv].Where(x => x.Difficult == difficult && x.Opponents == opponents).FirstOrDefault();

    private void LoadDate()
    {
        if (File.Exists(PATH))
        {
            var formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
            using (var fs = new FileStream($"{PATH}", FileMode.Open))
            {
                data = (Dictionary<string, List<StatisticsData>>)formatter.Deserialize(fs);
            }

            return;
        }

        Civilizations.Instance.Refresh();
        data = new Dictionary<string, List<StatisticsData>>();
        foreach (var item in Civilizations.Instance)
        {
            var stat = new List<StatisticsData>()
            {
                new StatisticsData(DifficultEnum.Easy, OpponentsEnum.Two),
                new StatisticsData(DifficultEnum.Easy, OpponentsEnum.Four),
                new StatisticsData(DifficultEnum.Easy, OpponentsEnum.Six),

                new StatisticsData(DifficultEnum.Medium, OpponentsEnum.Two),
                new StatisticsData(DifficultEnum.Medium, OpponentsEnum.Four),
                new StatisticsData(DifficultEnum.Medium, OpponentsEnum.Six),

                new StatisticsData(DifficultEnum.Difficult, OpponentsEnum.Two),
                new StatisticsData(DifficultEnum.Difficult, OpponentsEnum.Four),
                new StatisticsData(DifficultEnum.Difficult, OpponentsEnum.Six),
            };

            data.Add(item.Name, stat);
        }
    }
}
