﻿using System.Collections.Generic;
using UnityEngine;

public abstract class AttackerAbility : MonoBehaviour, IAbility
{
    [SerializeField] private Sprite spriteArtFon, spriteArt, frame;
    [SerializeField, Space(10)] protected float speedUnits = 5f;
    [SerializeField, Range(1,10)] private int countUnits = 1;
    [SerializeField, Header("Величина при появлении")] private float size = 1.5f;

    private UnitAbilityFactory _unitFactory;

    public int CountUnits { get => countUnits; set
    {
           countUnits = value;
            if (countUnits <= 0) countUnits = 1;
         }
     } // Количество запускаемых юнитов за раз

    protected ICivilization ThisCivilization { get; private set; }

    public string Name => name;
    public bool IsActive { get; set; } = true; // Активен ли(доступна ли способность)
    public Sprite Fon => spriteArtFon;
    public Sprite Art => spriteArt;
    public Sprite Frame => frame;

    public void Initialize(ICivilization civilization)
    {
        (this.ThisCivilization) = (civilization);
    }

    public void Initialize(UnitAbilityFactory unitFactory)
    {
        this._unitFactory = unitFactory;
    }

    public virtual bool ApplyAl(Diplomacy diplomacyCiv)
    {
        var target = diplomacyCiv.FindEnemy();
        if (target != null)
        {
            StartAttack(target); // Цель найдена
            return true;
        }
        else return false;
    }

    public virtual void SelectedApplayPlayer(List<ICivilizationAl> civilizationsTarget)
    {
        foreach (var item in civilizationsTarget)
        {
            if (item.IsOpen == false) continue;

            item.EnableFrame(Color.red);
        }
    }

    public virtual bool Apply(ICivilization civilizationTarget)
    {
        StartAttack(civilizationTarget);
        return true;
    }

    // Отправить флот в атаку
    protected void StartAttack(ICivilization civilizationTarget)
    {
        ThisCivilization.AbilityCiv.ReduceProgress();

        for (int i = 0; i < countUnits; i++)
        {
            var unit = _unitFactory.GetNewUnit(this, ThisCivilization, civilizationTarget);
            unit.TtransformUnit.up = new Vector2(0, 0) - ThisCivilization.PositionCiv;

            unit.SetWaitForSeconds(i / 2f).SetScale(size, 0.2f).Run(() => StartAct(unit, civilizationTarget));
        }
    }

    private void StartAct(IUnitAbility unit, ICivilization civilizationTarget)
        => unit.SetScale(1f, 0.2f).Run(() => MoveAct(unit, civilizationTarget));
    private void MoveAct(IUnitAbility unit, ICivilization civilizationTarget) 
        => unit.SetPositionBezier(civilizationTarget.PositionCiv, new Vector3(0,0), new Vector3(0, 0), speedUnits).Run(() => EndAttack(unit, civilizationTarget));
    private void EndAttack(IUnitAbility unit, ICivilization civilizationTarget)
        => unit.SetScale(0f, 0.2f).Run(() => Finish(unit, civilizationTarget));

    private void Finish(IUnitAbility unit, ICivilization civilizationTarget)
    {
        Finall(unit, civilizationTarget);
        // Ухудшить отношения, если напал игрок, а отношения были "дружба"
        if (ThisCivilization is ICivilizationPlayer)
            (civilizationTarget as ICivilizationAl).DiplomacyCiv.ChangeRelations(ThisCivilization, +1);

        unit.Destroy();
    }

    protected abstract void Finall(IUnitAbility unit, ICivilization civilizationTarget);
}