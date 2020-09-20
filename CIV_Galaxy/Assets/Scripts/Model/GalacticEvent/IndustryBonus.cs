using UnityEngine;

public class IndustryBonus : GalacticEvent
{
    public IndustryBonus(ICivilization civilization) : base(civilization)
    { }

    public override float Execute()
    {
        float count = 0.10f;
        _civilization.IndustryCiv.AddPoints(count);

        return count;
    }
}
