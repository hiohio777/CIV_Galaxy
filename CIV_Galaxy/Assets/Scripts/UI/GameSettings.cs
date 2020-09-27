using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameSettings : PanelUI
{
    [SerializeField] private List<Button> buttonsOpponents, buttonsDifficult;
    private PlayerSettings _playerSettings;

    [Inject]
    public void Inject(PlayerSettings playerSettings)
    {
        this._playerSettings = playerSettings;

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
    }

    private void Select(Button button)
    {
        button.image.color = Color.green;
    }

    private void RemoveSelection(Button button)
    {
        button.image.color = new Color(0.85f, 0.85f, 0.85f, 1);
    }
}
