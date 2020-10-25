using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageStartGame : MonoBehaviour
{
    [SerializeField] private Image art;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField] private Button OK;

    private Action _actOK;
    private IGalaxyUITimer _galaxyUITimer;
    private Animator _animator;
    private AudioSourceGame _audioSourceGame;

    public MessageStartGame Show(IGalaxyUITimer galaxyUITimer, CivilizationScriptable civData, Action actOK)
    {
        this._galaxyUITimer = galaxyUITimer;

        _animator = GetComponent<Animator>();
        OK.onClick.AddListener(OnOK);

        this._actOK = actOK;
        nameCiv.SetKey(civData.Name);
        art.sprite = civData.Icon;

        _galaxyUITimer.SetPause(true, string.Empty);
        _animator.SetTrigger("DisplayMessage");

        _audioSourceGame = GetComponent<AudioSourceGame>();
        _audioSourceGame.PlayOneShot(0, 0.5f);
        return this;
    }

    public void EndAnimation()
    {
        _actOK?.Invoke();

        OK.interactable = true;

        _galaxyUITimer.SetPause(false);
        Destroy(gameObject);
    }

    private void OnOK()
    {
        OK.interactable = false;

        _audioSourceGame.PlayOneShot(0, 0.5f);
        _animator.SetTrigger("CloseMessage");
    }
}
