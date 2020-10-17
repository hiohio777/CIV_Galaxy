using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpecialEffect : MonoBehaviour
{
    private SpriteRenderer art;
    private Animator animator;
    private Action<object> _buffered;
    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(Action<object> buffered, IGalaxyUITimer galaxyUITimer)
    {
        this._buffered = buffered;
        _galaxyUITimer = galaxyUITimer;
        _galaxyUITimer.PauseAct += _galaxyUITimer_PauseAct;
        _galaxyUITimer.SpeedAct += _galaxyUITimer_SpeedAct;

        animator = GetComponent<Animator>();
        art = GetComponent<SpriteRenderer>();
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

    public class Factory : PlaceholderFactory<Action<object>, SpecialEffect> { }

    public void Initialize(Vector3 position, Sprite spriteEffect)
    {
        gameObject.SetActive(true);

        _galaxyUITimer_PauseAct(_galaxyUITimer.IsPause);
        transform.position = position;
        art.sprite = spriteEffect;

        animator.SetTrigger("DisplayEffect");
    }


    public void Destroy()
    {
        _buffered.Invoke(this);
        gameObject.SetActive(false);
    }
}
