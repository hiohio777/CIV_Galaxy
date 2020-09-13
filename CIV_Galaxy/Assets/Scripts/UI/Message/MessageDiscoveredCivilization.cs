using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MessageDiscoveredCivilization : MessageBase
{
    [SerializeField] private Image artDiscoveredCivilization;
    [SerializeField] private LocalisationText nameCiv, descriptionCiv;
    [SerializeField] private Button welcome, offend;

    private Action _actWelcome, _actOffend;
    private IGalaxyUITimer _galaxyUITimer;
    private bool _selectButton;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;

        welcome.onClick.AddListener(OnWelcome);
        offend.onClick.AddListener(OnOffend); 
    }

    public void Show(CivilizationScriptable civData, Action actWelcome, Action actOffend)
    {
        (this._actWelcome, this._actOffend) = (actWelcome, actOffend);
        nameCiv.SetKey(civData.Name);
        descriptionCiv.SetKey(civData.Description);
        artDiscoveredCivilization.sprite = civData.Icon;

        gameObject.SetActive(true);
        _galaxyUITimer.SetPause(true);
        _animator.SetTrigger("DisplayMessage");
    }

    public void EndAnimation()
    {
        if (_selectButton) _actWelcome.Invoke();
        else _actOffend.Invoke();

        welcome.interactable = offend.interactable = true;

        _galaxyUITimer.SetPause(false);
        gameObject.SetActive(false);
    }

    private void OnWelcome()
    {
        _animator.SetTrigger("CloseMessage");
        _selectButton = true;

        welcome.interactable = offend.interactable = false;
    }

    private void OnOffend()
    {
        _animator.SetTrigger("CloseMessage");
        _selectButton = false;

        welcome.interactable = offend.interactable = false;
    }
}

