using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MessageStartGame : MonoBehaviour
{
    [SerializeField] private Image art;
    [SerializeField] private LocalisationText nameCiv, descriptionCiv;
    [SerializeField] private Button OK;

    private Action _actOK;
    private IGalaxyUITimer _galaxyUITimer;
    private Animator _animator;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;

        _animator = GetComponent<Animator>();
        OK.onClick.AddListener(OnOK);
    }

    public void Show(CivilizationScriptable civData, Action actOK)
    {
        this._actOK = actOK;
        nameCiv.SetKey(civData.Name);
        descriptionCiv.SetKey(civData.Description);
        art.sprite = civData.Icon;

        gameObject.SetActive(true);
        _galaxyUITimer.SetPause(true);
        _animator.SetTrigger("DisplayMessage");
    }

    public void EndAnimation()
    {
        _actOK.Invoke();

        OK.interactable = true;

        _galaxyUITimer.SetPause(false);
        Destroy(gameObject);
    }

    private void OnOK()
    {
        OK.interactable = false;

        _animator.SetTrigger("CloseMessage");
    }
}
