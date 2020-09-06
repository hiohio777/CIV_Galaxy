using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MessageStartGame : MessageBase
{
    [SerializeField] private Image art;
    [SerializeField] private LocalisationText nameCiv, descriptionCiv;
    [SerializeField] private Button OK;

    private Action _actOK;
    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;

        OK.onClick.AddListener(OnOK);
    }

    public class Factory : PlaceholderFactory<MessageStartGame> { }

    public void Show(CivilizationScriptable civData, Action actOK)
    {
        this._actOK = actOK;
        nameCiv.SetKey(civData.Name);
        descriptionCiv.SetKey(civData.Description);
        art.sprite = civData.Icon;

        _galaxyUITimer.SetPause(true);
    }

    public void EndAnimation()
    {
        _actOK.Invoke();

        _galaxyUITimer.SetPause(false);
        Destroy(gameObject);
    }

    private void OnOK() => _animator.SetTrigger("CloseMessage");
}
