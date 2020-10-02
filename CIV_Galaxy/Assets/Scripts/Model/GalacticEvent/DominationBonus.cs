using UnityEngine;

public class DominationBonus : GalacticEvent
{
    public DominationBonus(ICivilization civilization) : base(civilization)
    { }

    public override float Execute()
    {
        float count = _civilization.CivData.Planets * 1.5f;
        _civilization.CivData.AddDominance(count);

        return count;
    }
}
