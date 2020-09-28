using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ability : CivilizationStructureBase
{
    public event Action<float> ProgressEvent; // Отображение на экране
    private float _progress, _maxProgress = 60;

    private AbilityFactory _abilityFactory;
    protected ICivilization _civilization;

    public Ability(AbilityFactory abilityFactory)
    {
        this._abilityFactory = abilityFactory;
    }

    public bool IsActive { get; set; } = false; // Активен ли(доступна ли способность)
    public List<IAbility> Abilities { get; private set; }

    //Бонусы
    public float AccelerationBonus { get; set; } = 1; // Бонус скорости работы
    public bool IsReady => _progress >= _maxProgress;

    public virtual void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        Abilities = _abilityFactory.GetAbilities(civilization);

        if (civilization is ICivilizationAl)
        {
            _progress = UnityEngine.Random.Range(0, _maxProgress);
            AssingCurrentAlAbility();
        }

        IsActive = true;
    }

    public void ReduceProgress() => _progress = 0;

    private void AssingCurrentAlAbility()
    {
        var abilities = Abilities.Where(x => x.IsActive).ToList();
        currentAlAbility = abilities[UnityEngine.Random.Range(0, abilities.Count)];
    }

    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        if (_progress < _maxProgress)
        {
            _progress += deltaTime * AccelerationBonus;

            if (_progress >= _maxProgress)
                _progress = _maxProgress;

            ProgressEvent?.Invoke(_progress / (_maxProgress / 100));

            return;
        }

        if (_civilization is ICivilizationAl alCiv)
            ApplyAl(alCiv.DiplomacyCiv);
    }

    private IAbility currentAlAbility; // Абилка которую хочет использовать Al
    private void ApplyAl(Diplomacy diplomacyCiv)
    {
        currentAlAbility.ApplyAl(diplomacyCiv);
        AssingCurrentAlAbility();
    }

}
