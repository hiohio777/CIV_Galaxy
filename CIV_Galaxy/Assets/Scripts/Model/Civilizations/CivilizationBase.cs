using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class CivilizationBase : MonoBehaviour, ICivilization
{
    [SerializeField] protected CivilizationUI civilizationUI;
    protected bool isAssign = false;
    private AbilityFactory _abilityFactory;

    [Inject]
    public void Inject(CivilizationData civData, Scanner scanerPlanets, Science scienceCiv,
        Industry industryCiv, AbilityFactory abilityFactory)
    {
        (this.CivData, this.ScanerPlanets, this.ScienceCiv, this.IndustryCiv, this._abilityFactory)
        = (civData, scanerPlanets, scienceCiv, industryCiv, abilityFactory);

        PositionCiv = transform.position;
    }

    public abstract TypeCivEnum TypeCiv { get; }
    public event Action<float> ExecuteOnTimeEvent;
    public bool IsOpen { get; protected set; }
    public Vector2 PositionCiv { get; private set; }
    public CivilizationScriptable DataBase { get; private set; }
    public CivilizationData CivData { get; private set; }
    public Scanner ScanerPlanets { get; private set; }
    public Industry IndustryCiv { get; private set; }
    public Science ScienceCiv { get; private set; }
    public List<IAbility> Abilities { get; private set; } = new List<IAbility>();

    public virtual void Assign(CivilizationScriptable dataBase)
    {
        this.DataBase = dataBase;

        // Инициализация данных
        CivData.Initialize(this.DataBase, civilizationUI);
        CivData.DefineLeader += DefineLeader;

        ScanerPlanets.Initialize(this);
        ScienceCiv.Initialize(this, IndustryCiv);
        IndustryCiv.Initialize(this);

        isAssign = true;
    }

    protected void InitAbility()
    {
        // Инициировать абилки
        Abilities = _abilityFactory.GetAbilities(this);
    }

    public void ExecuteOnTime(float deltaTime)
    {
        if (isAssign == false) return;

        ExecuteOnTimeEvent?.Invoke(deltaTime);
    }
    public void ExicuteIndustryPoints(float points)
    {
        civilizationUI.SetIndustryPoints(points);
    }

    public abstract void ExicuteScanning();
    public abstract void ExicuteSciencePoints(int sciencePoints);
    public abstract void ExicuteAbility(IAbility ability);
    public abstract void DefineLeader();
}
