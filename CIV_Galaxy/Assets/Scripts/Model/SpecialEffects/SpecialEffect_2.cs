using UnityEngine;
using Zenject;

public class SpecialEffect_2 : SpecialEffectBase
{
    [SerializeField] private SpriteRenderer art0, art1;

    public class Factory : PlaceholderFactory<SpecialEffect_2> { }

    public void Initialize(Vector3 position, Sprite spriteEffect)
    {
        art0.sprite = art1.sprite = spriteEffect;

        InitializeBase(position, "DisplayEffect_2");
    }
}
