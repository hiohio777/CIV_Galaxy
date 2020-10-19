using System;
using UnityEngine;
using Zenject;

public class SpecialEffect_1 : SpecialEffectBase 
{
    [SerializeField] private SpriteRenderer art0, art1, art2;

    public class Factory : PlaceholderFactory<SpecialEffect_1> { }

    public void Initialize(Vector3 position, Sprite spriteEffect)
    {
        art0.sprite = art1.sprite = art2.sprite = spriteEffect;

        InitializeBase(position, "DisplayEffect_1");
    }
}
