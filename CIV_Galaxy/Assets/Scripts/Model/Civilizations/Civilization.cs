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
        _moving = GetComponentInChildren<MovingObject>();
        _moving.AssignScale(0);
    }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        EventGenerator.Initialize(this);
        AbilityCiv.Initialize(this);
        AbilityCiv.IsActive = false;
    }

    /// <summary>
    /// Показать на экране значёк закрытой цивилизации в начале игры
    /// </summary>
    public void DisplayCloseCiv()
    {
        _moving.SetWaitForSeconds(0.8f).SetScale(1, 1f).Run();
    }

    public void Open()
    {
        DiplomacyCiv.Initialize(this);
        civilizationUI.Assign(this);
        AbilityCiv.IsActive = true;

        IsOpen = true;
    }

    public override void OnClick()
    {
        if (_player.SelectedAbility == null)
        {
            // Вывести информацию
            if (IsOpen) base.OnClick();
        }
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
