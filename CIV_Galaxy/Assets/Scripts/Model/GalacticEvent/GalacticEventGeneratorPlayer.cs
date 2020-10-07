public class GalacticEventGeneratorPlayer : GalacticEventGenerator
{
    private IGalacticEventDisplay _galacticEventDisplay;

    public GalacticEventGeneratorPlayer(IGalacticEventDisplay galacticEventDisplay)
    {
        this._galacticEventDisplay = galacticEventDisplay;
    }

    protected override void StartNewGalacticEvent() => _galacticEventDisplay.Show(Execute, _typeEvent);
}