using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private Image art;
    [SerializeField] private LocalisationText nameCiv, descriptionCiv;
    [SerializeField] private Button OK;

    private IGalaxyUITimer _galaxyUITimer;
    private GalaxySceneUI _galaxySceneUI;

    private Animator _animator;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer, GalaxySceneUI galaxySceneUI)
    {
        this._galaxyUITimer = galaxyUITimer;
        this._galaxySceneUI = galaxySceneUI;
    }

    public void Show()
    {
        gameObject.SetActive(true);

        OK.onClick.AddListener(OnOK);
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("DisplayMessage");
    }

    public void EndAnimation()
    {
        OK.interactable = true;
    }

    private void OnOK()
    {
        _galaxySceneUI.BackMainScene();
    }
}
