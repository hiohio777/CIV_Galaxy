using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CivilizationPlayer : CivilizationBase, ICivilization, ICivilizationPlayer
{
    [SerializeField, Space(10)] private Image scanerImage;
    [SerializeField] private Image scienceImage;
    [SerializeField] private Text SciencePoints;

    private DiscoveredCivilization _discoveredCivilization;

    [Inject]
    public void InjectCivilizationPlayer(DiscoveredCivilization discoveredCivilization)
    {
        this._discoveredCivilization = discoveredCivilization;
    }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        civilizationUI.Assign(civData);
        scanerImage.fillAmount = 0;
        SciencePoints.text = "0";

        ScanerPlanets.ProgressEvent += ScanerPlanets_ProgressUIEvent;
        ScienceCiv.ProgressEvent += ScienceCiv_ProgressEvent;

        IsOpen = true;
    }

    public override void ExicuteScanning()
    {
        _discoveredCivilization.DiscoverAnotherCiv(anotherCiv);
    }

    public override void ExicuteSciencePoints(int sciencePoints)
    {
        SciencePoints.text = sciencePoints.ToString();
    }

    private void ScienceCiv_ProgressEvent(float rogress)
    {
        scienceImage.fillAmount = rogress / 100;
    }

    private void ScanerPlanets_ProgressUIEvent(float rogress)
    {
        scanerImage.fillAmount = rogress / 100;
    }
}