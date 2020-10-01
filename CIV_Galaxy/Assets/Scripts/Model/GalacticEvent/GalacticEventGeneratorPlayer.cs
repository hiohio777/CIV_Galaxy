using UnityEngine;

public class GalacticEventGeneratorPlayer : GalacticEventGenerator
{
    private IGalacticEventDisplay _galacticEventDisplay;

    public GalacticEventGeneratorPlayer(IGalacticEventDisplay galacticEventDisplay)
    {
        this._galacticEventDisplay = galacticEventDisplay;
    }

    protected override void StartNewGalacticEvent()
    {
        _galacticEventDisplay.Show(SelectEvent());
        _progressInterval = GetInterval; // Задать время для срабатывания нового события
    }
}