using System.Collections.Generic;
using UnityEngine;

public class CivilizationPlayer : CivilizationBase, ICivilization, ICivilizationPlayer
{
    private ScanerPanelUI _scanerPanelUI;
    private SciencePanelUI _sciencePanelUI;
    private List<AbilityUI> _abilitiesUI;
    private List<ICivilizationAl> _anotherCivilization;

    private DiscoveredCivilization _discoveredCivilization;

    public override void Start()
    {
        base.Start();
        _discoveredCivilization = new DiscoveredCivilization(GetRegisterObject<MessageFactory>());
        EventGenerator = new GalacticEventGeneratorPlayer(GetRegisterObject<IGalacticEventDisplay>(), GetRegisterObject<IGalaxyUITimer>());

        _sciencePanelUI = GetRegisterObject<SciencePanelUI>();
        _scanerPanelUI = GetRegisterObject<ScanerPanelUI>();
        _anotherCivilization = GetRegisterObjects<ICivilizationAl>();
        _abilitiesUI = GetRegisterObjects<AbilityUI>();

        ScanerCiv.ProgressEvent += _scanerPanelUI.ProgressEvent;
        ScienceCiv.ProgressEvent += _sciencePanelUI.ProgressEvent;
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
                _selectedAbility.Ability.SelectedApplayPlayer(_anotherCivilization);
            }
        }
    }

    /// <summary>
    /// Показать на экране
    /// </summary>
    public void DisplayCiv() => GetComponent<Animator>().SetTrigger("Display");

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

        DefineLeader(LeaderEnum.Leader);
        IsOpen = true;
    }

    public override void ExicuteScanning()
    {
        _audioSourceGame.PlayOneShot(1, 1);
        civilizationUI.ScanerEffect();
        _discoveredCivilization.DiscoverAnotherCiv(this, _anotherCivilization);
    }

    public override void ExicuteSciencePoints(int sciencePoints)
    {
        _sciencePanelUI.SetSciencePoints(sciencePoints, ScienceCiv.IsAvailableForStudy());
    }

    public override void AddCountPlanet(int count, bool isAdd)
    {
        if (isAdd) _audioSourceGame.PlayOneShot(0, 0.9f);
        else _audioSourceGame.PlayOneShot(2, 0.9f);

        base.AddCountPlanet(count);
    }
}