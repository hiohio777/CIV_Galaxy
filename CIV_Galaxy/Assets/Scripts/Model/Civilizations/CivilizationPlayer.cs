using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// Цивилизация игрока
/// </summary>
public class CivilizationPlayer : CivilizationBase, ICivilization, ICivilizationPlayer
{
    [SerializeField, Space(10)] private Image scaner;

    private DiscoveredCivilization _discoveredCivilization;

    private MessageDiscoveredCivilization.Factory _factoryMessageDiscovered;

    public string CurrentCivilization { get; set; } = "Humanity";

    [Inject]
    public void InjectCivilizationPlayer(MessageDiscoveredCivilization.Factory factoryMessageDiscovered)
    {
        this._factoryMessageDiscovered = factoryMessageDiscovered;
    }

    public override void Assign(CivilizationScriptable civData)
    {
        _positionCiv = transform.position;
        base.Assign(civData);

        civilizationUI.Assign(civData);
        scaner.fillAmount = 0;

        _discoveredCivilization = new DiscoveredCivilization(_factoryMessageDiscovered);

        IsOpen = true;
    }

    protected override void ExecuteOnTimeProcess()
    {
        if (scanerPlanets.IsActive)
            scaner.fillAmount = scanerPlanets.ScanProgressProc / 100;
    }

    protected override void ExicuteScanning()
    {
        // Debug.Log($"CivilizationPlayer: Сланирование!");
        bool isDiscovered = _discoveredCivilization.DiscoverAnotherCiv(anotherCiv);

        if (isDiscovered == false) ;

    }
}

