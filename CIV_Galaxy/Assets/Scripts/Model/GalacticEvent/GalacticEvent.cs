using System;

public abstract class GalacticEvent
{
    protected ICivilization _civilization;

    protected GalacticEvent(ICivilization civilization)
    {
        this._civilization = civilization ?? throw new ArgumentNullException(nameof(civilization));
    }

    public abstract float Execute();
}
