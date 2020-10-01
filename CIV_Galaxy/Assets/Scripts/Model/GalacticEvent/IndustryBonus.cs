using UnityEngine;

public class IndustryBonus : GalacticEvent
{
    public IndustryBonus(ICivilization civilization) : base(civilization)
    { }

    public override float Execute()
    {
        float count = 0.15f;
        _civilization.IndustryCiv.Points += count;

        return count;
    }
}
