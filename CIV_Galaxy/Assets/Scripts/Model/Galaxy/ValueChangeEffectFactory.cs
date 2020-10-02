public class ValueChangeEffectFactory : BaseFactory
{
    private readonly ValueChangeEffect.Factory factory;

    public ValueChangeEffectFactory(ValueChangeEffect.Factory factory)
    {
        this.factory = factory;
    }

    public ValueChangeEffect GetEffect()
    {
        ValueChangeEffect buffParam;

        if (buffer.Count > 0) buffParam = buffer.Pop() as ValueChangeEffect;
        else buffParam = factory.Create(Buffered);

        return buffParam;
    }
}
