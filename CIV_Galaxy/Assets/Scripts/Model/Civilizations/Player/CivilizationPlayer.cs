using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CivilizationPlayer : CivilizationBase, ICivilization, ICivilizationPlayer
{
    private ScanerPanelUI _scanerPanelUI;
    private SciencePanelUI _sciencePanelUI;
    private IndustryPanelUI _industryPanelUI;
    
    private DiscoveredCivilization _discoveredCivilization;

    [Inject]
    public void InjectCivilizationPlayer(DiscoveredCivilization discoveredCivilization, SciencePanelUI sciencePanelUI,
       ScanerPanelUI scanerPanelUI, IndustryPanelUI industryPanelUI)
    {
        (this._discoveredCivilization, this._sciencePanelUI, this._scanerPanelUI, this._industryPanelUI)
        = (discoveredCivilization, sciencePanelUI, scanerPanelUI, industryPanelUI);
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
        _sciencePanelUI.SetSciencePoints(sciencePoints);
    }
    public override void ExicuteIndustryPoints(int points, float pointProc)
    {
        _industryPanelUI.SetIndustryPoints(points, pointProc);
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