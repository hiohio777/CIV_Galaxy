using UnityEngine;

public abstract class CivilizationBase : RegisterMonoBehaviour, ICivilization
{
    [SerializeField] protected CivilizationUI civilizationUI;
    protected bool isAssign = false; // Назначена ли цивилизация
    private InfoCivilizationPanelUI _infoCivilizationPanelUI;
    private ShakeObject _shakeObject;
    protected MovingObject _moving;
    private SpecialEffectFactory _specialEffectFactory;

    public virtual void Start()
    {
        _infoCivilizationPanelUI = GetRegisterObject<InfoCivilizationPanelUI>();
        var timerUI = GetRegisterObject<IGalaxyUITimer>();

        CivData = new CivilizationData(timerUI, GetRegisterObject<LeaderQualifier>());
        ScanerCiv = new Scanner(timerUI, GetRegisterObject<GalaxyData>(), GetRegisterObject<PlanetsFactory>());
        ScienceCiv = new Science(timerUI);
        IndustryCiv = new Industry(timerUI);
        AbilityCiv = new Ability(timerUI, GetRegisterObject<AbilityFactory>());

        PositionCiv = transform.position;
        _shakeObject = GetComponentInChildren<ShakeObject>();
        _moving = GetComponentInChildren<MovingObject>();

        _specialEffectFactory = GetRegisterObject<SpecialEffectFactory>();
        civilizationUI.valueChangeEffectFactory = GetRegisterObject<ValueChangeEffectFactory>();
    }

    public Transform GetTransform => transform;
    public CivilizationUI CivUI => civilizationUI;
    public bool IsOpen { get; protected set; }
    public LeaderEnum Lider { get; protected set; } = LeaderEnum.Lagging;
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

    public void ExicuteSpecialEffect(Sprite spriteEffect, EffectEnum effectEnum = EffectEnum.SpecialEffect_0)
    {
        if (IsOpen) _specialEffectFactory.GetEffect(PositionCiv, spriteEffect, effectEnum);
    }

    /// <summary>
    ///  Эффект встряски
    /// </summary>
    public void Shake(float duration, float power) => _shakeObject.Shake(duration, power);

    public void DefineLeader(LeaderEnum leaderEnum)
    {
        Lider = leaderEnum;
        civilizationUI.SetAdvancedDomination(leaderEnum);
    }

    public abstract void ExicuteScanning();
    public abstract void ExicuteSciencePoints(int sciencePoints);
}
