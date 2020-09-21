using System.Collections.Generic;
using System.Linq;
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
    private List<ICivilizationAl> _anotherCivilization;

    private DiscoveredCivilization _discoveredCivilization;

    [Inject]
    public void InjectCivilizationPlayer(DiscoveredCivilization discoveredCivilization, SciencePanelUI sciencePanelUI,
       ScanerPanelUI scanerPanelUI, PlayerCivInfo playerCivInfo, GalacticEventGeneratorPlayer galacticEventGenerator,
       List<AbilityUI> abilitiesUI, List<ICivilizationAl> anotherCivilization)
    {
        (this._discoveredCivilization, this._sciencePanelUI, this._scanerPanelUI, this._playerCivInfo,
        this._galacticEventGenerator, this._abilitiesUI)
        = (discoveredCivilization, sciencePanelUI, scanerPanelUI, playerCivInfo, galacticEventGenerator, abilitiesUI);

        _anotherCivilization = anotherCivilization;
    }

    public override TypeCivEnum TypeCiv { get; } = TypeCivEnum.Player;
    public AbilityUI SelectedAbility { get; set; }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        civilizationUI.Assign(civData);
        _galacticEventGenerator.Initialize(this);

        InitAbility();
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
        _discoveredCivilization.DiscoverAnotherCiv(_anotherCivilization);
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
        foreach (var item in _anotherCivilization)
        {
            if (CivData.Planets >= item.CivData.Planets)
                item.DefineLeader();
        }
    }
}