using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class CivilizationBase : MonoBehaviour, ICivilizationBase
{
    [SerializeField] protected CivilizationUI civilizationUI;
    protected bool isAssign = false;
    protected List<ICivilization> anotherCiv; // Другие цивилизации

    [Inject]
    public void Inject(List<ICivilization> anotherCivilization, CivilizationData civData,
        ScannerPlanets scanerPlanets, Science scienceCiv)
    {
        this.anotherCiv = anotherCivilization.Where(x => x != this as ICivilization).ToList();
        (this.CivData, this.ScanerPlanets, this.ScienceCiv) = (civData, scanerPlanets, scienceCiv);

        PositionCiv = transform.position;
    }

    public event Action<float> ExecuteOnTimeEvent;
    public bool IsOpen { get; protected set; }
    public Vector2 PositionCiv { get; private set; }
    public CivilizationScriptable CivDataBase { get; private set; }
    public CivilizationData CivData { get; private set; }
    public ScannerPlanets ScanerPlanets { get; private set; }
    public Science ScienceCiv { get; private set; }

    public virtual void Assign(CivilizationScriptable civData)
    {
        this.CivDataBase = civData;

        // Инициализация данных
        CivData.Initialize(civData, civilizationUI);
        ScanerPlanets.Initialize(this);
        ScienceCiv.Initialize(this);

        isAssign = true;
    }

    public void ExecuteOnTime(float deltaTime)
    {
        if (isAssign == false) return;

        ExecuteOnTimeEvent?.Invoke(deltaTime);
    }

    public abstract void ExicuteScanning();
    public abstract void ExicuteSciencePoints(int sciencePoints);
}
