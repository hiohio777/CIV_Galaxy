using UnityEngine;

public class SpecialEffect_1 : SpecialEffectBase
{
    [SerializeField] private SpriteRenderer art0, art1, art2;

    public void Initialize(IGalaxyUITimer galaxyUITimer, Vector3 position, Sprite spriteEffect)
    {
        SetGalaxyUITimer(galaxyUITimer);
        art0.sprite = art1.sprite = art2.sprite = spriteEffect;

        InitializeBase(position, "DisplayEffect_1");
    }
}
