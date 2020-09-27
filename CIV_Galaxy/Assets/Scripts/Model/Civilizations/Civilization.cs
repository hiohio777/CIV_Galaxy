﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Civilization : CivilizationBase, ICivilization, ICivilizationAl
{
    [SerializeField, Space(10)] private Image leaderIcon;
    [SerializeField] private Image diplomaticRelationsIcon;
    [SerializeField] private Color colorHatred, colorThreat, colorNeutrality, colorCooperation, colorFriendship;

    private GalacticEventGenerator _galacticEventGenerator;
    private ICivilizationPlayer _player;

    public Diplomacy DiplomacyCiv { get; private set; }

    [Inject]
    public void InjectCivilizationPlayer(GalacticEventGenerator galacticEventGenerator, ICivilizationPlayer player,
        Diplomacy diplomacyCiv)
    {
        (this._galacticEventGenerator, this._player, this.DiplomacyCiv)
        = (galacticEventGenerator, player, diplomacyCiv);
    }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        _galacticEventGenerator.Initialize(this);
    }

    public void Open()
    {
        DiplomacyCiv.Initialize(this);
        AbilityCiv.Initialize(this);
        civilizationUI.Assign(this);

        IsOpen = true;
    }

    public void OnClick()
    {
        if (_player.SelectedAbility == null)
            return;
        else
        {
            if (_player.SelectedAbility.Apply(this))
                _player.SelectedAbility = null;
        }
    }

    public override void ExicuteSciencePoints(int sciencePoints)
    {
        ScienceCiv.ExicuteSciencePointsAl();
    }

    public override void ExicuteScanning()
    {
        if (IsOpen) civilizationUI.ScanerEffect();
    }

    public override void ExicuteAbility(IAbility ability)
    {
        AbilityCiv.ApplyAl(DiplomacyCiv);
    }

    private void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Default";

        civilizationUI.Close();
    }

    public void SetSetDiplomaticRelations(DiplomaticRelationsEnum relations)
    {
        switch (relations)
        {
            case DiplomaticRelationsEnum.Hatred: diplomaticRelationsIcon.color = colorHatred; break;
            case DiplomaticRelationsEnum.Threat: diplomaticRelationsIcon.color = colorThreat; break;
            case DiplomaticRelationsEnum.Neutrality: diplomaticRelationsIcon.color = colorNeutrality; break;
            case DiplomaticRelationsEnum.Cooperation: diplomaticRelationsIcon.color = colorCooperation; break;
            case DiplomaticRelationsEnum.Friendship: diplomaticRelationsIcon.color = colorFriendship; break;
        }
    }
}
