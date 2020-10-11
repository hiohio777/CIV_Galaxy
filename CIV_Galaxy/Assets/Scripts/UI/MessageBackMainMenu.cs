using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MessageBackMainMenu : MonoBehaviour
{
    [SerializeField] private Button yes, no;

    private Action _actYes, _actNo;
    private IGalaxyUITimer _galaxyUITimer;
    private bool _selectButton;
    private Animator _animator;

    public class Factory : PlaceholderFactory<MessageBackMainMenu> { }

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;

        _animator = GetComponent<Animator>();
        yes.onClick.AddListener(OnWelcome);
        no.onClick.AddListener(OnOffend); ;
    }

    public MessageBackMainMenu Show(Action actYes, Action actNo)
    {
        (this._actYes, this._actNo) = (actYes, actNo);

        _galaxyUITimer.SetPause(true, string.Empty);
        _animator.SetTrigger("DisplayMessage");

        return this;
    }

    public void EndAnimation()
    {
        if (_selectButton) _actYes?.Invoke();
        else _actNo?.Invoke();

        yes.interactable = no.interactable = true;

        _galaxyUITimer.SetPause(false);
        Destroy(gameObject);
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
