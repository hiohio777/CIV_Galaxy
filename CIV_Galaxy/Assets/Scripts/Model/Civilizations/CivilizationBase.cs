using UnityEngine;
using Zenject;

public abstract class CivilizationBase : MonoBehaviour, ICivilization
{
    [SerializeField] protected CivilizationUI civilizationUI;
    protected bool isAssign = false; // Назначена ли цивилизация
    private InfoCivilizationPanelUI _infoCivilizationPanelUI;

    [Inject]
    public void Inject(CivilizationData civData, Scanner scanerPlanets, Science scienceCiv, Industry industryCiv,
        Ability abilityCiv, ValueChangeEffectFactory valueChangeEffectFactory, InfoCivilizationPanelUI infoCivilizationPanelUI)
    {
        (this.CivData, this.ScanerCiv, this.ScienceCiv, this.IndustryCiv, this.AbilityCiv, this._infoCivilizationPanelUI)
        = (civData, scanerPlanets, scienceCiv, industryCiv, abilityCiv, infoCivilizationPanelUI);

        PositionCiv = transform.position;

        civilizationUI.valueChangeEffectFactory = valueChangeEffectFactory;
    }

    public CivilizationUI CivUI => civilizationUI;
    public bool IsOpen { get; protected set; }
    public LeaderEnum IsLider { get; protected set; } = LeaderEnum.Lagging;
    public Vector2 PositionCiv { get; private set; }
    public CivilizationScriptable DataBase { get; private set; }
    public CivilizationData CivData { get; private set; }
    public Scanner ScanerCiv { get; private set; }
    public Industry IndustryCiv { get; private set; }
    public Science ScienceCiv { get; private set; }
    public Ability AbilityCiv { get; private set; }
    public GalacticEventGenerator EventGenerator { get; protected set; }

    public virtual void Assign(CivilizationScriptable dataBase)
    {
        this.DataBase = dataBase;

        // Инициализация данных
        CivData.Initialize(this);
        ScanerCiv.Initialize(this);
        ScienceCiv.Initialize(this);
        IndustryCiv.Initialize(this);

        isAssign = true;
    }

    public virtual void OnClick() => _infoCivilizationPanelUI.Show(this);

    public void ExicuteIndustryPoints(float points)
    {
        civilizationUI.SetIndustryPoints(points);
    }

    public void DefineLeader(LeaderEnum leaderEnum)
    {
        IsLider = leaderEnum;
        civilizationUI.SetAdvancedDomination(leaderEnum);
    }

    public abstract void ExicuteScanning();
    public abstract void ExicuteSciencePoints(int sciencePoints);
}
