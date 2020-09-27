using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ability : CivilizationStructureBase
{
    protected float _progress, _maxProgress;

    private AbilityFactory _abilityFactory;

    public Ability(AbilityFactory abilityFactory)
    {
        this._abilityFactory = abilityFactory;
    }

    public bool IsActive { get; set; } = false; // Активен ли(доступна ли способность)
    public List<IAbility> Abilities { get; private set; }

    //Бонусы
    public float AccelerationBonus { get; set; } = 1; // Бонус скорости работы

    public virtual void Initialize(ICivilization civilization)
    {
        Abilities = _abilityFactory.GetAbilities(civilization);

        Abilities[0].IsActive = true;
        Abilities[1].IsActive = true;

        Abilities.ForEach(x => { if (x.IsActive) if (x.GetCost > _maxProgress) _maxProgress = x.GetCost; });

        if (civilization is ICivilizationAl)
        {
            _progress = UnityEngine.Random.Range(0, _maxProgress);
            AssingCurrentAlAbility();
        } 

        IsActive = true;
    }

    public void Activate<T>() where T : IAbility
    {
        foreach (var item in Abilities)
        {
            if (item.IsActive == false && item is T) 
            {
                item.IsActive = true;
                if (item.GetCost > _maxProgress) _maxProgress = item.GetCost;
            }
        }

        Abilities.ForEach(x => { });
    }

    public void ReduceProgress(float count)
    {
        _progress -= count;
        if (_progress < 0) _progress = 0;

        foreach (var item in Abilities)
        {
            if (_progress < item.GetCost) item.IsReady = false;
        }
    }

    private IAbility currentAlAbility; // Абилка которую хочет использовать Al
    public void ApplyAl(Diplomacy diplomacyCiv)
    {
        if (currentAlAbility.IsReady == false) return;

        currentAlAbility.ApplyAl(diplomacyCiv);
        AssingCurrentAlAbility();
    }

    private void AssingCurrentAlAbility()
    {
        var abilities = Abilities.Where(x => x.IsActive).ToList();
        currentAlAbility = abilities[UnityEngine.Random.Range(0, abilities.Count)];
    }

    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        _progress += deltaTime * AccelerationBonus;

        if (_progress >= _maxProgress)
            _progress = _maxProgress;

        Abilities.ForEach(x => x.ExecuteOnTimeEvent(_progress));
    }
}
