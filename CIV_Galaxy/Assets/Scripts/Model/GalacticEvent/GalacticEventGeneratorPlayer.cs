public class GalacticEventGeneratorPlayer : GalacticEventGenerator
{
    private IGalacticEventDisplay _galacticEventDisplay;

    public GalacticEventGeneratorPlayer(IGalacticEventDisplay galacticEventDisplay, IGalaxyUITimer galaxyUITimer) : base(galaxyUITimer)
    {
        this._galacticEventDisplay = galacticEventDisplay;
    }

    protected override void StartNewGalacticEvent() => _galacticEventDisplay.Show(Execute, _typeEvent);
}