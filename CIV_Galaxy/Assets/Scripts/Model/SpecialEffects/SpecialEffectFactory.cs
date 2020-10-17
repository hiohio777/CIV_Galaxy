using UnityEngine;

public class SpecialEffectFactory : BaseFactory
{
    private readonly SpecialEffect.Factory _factory;

    public SpecialEffectFactory(SpecialEffect.Factory factory)
    {
        this._factory = factory;
    }

    public SpecialEffect GetEffect(Vector3 position, Sprite spriteEffect, EffectEnum effectEnum = EffectEnum.Standart)
    {
        SpecialEffect unit;

        if (buffer.Count > 0) unit = buffer.Pop() as SpecialEffect;
        else unit = _factory.Create(Buffered);

        switch (effectEnum)
        {
            case EffectEnum.Standart:
                unit.Initialize(position, spriteEffect);
                break;
        }

        return unit;
    }
}
