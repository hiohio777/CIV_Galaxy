using System;
using UnityEngine;

public class Civilization : CivilizationBase, ICivilization, ICivilizationAl
{
    public override void Assign(CivilizationScriptable civData)
    {
        base.Assign(civData);
    }

    public void Open()
    {
        civilizationUI.Assign(DataBase);
        IsOpen = true;
    }

    public override void ExicuteScanning()  { }

    public override void ExicuteSciencePoints(int sciencePoints)
    {
        ScienceCiv.ExicuteSciencePointsAl();
    }

    private void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Default";

        civilizationUI.Close();
    }
}
