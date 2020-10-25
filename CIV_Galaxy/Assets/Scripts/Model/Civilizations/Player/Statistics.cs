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
    private static Statistics instance;
    public static Statistics Instance {
        get {
            if (instance == null)
                return instance = new Statistics();
            return instance;
        }
    }

    private Dictionary<string, List<StatisticsData>> data;
    private string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Statistics.dat");

    private Statistics()
    {
        LoadDate();
    }

    public StatisticsData GetStatistics(string nameCiv, DifficultEnum difficult, OpponentsEnum opponents) =>
        data[nameCiv].Where(x => x.Difficult == difficult && x.Opponents == opponents).FirstOrDefault();

    public void SaveDate()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                var formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, data);
                }
                break;
            case RuntimePlatform.Android:
                var jsonString = "";// JsonConvert.SerializeObject(data);
                PlayerPrefs.SetString("Statistics", jsonString);
                PlayerPrefs.Save();
                break;
        }
    }

    public void LoadDate()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor: LoadFromWindows(); break;
            case RuntimePlatform.Android: LoadFromAndroid(); break;
            default: CreateNew(); break;
        }
    }

    private void LoadFromWindows()
    {
        if (File.Exists(filePath))
        {
            var formatter = new BinaryFormatter(); // создаем объект BinaryFormatter
            using (var fs = new FileStream(filePath, FileMode.Open))
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
        string result = PlayerPrefs.GetString("Statistics", "");
        if (result != "")
        {
            //data = JsonConvert.DeserializeObject<Dictionary<string, List<StatisticsData>>>(result);
            CreateNew();
        }
        else
            CreateNew();
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