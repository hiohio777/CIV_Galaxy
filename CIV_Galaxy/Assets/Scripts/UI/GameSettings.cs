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
    [SerializeField] private JustRotate imageArtGalaxy;
    private PlayerSettings _playerSettings;
    private CanvasFonGalaxy _canvasFonGalaxy;
    private PlayerSettings _playerData;

    [Inject]
    public void Inject(PlayerSettings playerSettings, CanvasFonGalaxy canvasFonGalaxy, PlayerSettings playerData)
    {
        this._playerSettings = playerSettings;
        this._canvasFonGalaxy = canvasFonGalaxy;
        this._playerData = playerData; 

        Select(buttonsOpponents[(int)_playerSettings.CurrentOpponents]);
        Select(buttonsDifficult[(int)_playerSettings.CurrentDifficult]);
    }

    public void AssignOpponents(int indexButton)
    {
        if (_playerSettings.CurrentOpponents == (OpponentsEnum)indexButton) return;

        RemoveSelection(buttonsOpponents[(int)_playerSettings.CurrentOpponents]);
        _playerSettings.CurrentOpponents = (OpponentsEnum)indexButton;
        Select(buttonsOpponents[indexButton]);
    }

    public void AssignDifficult(int indexButton)
    {
        if (_playerSettings.CurrentDifficult == (DifficultEnum)indexButton) return;

        RemoveSelection(buttonsDifficult[(int)_playerSettings.CurrentDifficult]);
        _playerSettings.CurrentDifficult = (DifficultEnum)indexButton;
        Select(buttonsDifficult[indexButton]);

        _canvasFonGalaxy.SetDifficultFon(_playerSettings.CurrentDifficult);
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

        imageArtGalaxy.StartResize(act, new Vector3(0, 0, 0));
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
