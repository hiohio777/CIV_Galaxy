public class GalacticEventGenerator
{
    protected float _progressInterval = 30; // Интервал
    private float _progress = 0; // Прогресс

    private ICivilization _civilization;

    public void Initialize(ICivilization civilization)
    {
        this._civilization = civilization;
        _civilization.ExecuteOnTimeEvent += Сivilization_ExecuteOnTimeEvent;

        _progressInterval = UnityEngine.Random.Range(5, 10);
    }

    private void Сivilization_ExecuteOnTimeEvent(float deltaTime)
    {
        _progress += deltaTime;

        if (_progress > _progressInterval)
        {
            _progress = 0;

            StartNewGalacticEvent();
        }
    }

    protected virtual void StartNewGalacticEvent()
    {
        SelectEvent().Execute();
        _progressInterval = GetInterval; // Задать время для срабатывания нового события
    }

    protected GalacticEvent SelectEvent()
    {
        int resultRandom = UnityEngine.Random.Range(0, 10);

        if (resultRandom == 0)
        {
            return new SciencePointBonus(_civilization);
        }
        else if (resultRandom <= 3)
        {
            return new IndustryBonus(_civilization);
        }
        else if (resultRandom <= 6)
        {
            return new ResearchBonus(_civilization);
        }

        return new DominationBonus(_civilization);
    }

    protected float GetInterval => UnityEngine.Random.Range(10, 20);
}
