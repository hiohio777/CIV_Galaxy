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
    private AudioSourceGame _audioSourceGame;

    public MessageBackMainMenu Show(IGalaxyUITimer galaxyUITimer, Action actYes, Action actNo)
    {
        this._galaxyUITimer = galaxyUITimer;

        _animator = GetComponent<Animator>();
        yes.onClick.AddListener(Yes);
        no.onClick.AddListener(No);

        (this._actYes, this._actNo) = (actYes, actNo);

        _galaxyUITimer.SetPause(true, string.Empty);
        _animator.SetTrigger("DisplayMessage");

        _audioSourceGame = GetComponent<AudioSourceGame>();
        _audioSourceGame.PlayOneShot(0, 0.5f);
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

    private void Yes()
    {
        _animator.SetTrigger("CloseMessage");
        _audioSourceGame.PlayOneShot(0, 0.5f);
        _selectButton = true;

        yes.interactable = no.interactable = false;
    }

    private void No()
    {
        _animator.SetTrigger("CloseMessage");
        _audioSourceGame.PlayOneShot(0, 0.5f);
        _selectButton = false;

        yes.interactable = no.interactable = false;
    }
}
