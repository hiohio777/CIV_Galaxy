using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ability : CivilizationStructureBase
{
    public event Action<float> ProgressEvent; // Отображение на экране
    private float _progress, _progressInterval = 45;

    private AbilityFactory _abilityFactory;
    protected ICivilization _civilization;

    public Ability(AbilityFactory abilityFactory)
    {
        this._abilityFactory = abilityFactory;
    }

    public bool IsActive { get; set; } = false; // Активен ли(доступна ли способность)
    protected float ProgressProc => _progress / (_progressInterval / 100); // Прогресс в процентах
    public List<IAbility> Abilities { get; private set; }

    public void AddProgress(float count)
    {
        _progress += _progressInterval / 100 * count; ;
        ProgressEvent?.Invoke(ProgressProc);
    }

    //Бонусы
    private float _accelerationBonus = 0;
    public int AccelerationBonus { get => (int)(_accelerationBonus * 100); set => _accelerationBonus = value / 100f; }
    public bool IsReady => _progress >= _progressInterval;

    public virtual void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        Abilities = _abilityFactory.GetAbilities(civilization);

        if (civilization is ICivilizationAl)
        {
            _progress = UnityEngine.Random.Range(0, _progressInterval);
            AssingCurrentAlAbility();
        }

        IsActive = true;
    }

    public void ReduceProgress() => _progress = 1;

    private void AssingCurrentAlAbility()
    {
        var abilities = Abilities.Where(x => x.IsActive).ToList();
        currentAlAbility = abilities[UnityEngine.Random.Range(0, abilities.Count)];
    }

    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        if (IsActive == false) return;

        if (_progress < _progressInterval)
        {
            _progress += deltaTime * (1 + _civilization.IndustryCiv.Points / 2f + _accelerationBonus);

            if (_progress >= _progressInterval)
                _progress = _progressInterval;

            ProgressEvent?.Invoke(ProgressProc);

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
    public string GetInfo(bool isPlayer = true)
    {
        string info = string.Empty;

        if (isPlayer)
        {
            info += $"{LocalisationGame.Instance.GetLocalisationString("acceleration")}: <color=lime>{(int)((1 + _civilization.IndustryCiv.Points / 2f + _accelerationBonus) * 100)}%</color>\r\n";
            info += $"  <color=#add8e6ff>{LocalisationGame.Instance.GetLocalisationString("base")}:</color> <color=orange>{100 + AccelerationBonus}%</color>\r\n";
            info += $"  <color=#add8e6ff>{LocalisationGame.Instance.GetLocalisationString("industry")}:</color> <color=orange>{(int)(_civilization.IndustryCiv.Points / 2f * 100)}%</color>\r\n";
        }

        return info;
    }
}
