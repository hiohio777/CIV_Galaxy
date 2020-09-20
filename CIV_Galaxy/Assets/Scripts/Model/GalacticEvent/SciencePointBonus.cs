using UnityEngine;

public class SciencePointBonus : GalacticEvent
{
    public SciencePointBonus(ICivilization civilization) : base(civilization)
    { }

    public override float Execute()
    {
        float count = 1;
        _civilization.ScienceCiv.AddPoints((int)count);

        return count;
    }
}
