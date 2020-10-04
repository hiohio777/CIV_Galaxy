using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Civilization : CivilizationBase, ICivilization, ICivilizationAl
{
    [SerializeField, Space(10)] private Image leaderIcon, frame;
    [SerializeField] private Image diplomaticRelationsIcon;
    [SerializeField] private Color colorHatred, colorThreat, colorNeutrality, colorCooperation, colorFriendship;

    private ICivilizationPlayer _player;

    public Diplomacy DiplomacyCiv { get; private set; }

    public void EnableFrame(Color color)
    {
        frame.enabled = true;
        frame.color = color;
    }

    public void TurnOffFrame()
    {
        frame.enabled = false;
    }

    [Inject]
    public void InjectCivilizationPlayer(GalacticEventGenerator galacticEventGenerator, ICivilizationPlayer player,
        Diplomacy diplomacyCiv)
    {
        (this.EventGenerator, this._player, this.DiplomacyCiv)
        = (galacticEventGenerator, player, diplomacyCiv);

        TurnOffFrame();
        civilizationUI.Close();
    }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        EventGenerator.Initialize(this);
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
