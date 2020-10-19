using UnityEngine;

public class SpecialEffectFactory
{
    private readonly SpecialEffect_0.Factory _factory_0;
    private readonly SpecialEffect_1.Factory _factory_1;
    private readonly SpecialEffect_2.Factory _factory_2;

    public SpecialEffectFactory(SpecialEffect_0.Factory factory_0, SpecialEffect_1.Factory factory_1, SpecialEffect_2.Factory factory_2)
    {
        this._factory_0 = factory_0;
        this._factory_1 = factory_1;
        this._factory_2 = factory_2;
    }

    public void GetEffect(Vector3 position, Sprite spriteEffect, EffectEnum effectEnum = EffectEnum.SpecialEffect_0)
    {
        switch (effectEnum)
        {
            case EffectEnum.SpecialEffect_0: _factory_0.Create().Initialize(position, spriteEffect); break;
            case EffectEnum.SpecialEffect_1: _factory_1.Create().Initialize(position, spriteEffect); break;
            case EffectEnum.SpecialEffect_2: _factory_2.Create().Initialize(position, spriteEffect); break;
        }
    }
}
