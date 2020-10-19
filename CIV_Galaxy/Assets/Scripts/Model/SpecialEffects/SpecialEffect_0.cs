using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpecialEffect_0 : SpecialEffectBase
{
    [SerializeField] private SpriteRenderer art;

    public class Factory : PlaceholderFactory<SpecialEffect_0> { }

    public void Initialize(Vector3 position, Sprite spriteEffect)
    {
        art.sprite = spriteEffect;

        InitializeBase(position, "DisplayEffect_0");
    }
}
