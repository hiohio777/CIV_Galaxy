using UnityEngine;

public class SpecialEffect_0 : SpecialEffectBase
{
    [SerializeField] private SpriteRenderer art;

    public void Initialize(IGalaxyUITimer galaxyUITimer, Vector3 position, Sprite spriteEffect)
    {
        SetGalaxyUITimer(galaxyUITimer);
        art.sprite = spriteEffect;

        InitializeBase(position, "DisplayEffect_0");
    }
}
