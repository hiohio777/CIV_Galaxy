using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// Цивилизация игрока
/// </summary>
public class CivilizationPlayer : CivilizationBase, ICivilization, ICivilizationPlayer
{
    [SerializeField, Space(10)] private Image scaner;

    private DiscoveredCivilization discoveredCivilization;

    public string CurrentCivilization { get; set; } = "Humanity";

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        civilizationUI.Assign(civData);
        scaner.fillAmount = 0;

        discoveredCivilization = new DiscoveredCivilization(anotherCivilization.Count);
    }

    protected override void ExecuteOnTimeProcess()
    {
        scaner.fillAmount = scanPlanets.ScanProgressProc / 100;
    }

    protected override void ExicuteScanning()
    {
        // Debug.Log($"CivilizationPlayer: Сланирование!");

        discoveredCivilization.DiscoverAnotherCivilization(anotherCivilization);
    }
}

public class DiscoveredCivilization
{
    private int maxCivilization, procDiscoverAnotherCivilization, countDiscoveredCivilizations;

    public DiscoveredCivilization(int maxCivilization)
    {
        this.maxCivilization = maxCivilization;
    }

    // Попытаться открыть иную цивилизацию, если есть неоткрытые(цивилизации открываются по порядку с 1-вой)
    public void DiscoverAnotherCivilization(List<ICivilization> anotherCivilization)
    {
        if (countDiscoveredCivilizations >= maxCivilization)
            return; // все цивилизации открыты

        procDiscoverAnotherCivilization += 20; // Увеличить шанс открытия цивилизации

        Debug.Log(procDiscoverAnotherCivilization);
        if (Random.Range(0, 100) < procDiscoverAnotherCivilization)
        {
            // Открыть новую цивилизацию
            (anotherCivilization[countDiscoveredCivilizations] as ICivilizationAl).Open();

            procDiscoverAnotherCivilization = 0;
            countDiscoveredCivilizations++;
        }
    }
}

