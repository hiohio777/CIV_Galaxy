using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MessageBackMainMenu : MessageBase
{
    [SerializeField] private Image art;
    [SerializeField] private Button yes, no;

    private Action _actYes, _actNo;
    private IGalaxyUITimer _galaxyUITimer;
    private bool _selectButton;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;

        yes.onClick.AddListener(OnWelcome);
        no.onClick.AddListener(OnOffend); ;
    }

    public void Show(CivilizationScriptable civData, Action actYes, Action actNo)
    {
        (this._actYes, this._actNo) = (actYes, actNo);
        art.sprite = civData.Icon;

        gameObject.SetActive(true);
        _galaxyUITimer.SetPause(true);
        _animator.SetTrigger("DisplayMessage");
    }

    public void EndAnimation()
    {
        if (_selectButton) _actYes.Invoke();
        else _actNo.Invoke();

        yes.interactable = no.interactable = true;

        _galaxyUITimer.SetPause(false);
        gameObject.SetActive(false);
    }

    private void OnWelcome()
    {
        _animator.SetTrigger("CloseMessage");
        _selectButton = true;

        yes.interactable = no.interactable = false;
    }

    private void OnOffend()
    {
        _animator.SetTrigger("CloseMessage");
        _selectButton = false;

        yes.interactable = no.interactable = false;
    }
}
