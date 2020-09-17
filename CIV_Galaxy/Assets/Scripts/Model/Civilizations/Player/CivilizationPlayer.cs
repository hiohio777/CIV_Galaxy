using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CivilizationPlayer : CivilizationBase, ICivilization, ICivilizationPlayer
{
    private ScanerPanelUI _scanerPanelUI;
    private SciencePanelUI _sciencePanelUI;
    
    private DiscoveredCivilization _discoveredCivilization;

    [Inject]
    public void InjectCivilizationPlayer(DiscoveredCivilization discoveredCivilization, SciencePanelUI sciencePanelUI,
       ScanerPanelUI scanerPanelUI)
    {
        (this._discoveredCivilization, this._sciencePanelUI, this._scanerPanelUI)
        = (discoveredCivilization, sciencePanelUI, scanerPanelUI);
    }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        civilizationUI.Assign(civData);
        IsOpen = true;
    }

    public override void ExicuteScanning()
    {
        _discoveredCivilization.DiscoverAnotherCiv(anotherCiv);
    }

    public override void ExicuteSciencePoints(int sciencePoints)
    {
        _sciencePanelUI.SetSciencePoints(sciencePoints, ScienceCiv.IsAvailableForStudy());
    }

    private void OnEnable()
    {
        ScanerPlanets.ProgressEvent += _scanerPanelUI.ProgressEvent;
        ScienceCiv.ProgressEvent += _sciencePanelUI.ProgressEvent;
    }

    private void OnDisable()
    {
        ScanerPlanets.ProgressEvent -= _scanerPanelUI.ProgressEvent;
        ScienceCiv.ProgressEvent -= _sciencePanelUI.ProgressEvent;
    }
}