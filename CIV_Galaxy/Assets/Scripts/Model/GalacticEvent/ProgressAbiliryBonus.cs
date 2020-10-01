public class ProgressAbiliryBonus : GalacticEvent
{
    public ProgressAbiliryBonus(ICivilization civilization) : base(civilization)
    { }

    public override float Execute()
    {
        float count = 10f;
        _civilization.AbilityCiv.AddProgress(count);

        return count;
    }
}