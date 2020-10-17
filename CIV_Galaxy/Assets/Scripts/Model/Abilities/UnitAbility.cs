using System;
using UnityEngine;
using Zenject;

public class UnitAbility : UnitBase, IUnitAbility
{
    [SerializeField] private SpriteRenderer frame, art;
    [SerializeField] private TrailRenderer trail;

    private IGalaxyUITimer _galaxyUITimer;
    private float currentTimeTrail;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        _galaxyUITimer = galaxyUITimer;
        _galaxyUITimer.PauseAct += _galaxyUITimer_PauseAct;
        _galaxyUITimer.SpeedAct += _galaxyUITimer_SpeedAct;
    }

    private void _galaxyUITimer_PauseAct(bool isPause)
    {
        if (isPause) trail.time = 1000;
        else trail.time = currentTimeTrail / _galaxyUITimer.GetSpeed;
    }

    private void _galaxyUITimer_SpeedAct(float speed)
    {
        if (_galaxyUITimer.IsPause == false)
            trail.time = currentTimeTrail / speed;
    }

    public class Factory : PlaceholderFactory<Action<object>, UnitAbility> { }

    public ICivilization TargetCiv { get; private set; }// Целевая цивилизация
    public UnitAbility Initialize(AttackerAbility ability, Vector3 startPosition, ICivilization targetCiv, TypeDisplayAbilityEnum type)
    {
        gameObject.SetActive(true);

        transform.position = startPosition;
        transform.localScale = new Vector3(0, 0, 0);
        this.TargetCiv = targetCiv;

        if (ability.TimeTrail <= 0)
        {
            trail.gameObject.SetActive(false);
        }
        else
        {
            trail.gameObject.SetActive(true);
            trail.time = (currentTimeTrail = ability.TimeTrail) / _galaxyUITimer.GetSpeed;
        }

        frame.sprite = ability.Frame;
        art.sprite = ability.Art;
        switch (type)
        {
            case TypeDisplayAbilityEnum.PlayerTarget:
                InitTypeDisplay(new Color(1, 0, 0, 0.8f));
                break;
            case TypeDisplayAbilityEnum.PlayerAttack:
                InitTypeDisplay(new Color(0, 1, 0, 0.8f));
                break;
            case TypeDisplayAbilityEnum.Al:
                frame.enabled = false;
                art.color = new Color(0.6f, 0.6f, 0.6f, 1);
                art.sortingOrder = GetSortingOrder - 20;
                break;
        }

        return this;
    }

    private void InitTypeDisplay(Color color)
    {
        frame.enabled = true;
        trail.sortingOrder = GetSortingOrder;
        frame.sortingOrder = GetSortingOrder;
        art.sortingOrder = GetSortingOrder;
        art.color = Color.white;

        frame.color = color;
    }

    public override void Destroy()
    {
        trail.time = 0;
        trail.gameObject.SetActive(false);
    }
}
