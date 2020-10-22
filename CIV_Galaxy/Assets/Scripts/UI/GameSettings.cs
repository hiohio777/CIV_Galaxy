using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private CivilizationScriptable _civPlayer;

    public void Start()
    {
        PlayerSettings.Instance.CurrentDifficult = DifficultEnum.Easy;

        Select(buttonsOpponents[(int)PlayerSettings.Instance.CurrentOpponents]);
        Select(buttonsDifficult[(int)PlayerSettings.Instance.CurrentDifficult]);
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
        if (PlayerSettings.Instance.CurrentOpponents == (OpponentsEnum)indexButton) return;

        RemoveSelection(buttonsOpponents[(int)PlayerSettings.Instance.CurrentOpponents]);
        PlayerSettings.Instance.CurrentOpponents = (OpponentsEnum)indexButton;
        Select(buttonsOpponents[indexButton]);

        float size = indexButton * 0.2f + 1f;
        imageArtGalaxy.StartResize(new Vector3(size, size, size), 0.5f);
        DisplayRecord(_civPlayer);
    }

    public void AssignDifficult(int indexButton)
    {
        if (PlayerSettings.Instance.CurrentDifficult == (DifficultEnum)indexButton) return;

        RemoveSelection(buttonsDifficult[(int)PlayerSettings.Instance.CurrentDifficult]);
        PlayerSettings.Instance.CurrentDifficult = (DifficultEnum)indexButton;
        Select(buttonsDifficult[indexButton]);

        SetDifficultFon(PlayerSettings.Instance.CurrentDifficult);
        DisplayRecord(_civPlayer);
    }

    public void StartGalaxyScene()
    {
        Action act = () =>
        {
            switch (PlayerSettings.Instance.CurrentOpponents)
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

        Civilizations.Instance.Refresh();
        _civPlayer = Civilizations.Instance.GetCivilizationPlayer(PlayerSettings.Instance.CurrentCivilization, false);
        art.sprite = _civPlayer.Icon;
        nameCiv.SetKey(_civPlayer.Name);

        DisplayRecord(_civPlayer);
    }

    private void DisplayRecord(CivilizationScriptable civPlayer)
    {
        var stat = Statistics.Instance.GetStatistics(civPlayer.Name, PlayerSettings.Instance.CurrentDifficult, PlayerSettings.Instance.CurrentOpponents);
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
