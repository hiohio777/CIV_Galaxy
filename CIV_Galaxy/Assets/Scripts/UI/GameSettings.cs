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

    [SerializeField, Space(10)] private GameObject panelRecord;
    [SerializeField] private Image art;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField]
    private Text yearGame, countPlanet, countDominationPoints,
        countDiscoveries, countBombs, countSpaceFleet, countScientificMission;

    private PlayerSettings _playerSettings;
    private PlayerSettings _playerData;
    private Civilizations _civilizations;
    private Statistics _statistics;
    private CivilizationScriptable _civPlayer;

    [Inject]
    public void Inject(PlayerSettings playerSettings, PlayerSettings playerData, Civilizations civilizations, Statistics statistics)
    {
        this._playerSettings = playerSettings;
        this._playerData = playerData;
        this._civilizations = civilizations;
        this._statistics = statistics;

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
        DisplayRecord(_civPlayer);
    }

    public void AssignDifficult(int indexButton)
    {
        if (_playerSettings.CurrentDifficult == (DifficultEnum)indexButton) return;

        RemoveSelection(buttonsDifficult[(int)_playerSettings.CurrentDifficult]);
        _playerSettings.CurrentDifficult = (DifficultEnum)indexButton;
        Select(buttonsDifficult[indexButton]);

        SetDifficultFon(_playerSettings.CurrentDifficult);
        DisplayRecord(_civPlayer);
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

    public override void Enable()
    {
        base.Enable();

        _civilizations.Refresh();
        _civPlayer = _civilizations.GetCivilizationPlayer(_playerSettings.CurrentCivilization, false);
        art.sprite = _civPlayer.Icon;
        nameCiv.SetKey(_civPlayer.Name);

        DisplayRecord(_civPlayer);
    }

    private void DisplayRecord(CivilizationScriptable civPlayer)
    {
        var stat = _statistics.GetStatistics(civPlayer.Name, _playerSettings.CurrentDifficult, _playerSettings.CurrentOpponents);
        if (stat.IsRecorded)
        {
            // Есть рекорд
            panelRecord.SetActive(true);
            yearGame.text = $"{LocalisationGame.Instance.GetLocalisationString("year")}:  {stat.Years}";
            countDominationPoints.text = stat.CountDomination.ToString("#,#");
            countPlanet.text = stat.CountPlanets.ToString();
            countDiscoveries.text = stat.CountDiscoveries.ToString();
            countBombs.text = stat.CountBombs.ToString();
            countSpaceFleet.text = stat.CountSpaceFleet.ToString();
            countScientificMission.text = stat.CountScientificMission.ToString();
        }
        else
            panelRecord.SetActive(false);
    }

    private void Select(Button button)
    {
        button.image.sprite = buttonSelected;
        button.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1);
    }
    private void RemoveSelection(Button button)
    {
        button.image.sprite = buttonDontSelected;
        button.GetComponentInChildren<Text>().color = new Color(0.8f, 0.8f, 0.8f, 1); ;
    }


}
