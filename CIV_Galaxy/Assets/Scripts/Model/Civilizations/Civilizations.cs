using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Civilizations
{
    private static readonly Civilizations _instance = new Civilizations();
    public static Civilizations Instance => _instance;

    private List<CivilizationScriptable> civilizationsData;

    private Civilizations()
    {
        Refresh();
    }

    public IEnumerator<CivilizationScriptable> GetEnumerator()
    {
        return civilizationsData.GetEnumerator();
    }

    public void Refresh() => civilizationsData = Resources.LoadAll<CivilizationScriptable>($"Civilizations/").ToList();

    public CivilizationScriptable GetCivilizationPlayer(string name, bool isDelet = true)
    {
        var civPlayer = civilizationsData.Where(x => x.Name == name).FirstOrDefault();
        if (isDelet) civilizationsData.Remove(civPlayer);
        return civPlayer;
    }

    public CivilizationScriptable GetCivilizationEnemy()
    {
        var rand = new System.Random();
        var id = rand.Next(0, civilizationsData.Count);
        var civEnemy = civilizationsData[id];
        civilizationsData.RemoveAt(id);

        return civEnemy;
    }
}
