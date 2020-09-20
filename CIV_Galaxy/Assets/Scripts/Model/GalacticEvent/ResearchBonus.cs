using UnityEngine;

public class ResearchBonus : GalacticEvent
{
    public ResearchBonus(ICivilization civilization) : base(civilization)
    { }

    public override float Execute()
    {
        float count = 3f;
        _civilization.ScienceCiv.AddProgress(count);

        return count;
    }
}
