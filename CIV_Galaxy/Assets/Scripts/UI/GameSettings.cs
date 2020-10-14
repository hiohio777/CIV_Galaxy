using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameSettings : PanelUI
{
    [SerializeField] private List<Button> buttonsOpponents, buttonsDifficult;
    [SerializeField] private Sprite buttonSelected, buttonDontSelected;
    [SerializeField] private JustRotateArtGalaxy imageArtGalaxy;
    [SerializeField] private Image galaxyFon;
    [SerializeField] private Sprite[] galaxyFonSprite;
    private PlayerSettings _playerSettings;
    private PlayerSettings _playerData;

    [Inject]
    public void Inject(PlayerSettings playerSettings, PlayerSettings playerData)
    {
        this._playerSettings = playerSettings;
        this._playerData = playerData;

        _playerSettings.CurrentDifficult = DifficultEnum.Easy;

        Select(buttonsOpponents[(int)_playerSettings.CurrentOpponents]);
        Select(buttonsDifficult[(int)_playerSettings.CurrentDifficult]);
    }

    public override void Disable()
    {
        base.Disable();
        AssignDifficult(0); 
        AssignOpponents(0);
    }

    public void SetDifficultFon(DifficultEnum difficult) => galaxyFon.sprite = galaxyFonSprite[(int)difficult];

    public void AssignOpponents(int indexButton)
    {
        if (_playerSettings.CurrentOpponents == (OpponentsEnum)indexButton) return;

        RemoveSelection(buttonsOpponents[(int)_playerSettings.CurrentOpponents]);
        _playerSettings.CurrentOpponents = (OpponentsEnum)indexButton;
        Select(buttonsOpponents[indexButton]);

        float size = indexButton * 0.2f + 1f;
        imageArtGalaxy.StartResize(new Vector3(size, size, size), 0.5f);
    }

    public void AssignDifficult(int indexButton)
    {
        if (_playerSettings.CurrentDifficult == (DifficultEnum)indexButton) return;

        RemoveSelection(buttonsDifficult[(int)_playerSettings.CurrentDifficult]);
        _playerSettings.CurrentDifficult = (DifficultEnum)indexButton;
        Select(buttonsDifficult[indexButton]);

        SetDifficultFon(_playerSettings.CurrentDifficult);
    }

    public void StartGalaxyScene()
    {
        Action act = () =>
        {
            switch (_playerData.CurrentOpponents)
            {
                case OpponentsEnum.Two:
                    SceneManager.LoadScene("GalaxyScene_2"); break;
                case OpponentsEnum.Four:
                    SceneManager.LoadScene("GalaxyScene_4"); break;
                case OpponentsEnum.Six:
                    SceneManager.LoadScene("GalaxyScene_6"); break;
            }
        };

        imageArtGalaxy.StartResize(new Vector3(0, 0, 0), 1, act);
    }

    public void AnimationCloseScen()
    {
        animator.SetTrigger("StartBattel");
    }

    private void Select(Button button)
    {
        button.image.sprite = buttonSelected;
        button.GetComponentInChildren<Text>().color = new Color(1,1,1,1);
    }
    private void RemoveSelection(Button button)
    {
        button.image.sprite = buttonDontSelected;
        button.GetComponentInChildren<Text>().color = new Color(0.8f, 0.8f, 0.8f, 1); ;
    }
}
