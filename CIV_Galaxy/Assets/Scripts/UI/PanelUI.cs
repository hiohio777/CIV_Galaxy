using System;
using UnityEngine;

public class PanelUI : NoRegisterMonoBehaviour
{
    public static Action<string> startNewPanelUI;
    public static Action finishDisableUI;

    protected GameObject _gameObject;
    protected Animator animator;

    public PanelUI Initialize()
    {
        animator = GetComponent<Animator>();
        _gameObject = gameObject;
        _gameObject.SetActive(false);

        return this;
    }

    public virtual void Enable()
    {
        _gameObject.SetActive(true);
        StartAnimation("Enable");
    }

    public virtual void Disable()
    {
        if (StartAnimation("Disable") == false)
            DisableFinish();
    }

    public virtual void DisableFinish()
    {
        _gameObject.SetActive(false);
        finishDisableUI.Invoke();
    }

    public void StartNewPanelUI(string namePanel)
    {
        startNewPanelUI.Invoke(namePanel);
    }

    private bool StartAnimation(string trigger)
    {
        if (animator != null)
        {
            animator.SetTrigger(trigger);
            return true;
        }

        // Нет анимации закрытия интерфейса
        return false;
    }
}
