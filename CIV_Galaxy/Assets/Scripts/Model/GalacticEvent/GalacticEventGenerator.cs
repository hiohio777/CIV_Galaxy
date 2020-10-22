public class GalacticEventGenerator : CivilizationStructureBase
{
    protected float _progressInterval = 6; // Интервал
    protected GalaxyTypeEventEnum _typeEvent;

    private float _progress = 0; // Прогресс
    private float _bonusEfficiency = 1;
    private ICivilization _civilization;

    public GalacticEventGenerator(IGalaxyUITimer galaxyUITimer) : base(galaxyUITimer) { }

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
    }

    public void AddBonusEfficiency(float count)
    {
        _bonusEfficiency += count / 100;
        if (_bonusEfficiency < 0) _bonusEfficiency = 0;
    }

    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        _progress += deltaTime;

        if (_progress >= _progressInterval)
        {
            _progress = 0;

            _progressInterval = UnityEngine.Random.Range(10, 20);
            _typeEvent = (GalaxyTypeEventEnum)UnityEngine.Random.Range(0, 5);

            StartNewGalacticEvent();
        }
    }

    protected virtual void StartNewGalacticEvent() => Execute();

    protected void Execute()
    {
        switch (_typeEvent)
        {
            case GalaxyTypeEventEnum.IndustryBonus: _civilization.IndustryCiv.AddPoints(15f * _bonusEfficiency); break;
            case GalaxyTypeEventEnum.ResearchBonus: _civilization.ScienceCiv.AddProgress(15f * _bonusEfficiency); break;
            case GalaxyTypeEventEnum.ProgressAbiliryBonus: _civilization.AbilityCiv.AddProgress(30f * _bonusEfficiency); break;
            case GalaxyTypeEventEnum.ProgressScanerBonus: _civilization.ScanerCiv.AddProgress(30f * _bonusEfficiency); break;
            case GalaxyTypeEventEnum.DominationBonus: _civilization.CivData.AddDominance(_civilization.CivData.Planets * 1.5f * _bonusEfficiency); break;
            default: _civilization.CivData.AddDominance(_civilization.CivData.Planets * 1.5f * _bonusEfficiency); break;
        }
    }
}

public enum GalaxyTypeEventEnum { IndustryBonus, ResearchBonus, ProgressAbiliryBonus, ProgressScanerBonus, DominationBonus, SciencePointBonus }
