using System;
using UnityEngine;

public class DiplomaticRelations
{
    public int Danger { get; private set; }
    private float _progressInterval = 0; // Интервал(Изменить отношение)
    private float _progress = 0; // Прогресс

    private int _relations = 0;
    private DiplomaticRelationsEnum _relationsType;
    private ICivilizationAl _civilization;
    private int _dangerPlayer; // Настройка сложности(агрессивность Al по отношению к игроку)

    public ICivilization AnotherCiv { get; private set; }
    public DiplomaticRelationsEnum Relation {
        get => _relationsType; private set {
            _relationsType = value;
            if (AnotherCiv is ICivilizationPlayer)
                _civilization.SetSetDiplomaticRelations(_relationsType);
        }
    }

    public DiplomaticRelations(ICivilizationAl civilization, ICivilization anotherCiv, int dangerPlayer, IGalaxyUITimer galaxyUITimer)
    {
        this._civilization = civilization;
        AnotherCiv = anotherCiv ?? throw new ArgumentNullException(nameof(anotherCiv));
        this._dangerPlayer = dangerPlayer;

        galaxyUITimer.ExecuteOfTime += ExecuteOnTimeEvent;
        SetProgressInterval();
        ChangeRelations(UnityEngine.Random.Range(0, 4));
    }

    public void CalculateDanger(AttackerAbility ability)
    {
        if (ability is SpaceFleet)
        {
            if (AnotherCiv.IsOpen == false || Relation == DiplomaticRelationsEnum.Friendship || Relation == DiplomaticRelationsEnum.Cooperation)
            {
                Danger = -1000; // Не атакует флотом друзей и тех кто в сотрудничестве
                return;
            }
        }

        if (ability is Bombs)
            if (AnotherCiv.IsOpen == false || Relation == DiplomaticRelationsEnum.Friendship)
            {
                Danger = -1000; // Не атакует бомбами друзей
                return;
            }


        Danger = UnityEngine.Random.Range((int)Relation * 5, 100);

        if (_civilization.CivData.DominationPoints < AnotherCiv.CivData.DominationPoints)
            Danger += UnityEngine.Random.Range(0, 20);
        if (_civilization.CivData.Planets < AnotherCiv.CivData.Planets)
            Danger += UnityEngine.Random.Range(0, 20);
        if (AnotherCiv.IsLider == LeaderEnum.Leader)
            Danger += 30;
        if (AnotherCiv is ICivilizationPlayer)
            Danger += UnityEngine.Random.Range(_dangerPlayer / 2, _dangerPlayer);
    }

    public DiplomaticRelationsEnum ChangeRelations(int count = 0)
    {
        _relations += count;
        if (_relations < 0) _relations = 0;
        if (_relations > 4) _relations = 4;

        return Relation = (DiplomaticRelationsEnum)_relations;
    }

    private void SetProgressInterval() => _progressInterval = UnityEngine.Random.Range(20, 31);

    private void ExecuteOnTimeEvent(float deltaTime)
    {
        if (_civilization.IsOpen && AnotherCiv.IsOpen)
            _progress += deltaTime;

        if (_progress > _progressInterval)
        {
            _progress = 0;
            SetProgressInterval();

            // Определение отношений
            // Вероятность ухудшить отношения к лидеру
            if (AnotherCiv.IsLider == LeaderEnum.Leader)
            {
                if (UnityEngine.Random.Range(0, 3) > 0) ChangeRelations(+1);
                else ChangeRelations(-1);
            }
            else ChangeRelations(UnityEngine.Random.Range(-1, +2));
        }
    }
}

public enum DiplomaticRelationsEnum { Friendship = 0, Cooperation = 1, Neutrality = 2, Threat = 3, Hatred = 4, }
