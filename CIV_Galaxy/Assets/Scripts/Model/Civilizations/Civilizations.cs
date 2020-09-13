using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Civilizations
{
    private List<CivilizationScriptable> civilizationsData;

    public Civilizations()
    {
        Reset();
    }

    public void Reset() =>  civilizationsData = Resources.LoadAll<CivilizationScriptable>($"Civilizations/").ToList();

    public CivilizationScriptable GetCivilizationPlayer(string name)
    {
        var civPlayer = civilizationsData.Where(x => x.Name == name).FirstOrDefault();
        civilizationsData.Remove(civPlayer);
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
