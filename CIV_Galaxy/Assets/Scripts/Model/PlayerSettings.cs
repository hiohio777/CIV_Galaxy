using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings
{
    private DifficultEnum _currentDifficult;

    public PlayerSettings()
    {
        _currentDifficult = DifficultEnum.Easy;
        GetDifficultSettings = Resources.Load<DifficultSettingsScriptable>($"Difficult/{_currentDifficult}");
    }

    public OpponentsEnum CurrentOpponents { get; set; } = OpponentsEnum.Two;
    public DifficultEnum CurrentDifficult {
        get => _currentDifficult; set {
            _currentDifficult = value;
            GetDifficultSettings = Resources.Load<DifficultSettingsScriptable>($"Difficult/{_currentDifficult}");
        }
    }
    public string CurrentCivilization { get; set; } = "Humanity";

    public DifficultSettingsScriptable GetDifficultSettings { get; private set; }
}

public enum DifficultEnum { Easy, Medium, Difficult }
public enum OpponentsEnum { Two, Four, Six }
