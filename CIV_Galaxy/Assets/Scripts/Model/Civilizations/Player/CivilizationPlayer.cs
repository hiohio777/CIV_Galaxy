using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CivilizationPlayer : CivilizationBase, ICivilization, ICivilizationPlayer
{
    private ScanerPanelUI _scanerPanelUI;
    private SciencePanelUI _sciencePanelUI;
    private List<AbilityUI> _abilitiesUI;
    private PlayerCivInfo _playerCivInfo;
    private GalacticEventGenerator _galacticEventGenerator;

    private DiscoveredCivilization _discoveredCivilization;

    [Inject]
    public void InjectCivilizationPlayer(DiscoveredCivilization discoveredCivilization, SciencePanelUI sciencePanelUI,
       ScanerPanelUI scanerPanelUI, PlayerCivInfo playerCivInfo, GalacticEventGeneratorPlayer galacticEventGenerator,
       List<AbilityUI> abilitiesUI)
    {
        (this._discoveredCivilization, this._sciencePanelUI, this._scanerPanelUI, this._playerCivInfo,
        this._galacticEventGenerator, this._abilitiesUI)
        = (discoveredCivilization, sciencePanelUI, scanerPanelUI, playerCivInfo,
        galacticEventGenerator, abilitiesUI);
    }

    public AbilityUI SelectAbility { get; set; }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        civilizationUI.Assign(civData);
        _galacticEventGenerator.Initialize(this);

        for (int i = 0; i < _abilitiesUI.Count; i++)
        {
            if (i < Abilities.Count)
                _abilitiesUI[i].Assing(Abilities[i]);
            else _abilitiesUI[i].gameObject.SetActive(false);
        }

        IsOpen = true;
    }

    public void OpenPanelPlayerCivInfo() => _playerCivInfo.Show(this);

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

    public override void ExicuteAbility(IAbility ability)
    {
        _abilitiesUI[ability.Id].Select(false);
    }

    public override void DefineLeader()
    {
        // определение лидерства
        foreach (var item in anotherCiv)
        {
            if (CivData.Planets >= item.CivData.Planets)
                item.DefineLeader();
        }
    }
}