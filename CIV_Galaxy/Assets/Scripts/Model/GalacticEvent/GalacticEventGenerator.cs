public class GalacticEventGenerator : CivilizationStructureBase
{
    protected float _progressInterval = 6; // Интервал
    private float _progress = 0; // Прогресс

    private ICivilization _civilization;

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
    }

    protected override void ExecuteOnTimeEvent(float deltaTime)
    {
        _progress += deltaTime;

        if (_progress >= _progressInterval)
        {
            _progress -= _progressInterval;

            SelectEvent().Execute();
            _progressInterval = GetInterval; // Задать время для срабатывания нового события
            StartNewGalacticEvent();
        }
    }

    protected virtual void StartNewGalacticEvent()
    {
    }

    protected GalacticEvent SelectEvent()
    {
        int resultRandom = UnityEngine.Random.Range(0, 10);

        if (resultRandom > 0)
        {
            resultRandom = UnityEngine.Random.Range(0, 4);
            switch (resultRandom)
            {
                case 0: return new IndustryBonus(_civilization);
                case 1: return new ResearchBonus(_civilization);
                case 2: return new ProgressAbiliryBonus(_civilization);
                default: return new DominationBonus(_civilization);
            }
        }

        return new SciencePointBonus(_civilization);
    }

    protected float GetInterval => UnityEngine.Random.Range(10, 20);
}
