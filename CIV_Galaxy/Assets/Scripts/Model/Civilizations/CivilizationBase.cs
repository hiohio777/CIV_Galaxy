using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class CivilizationBase : MonoBehaviour, ICivilizationBase
{
    [SerializeField] protected CivilizationUI civilizationUI;
    protected bool isAssign = false;
    protected List<ICivilization> anotherCiv; // Другие цивилизации

    [Inject]
    public void Inject(List<ICivilization> anotherCivilization, CivilizationData civData,
        Scanner scanerPlanets, Science scienceCiv, Industry industryCiv)
    {
        this.anotherCiv = anotherCivilization.Where(x => x != this as ICivilization).ToList();
        (this.CivData, this.ScanerPlanets, this.ScienceCiv, this.IndustryCiv)
        = (civData, scanerPlanets, scienceCiv, industryCiv);

        PositionCiv = transform.position;
    }

    public event Action<float> ExecuteOnTimeEvent;
    public bool IsOpen { get; protected set; }
    public Vector2 PositionCiv { get; private set; }
    public CivilizationScriptable DataBase { get; private set; }
    public CivilizationData CivData { get; private set; }
    public Scanner ScanerPlanets { get; private set; }
    public Science ScienceCiv { get; private set; }
    public Industry IndustryCiv { get; private set; }

    public virtual void Assign(CivilizationScriptable dataBase)
    {
        this.DataBase = dataBase;

        // Инициализация данных
        CivData.Initialize(this.DataBase, civilizationUI);
        ScanerPlanets.Initialize(this);
        ScienceCiv.Initialize(this);
        IndustryCiv.Initialize(this);

        isAssign = true;
    }

    public void ExecuteOnTime(float deltaTime)
    {
        if (isAssign == false) return;

        ExecuteOnTimeEvent?.Invoke(deltaTime);
    }

    public abstract void ExicuteScanning();
    public abstract void ExicuteSciencePoints(int sciencePoints);
    public abstract void ExicuteIndustryPoints(float points);
}
