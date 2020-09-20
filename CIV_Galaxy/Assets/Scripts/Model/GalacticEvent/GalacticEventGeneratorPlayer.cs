using UnityEngine;

public class GalacticEventGeneratorPlayer : GalacticEventGenerator
{
    private IGalaxyUITimer _galaxyUITimer;
    private IGalacticEventDisplay _galacticEventDisplay;

    public GalacticEventGeneratorPlayer(IGalaxyUITimer galaxyUITimer, IGalacticEventDisplay galacticEventDisplay)
    {
        this._galaxyUITimer = galaxyUITimer;
        this._galacticEventDisplay = galacticEventDisplay;
    }

    protected override void StartNewGalacticEvent()
    {
        _galacticEventDisplay.Show(SelectEvent());
        _progressInterval = GetInterval; // Задать время для срабатывания нового события
    }
}