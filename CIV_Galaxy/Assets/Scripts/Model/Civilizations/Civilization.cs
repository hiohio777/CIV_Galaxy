using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Civilization : CivilizationBase, ICivilization, ICivilizationAl
{
    [SerializeField] private Image leaderIcon, dominatorIcon;
    private GalacticEventGenerator _galacticEventGenerator;
    private ICivilizationPlayer _player;

    [Inject]
    public void InjectCivilizationPlayer(GalacticEventGenerator galacticEventGenerator, ICivilizationPlayer player)
    {
        (this._galacticEventGenerator, this._player) = (galacticEventGenerator, player);
    }

    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);

        _galacticEventGenerator.Initialize(this);
    }

    public void Open()
    {
        civilizationUI.Assign(DataBase);
        IsOpen = true;
    }

    public override void ExicuteSciencePoints(int sciencePoints)
    {
        ScienceCiv.ExicuteSciencePointsAl();
    }

    public override void ExicuteScanning()
    { }

    public override void ExicuteAbility(IAbility ability) { }

    private void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Default";
        leaderIcon.enabled = dominatorIcon.enabled = false;

        civilizationUI.Close();
    }

    public override void DefineLeader()
    {
        // определение лидерства
        if (CivData.DominationPoints >= _player.CivData.DominationPoints)
        {
            dominatorIcon.enabled = true;
        }
        else
        {
            leaderIcon.enabled = dominatorIcon.enabled = false;
        }
    }
}
