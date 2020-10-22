using UnityEngine;

public class SpecialEffectFactory : BaseFactory
{
    private IGalaxyUITimer _galaxyUITimer;

    [SerializeField] private SpecialEffect_0 effect_0;
    [SerializeField] private SpecialEffect_1 effect_1;
    [SerializeField] private SpecialEffect_2 effect_2;

    private void Start()
    {
        this._galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
    }

    public void GetEffect(Vector3 position, Sprite spriteEffect, EffectEnum effectEnum = EffectEnum.SpecialEffect_0)
    {
        switch (effectEnum)
        {
            case EffectEnum.SpecialEffect_0: InstantiateObject(effect_0).Initialize(_galaxyUITimer, position, spriteEffect); break;
            case EffectEnum.SpecialEffect_1: InstantiateObject(effect_1).Initialize(_galaxyUITimer, position, spriteEffect); break;
            case EffectEnum.SpecialEffect_2: InstantiateObject(effect_2).Initialize(_galaxyUITimer, position, spriteEffect); break;
        }
    }
}
