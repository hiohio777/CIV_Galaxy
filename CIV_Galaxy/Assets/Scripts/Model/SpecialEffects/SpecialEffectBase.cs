using System;
using UnityEngine;
using Zenject;

public abstract class SpecialEffectBase : MonoBehaviour
{
    private Animator animator;
    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        _galaxyUITimer = galaxyUITimer;
        _galaxyUITimer.PauseAct += _galaxyUITimer_PauseAct;
        _galaxyUITimer.SpeedAct += _galaxyUITimer_SpeedAct;

        animator = GetComponent<Animator>();
    }

    public void Destroy()
    {
        _galaxyUITimer.PauseAct -= _galaxyUITimer_PauseAct;
        _galaxyUITimer.SpeedAct -= _galaxyUITimer_SpeedAct;
        Destroy(gameObject);
    }

    protected void InitializeBase(Vector3 position, string nameTriggerAnimation)
    {
        _galaxyUITimer_PauseAct(_galaxyUITimer.IsPause);
        transform.position = position;

        animator.SetTrigger(nameTriggerAnimation);
    }

    private void _galaxyUITimer_SpeedAct(float speed)
    {
        animator.speed = speed;
    }

    private void _galaxyUITimer_PauseAct(bool isPause)
    {
        if (isPause == true)
            animator.speed = 0;
        else animator.speed = _galaxyUITimer.GetSpeed;
    }
}