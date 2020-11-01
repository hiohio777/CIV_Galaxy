using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsPurchase : MonoBehaviour
{
    [SerializeField] private string keyDataPlayer = "StatisticsGame";
    [SerializeField] private string keyDataPurchase = "PurchaseGame";

    private Dictionary<string, List<StatisticsData>> data;

    // Покупки новых цивилизаций(одноразовые)
    private List<string> dataPurchase;

    private void Start() => LoadDate();

    public bool IsValidatePurchase(string id) => dataPurchase.Contains(id);
    public void MakePurchase(string id)
    {
        Debug.Log(id);
        if (dataPurchase.Contains(id) == false)
        {
            dataPurchase.Add(id);
            SaveDate();
        }
    }

    public StatisticsData GetStatistics(string nameCiv, DifficultEnum difficult, OpponentsEnum opponents) =>
        data[nameCiv].Where(x => x.Difficult == difficult && x.Opponents == opponents).FirstOrDefault();

    public void SaveDate()
    {
        PlayerPrefs.SetString(keyDataPlayer, JsonConvert.SerializeObject(data));
        PlayerPrefs.SetString(keyDataPurchase, JsonConvert.SerializeObject(dataPurchase));
        PlayerPrefs.Save();
    }

    public void LoadDate()
    {
        var yyyy = new MyIAPManager();
        string resultStatistics = PlayerPrefs.GetString(keyDataPlayer, "Done");
        if (resultStatistics == "Done") CreateNewStatistics(); // Данных нет, будут созданы новые
        else data = JsonConvert.DeserializeObject<Dictionary<string, List<StatisticsData>>>(resultStatistics);

        string resultPurchase = PlayerPrefs.GetString(keyDataPurchase, "Done");
        if (resultPurchase == "Done") dataPurchase = new List<string> { "galaxy_domination_humanity", "galaxy_domination_shaktalasi" }; // Данных нет, будут созданы новые
        else dataPurchase = JsonConvert.DeserializeObject<List<string>>(resultPurchase);

        foreach (var item in dataPurchase)
        {
            Debug.Log(item);
        }
    }

    // Создание пустых данных статистики
    private void CreateNewStatistics()
    {
        data = new Dictionary<string, List<StatisticsData>>();
        foreach (var item in Civilizations.Instance.Refresh())
        {
            var stat = new List<StatisticsData>();
            for (int i = 0; i < 3; i++)
                for (int t = 0; t < 3; t++)
                    stat.Add(new StatisticsData(item.Name, i, t));

            data.Add(item.Name, stat);
        }

#if UNITY_EDITOR
        Debug.Log("Statistics: Данные статистики созданы заново!");
#endif
    }
}