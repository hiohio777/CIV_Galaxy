using System.Collections.Generic;
using Zenject;

public class CivilizationPlayer : CivilizationBase, ICivilization, ICivilizationPlayer
{
    private ScanerPanelUI _scanerPanelUI;
    private SciencePanelUI _sciencePanelUI;
    private List<AbilityUI> _abilitiesUI;
    private List<ICivilizationAl> _anotherCivilization;

    private DiscoveredCivilization _discoveredCivilization;

    [Inject]
    public void InjectCivilizationPlayer(DiscoveredCivilization discoveredCivilization, SciencePanelUI sciencePanelUI,
       ScanerPanelUI scanerPanelUI, GalacticEventGeneratorPlayer galacticEventGenerator,
       List<AbilityUI> abilitiesUI, List<ICivilizationAl> anotherCivilization)
    {
        (this._discoveredCivilization, this._sciencePanelUI, this._scanerPanelUI, this.EventGenerator, this._abilitiesUI)
        = (discoveredCivilization, sciencePanelUI, scanerPanelUI, galacticEventGenerator, abilitiesUI);

        _anotherCivilization = anotherCivilization;

    }

    private AbilityUI _selectedAbility;
    public AbilityUI SelectedAbility {
        get => _selectedAbility; set {
            if (value == null)
            {
                _selectedAbility = null;
                _anotherCivilization.ForEach(x => x.TurnOffFrame());
            }
            else
            {
                _selectedAbility = value;
                _anotherCivilization.ForEach(x => x.TurnOffFrame());
                _selectedAbility.Getability.SelectedApplayPlayer(_anotherCivilization);
            }
        }
    }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        civilizationUI.Assign(this);
        EventGenerator.Initialize(this);

        AbilityCiv.Initialize(this);
        for (int i = 0; i < _abilitiesUI.Count; i++)
        {
            if (i < AbilityCiv.Abilities.Count)
                _abilitiesUI[i].Assing(AbilityCiv.Abilities[i], AbilityCiv);
            else _abilitiesUI[i].gameObject.SetActive(false);
        }

        IsOpen = true;
    }

    public override void ExicuteScanning()
    {
        civilizationUI.ScanerEffect();
        _discoveredCivilization.DiscoverAnotherCiv(_anotherCivilization);
    }

    public override void ExicuteSciencePoints(int sciencePoints)
    {
        _sciencePanelUI.SetSciencePoints(sciencePoints, ScienceCiv.IsAvailableForStudy());
    }

    private void OnEnable()
    {
        ScanerCiv.ProgressEvent += _scanerPanelUI.ProgressEvent;
        ScienceCiv.ProgressEvent += _sciencePanelUI.ProgressEvent;
    }

    private void OnDisable()
    {
        ScanerCiv.ProgressEvent -= _scanerPanelUI.ProgressEvent;
        ScienceCiv.ProgressEvent -= _sciencePanelUI.ProgressEvent;
    }
}