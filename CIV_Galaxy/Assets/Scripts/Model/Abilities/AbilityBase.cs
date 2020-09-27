using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour, IAbility
{
    public event Action<float> ProgressEvent; // Отображение на экране

    [SerializeField] private Sprite spriteArtFon, spriteArt, frame;
    [SerializeField] private float costTime;

    protected ICivilization ThisCivilization { get; private set; }

    public string Name => name;
    public bool IsActive { get; set; } = false; // Активен ли(доступна ли способность)
    public bool IsReady { get; set; } = false; // Готовность зарядки
    public Sprite Fon => spriteArtFon;
    public Sprite Art => spriteArt;
    public Sprite Frame => frame;
    public float GetCost => costTime;


    public virtual void Initialize(ICivilization civilization)
    {
        (this.ThisCivilization) = (civilization);
    }

    public abstract bool ApplyAl(Diplomacy diplomacyCiv);
    public abstract bool Apply(ICivilization civilizationTarget);
    public abstract void SelectedApplayPlayer(List<ICivilizationAl> civilizationsTarget); // Выделение доступных целей

    public void ExecuteOnTimeEvent(float progress)
    {
        if (IsActive == false) return;

        if (IsReady == false)
        {
            if (progress >= costTime)
                IsReady = true;

            ProgressEvent?.Invoke(progress / (costTime / 100));
        }

        ThisCivilization.ExicuteAbility(this);
    }

    protected void Ready()
    {
        ThisCivilization.AbilityCiv.ReduceProgress(costTime);
    }
}
