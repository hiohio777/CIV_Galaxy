using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Statistics
{
    private static readonly Statistics _instance = new Statistics();
    public static Statistics Instance => _instance;

    private Dictionary<string, List<StatisticsData>> data;

    private Statistics()
    {
        LoadDate();
    }

    public void SaveDate()
    {
        //var formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
        //using (var fs = new FileStream(PATH, FileMode.OpenOrCreate))
        //{
        //    formatter.Serialize(fs, data);
        //}

        
    }

    public StatisticsData GetStatistics(string nameCiv, DifficultEnum difficult, OpponentsEnum opponents) =>
        data[nameCiv].Where(x => x.Difficult == difficult && x.Opponents == opponents).FirstOrDefault();

    private void LoadDate()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    LoadFromWindows();
                    break;
                case RuntimePlatform.Android:
                    break;
                default:
                    CreateNew();
                    break;
            }
    }

    private void LoadFromWindows()
    {
        string PATH = $"{Application.dataPath}/StreamingAssets/Statistics.dat";
        if (File.Exists(PATH))
        {
            var formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
            using (var fs = new FileStream($"{PATH}", FileMode.Open))
            {
                data = (Dictionary<string, List<StatisticsData>>)formatter.Deserialize(fs);
            }
        }
        else
        {
            // Данные не обнаружены
            CreateNew();
        }
    }

    private void LoadFromAndroid()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Settings.txt");
        UnityWebRequest wwwfile = new UnityWebRequest(filePath);

        if (string.IsNullOrEmpty(wwwfile.error))
        {
            // Данные не обнаружены
            CreateNew();
            return;
        }

        while (!wwwfile.isDone) { } // Ожидание загрузки данных

        // десериализация
        var formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
        using (var fs = new MemoryStream(wwwfile.downloadHandler.data))
        {
            data = (Dictionary<string, List<StatisticsData>>)formatter.Deserialize(fs);
        }
    }

    /// <summary>
    ///  Создание пустых данных статистики
    /// </summary>
    private void CreateNew()
    {
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