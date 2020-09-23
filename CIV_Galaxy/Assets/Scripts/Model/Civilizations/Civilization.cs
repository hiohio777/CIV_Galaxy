using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public enum DiplomaticRelationsEnum
{
    Hatred = 0, Threat = 1, Neutrality = 2, Cooperation = 3, Friendship = 4
}

public class Civilization : CivilizationBase, ICivilization, ICivilizationAl
{
    [SerializeField, Space(10)] private Image leaderIcon;
    [SerializeField] private Image dominatorIcon, diplomaticRelationsIcon;
    [SerializeField] private Color colorHatred, colorThreat, colorNeutrality, colorCooperation, colorFriendship;

    private GalacticEventGenerator _galacticEventGenerator;
    private ICivilizationPlayer _player;
    private Diplomacy diplomacy;

    [Inject]
    public void InjectCivilizationPlayer(GalacticEventGenerator galacticEventGenerator, ICivilizationPlayer player,
        Diplomacy diplomacyCiv)
    {
        (this._galacticEventGenerator, this._player, this.diplomacy)
        = (galacticEventGenerator, player, diplomacyCiv);
    }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        _galacticEventGenerator.Initialize(this);
    }

    public void Open()
    {
        civilizationUI.Assign(DataBase);
        diplomacy.Initialize(this);
        InitAbility();

        IsOpen = true;
    }

    public void OnClick()
    {
        if (_player.SelectedAbility == null) 
        {
            return;
        }
        else
        {
            _player.SelectedAbility.Apply(this);
            _player.SelectedAbility = null;
        }
    }

    public override void ExicuteSciencePoints(int sciencePoints)
    {
        ScienceCiv.ExicuteSciencePointsAl();
    }

    public override void ExicuteScanning()
    { }

    public override void ExicuteAbility(IAbility ability) 
    {
        ability.ApplyAl(diplomacy);
    }

    private void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Default";
        dominatorIcon.enabled = false;

        civilizationUI.Close();
    }

    public override void DefineLeader(LeaderEnum leaderEnum)
    {

        Debug.Log(leaderEnum);

        civilizationUI.SetAdvancedDomination(leaderEnum);
        switch (leaderEnum)
        {
            case LeaderEnum.Lagging:
                dominatorIcon.enabled = false;
                break;
            case LeaderEnum.Advanced:
                dominatorIcon.enabled = true;
                dominatorIcon.color = new Color(1, 1, 0, 0.15f);
                break;
            case LeaderEnum.Leader:
                dominatorIcon.enabled = true;
                dominatorIcon.color = new Color(1, 1, 0, 0.75f);
                break;
        }
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
