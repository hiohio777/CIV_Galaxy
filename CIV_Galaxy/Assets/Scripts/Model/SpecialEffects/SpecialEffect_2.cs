using UnityEngine;

public class SpecialEffect_2 : SpecialEffectBase
{
    [SerializeField] private SpriteRenderer art0, art1;

    public void Initialize(IGalaxyUITimer galaxyUITimer, Vector3 position, Sprite spriteEffect)
    {
        SetGalaxyUITimer(galaxyUITimer);
        art0.sprite = art1.sprite = spriteEffect;

        InitializeBase(position, "DisplayEffect_2");
    }
}
