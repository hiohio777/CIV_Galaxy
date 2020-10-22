using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageBackMainMenu : MonoBehaviour
{
    [SerializeField] private Button yes, no;

    private Action _actYes, _actNo;
    private IGalaxyUITimer _galaxyUITimer;
    private bool _selectButton;
    private Animator _animator;

    public MessageBackMainMenu Show(IGalaxyUITimer galaxyUITimer, Action actYes, Action actNo)
    {
        this._galaxyUITimer = galaxyUITimer;

        _animator = GetComponent<Animator>();
        yes.onClick.AddListener(OnWelcome);
        no.onClick.AddListener(OnOffend);

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
