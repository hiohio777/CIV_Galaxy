using System;

public class DiplomaticRelations
{
    public float Danger { get; private set; }
    private float _progressInterval = 0; // Интервал(Изменить отношение)
    private float _progress = 0; // Прогресс

    private int _relations = 0;
    private DiplomaticRelationsEnum _relationsType;
    private ICivilizationAl _civilization;

    public ICivilization AnotherCiv { get; private set; }
    public DiplomaticRelationsEnum Relations {
        get => _relationsType; private set {
            _relationsType = value;
            if (AnotherCiv is ICivilizationPlayer)
                _civilization.SetSetDiplomaticRelations(_relationsType);
        }
    }

    public DiplomaticRelations(ICivilization civ)
    {
        AnotherCiv = civ ?? throw new ArgumentNullException(nameof(civ));
    }

    public DiplomaticRelations Initialize(ICivilizationAl civilization)
    {
        this._civilization = civilization;
        _civilization.ExecuteOnTimeEvent += ExecuteOnTimeEvent;

        SetProgressInterval();
        ChangeRelations(UnityEngine.Random.Range(0, 4));

        return this;
    }

    public DiplomaticRelationsEnum ChangeRelations(int count = 0)
    {
        _relations += count;
        if (_relations < 0) _relations = 0;
        if (_relations > 4) _relations = 4;

        return Relations = (DiplomaticRelationsEnum)_relations;
    }

    public void CalculateDanger()
    {
        if (AnotherCiv.IsOpen == false)
        {
            Danger = -1;
            return;
        }

        Danger = 0;
        if (_civilization.CivData.DominationPoints < AnotherCiv.CivData.DominationPoints)
            Danger += UnityEngine.Random.Range(10f, 40f);
        if (_civilization.CivData.Planets < AnotherCiv.CivData.Planets)
            Danger += UnityEngine.Random.Range(10f, 40f);

        switch (Relations)
        {
            case DiplomaticRelationsEnum.Hatred: Danger += UnityEngine.Random.Range(60f, 100f); break;
            case DiplomaticRelationsEnum.Threat: Danger += UnityEngine.Random.Range(40f, 100f); break;
            case DiplomaticRelationsEnum.Neutrality: Danger += UnityEngine.Random.Range(-20f, 100f); break;
            case DiplomaticRelationsEnum.Cooperation: Danger += UnityEngine.Random.Range(-80f, 80f); break;
            case DiplomaticRelationsEnum.Friendship: Danger += UnityEngine.Random.Range(-100f, 50f); break;
            default: Danger = -100f; break;
        }
    }

    private void ExecuteOnTimeEvent(float deltaTime)
    {
        if (_civilization.IsOpen && AnotherCiv.IsOpen)
            _progress += deltaTime;

        if (_progress > _progressInterval)
        {
            _progress = 0;
            SetProgressInterval();

            // Определение отношений
            ChangeRelations(UnityEngine.Random.Range(-1, +2));
        }
    }

    private void SetProgressInterval() => _progressInterval = UnityEngine.Random.Range(20, 31);
}