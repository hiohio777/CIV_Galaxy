using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : NoRegisterMonoBehaviour
{
    [SerializeField] private AudioClip musicEndGame, audioEffectVictiry, audioEffectDefeat;
    [SerializeField, Space(10)] private Image art;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField] private Button OK;
    [SerializeField] private LocalisationText textVictory;
    [SerializeField]
    private Text yearGame, countPlanet, countDominationPoints,
        countDiscoveries, countBombs, countSpaceFleet, countScientificMission;
    [SerializeField] private Image liderIcon;
    [SerializeField] private List<CivilizationEndGamelUI> _civilizationEndGamelUI;
    [SerializeField] private List<GameObject> _civilizationsPanelUI;

    private IGalaxyUITimer _galaxyUITimer;
    private GalaxySceneUI _galaxySceneUI;
    private ICivilizationPlayer _civPlayer;
    private List<ICivilizationAl> _civsAl;

    private Animator _animator;

    private void Creat()
    {
        _galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
        _galaxySceneUI = GetRegisterObject<GalaxySceneUI>();
        _civPlayer = GetRegisterObject<ICivilizationPlayer>();
        _civsAl = GetRegisterObjects<ICivilizationAl>();
    }

    public EndGameUI Show()
    {
        Creat();

        OK.onClick.AddListener(OnOK);
        OK.interactable = false;

        art.sprite = _civPlayer.DataBase.Icon;
        nameCiv.SetKey(_civPlayer.DataBase.Name);

        yearGame.text = $"{LocalisationGame.Instance.GetLocalisationString("year")}: {_galaxyUITimer.GetYears}";
        countDominationPoints.text = ((int)_civPlayer.CivData.DominationPoints).ToString("#,#");
        countPlanet.text = _civPlayer.CivData.Planets.ToString();
        countDiscoveries.text = _civPlayer.ScienceCiv.CountDiscoveries.ToString();
        countBombs.text = _civPlayer.AbilityCiv.Abilities[0].CountUses.ToString();
        countSpaceFleet.text = _civPlayer.AbilityCiv.Abilities[1].CountUses.ToString();
        countScientificMission.text = _civPlayer.AbilityCiv.Abilities[2].CountUses.ToString();

        switch (_civPlayer.Lider)
        {
            case LeaderEnum.Lagging:
                liderIcon.enabled = false;
                countDominationPoints.color = Color.red;
                break;
            case LeaderEnum.Leader:
                liderIcon.enabled = true;
                countDominationPoints.color = Color.yellow;
                liderIcon.color = new Color(1, 1, 0, 0.75f);
                break;
        }

        DefinitionOfVctory(CivilizationTable());
        _galaxyUITimer.SetPause(true, string.Empty);
        _animator = GetComponent<Animator>();
        PlayNewMusic(musicEndGame);
        _animator.SetTrigger("DisplayEndGameUI");
        return this;
    }

    private void DefinitionOfVctory(bool isVictory)
    {
        // Определение победы и сравнение статистики
        if (isVictory)
        {
            textVictory.Color = Color.green;
            textVictory.SetKey("victory");
            CreateBattleStatistics();
            PlaySoundEffect(audioEffectVictiry);
        }
        else
        {
            textVictory.Color = Color.red;
            textVictory.SetKey("defeat");
            PlaySoundEffect(audioEffectDefeat);
        }
    }

    private void CreateBattleStatistics()
    {
        // Статистика
        var stat = StatisticsPurchasePlayer.GetStatistics(_civPlayer.DataBase.Name, PlayerSettings.Instance.CurrentDifficult, PlayerSettings.Instance.CurrentOpponents);
        if (stat.IsRecorded)
        {
            if (stat.CountDomination < (int)_civPlayer.CivData.DominationPoints)
            {
                SeveStatistics(stat);
                Debug.Log("Рекорд обнавлён!");
            }
        }
        else
        {
            // Рекорда ещё нет
            SeveStatistics(stat);
            Debug.Log("Первый рекорд записан!");
        }
    }

    private void SeveStatistics(StatisticsData stat)
    {
        stat.Write((int)_galaxyUITimer.GetYears, (int)_civPlayer.CivData.DominationPoints, _civPlayer.CivData.Planets, _civPlayer.ScienceCiv.CountDiscoveries,
            _civPlayer.AbilityCiv.Abilities[0].CountUses, _civPlayer.AbilityCiv.Abilities[1].CountUses, _civPlayer.AbilityCiv.Abilities[2].CountUses);
        StatisticsPurchasePlayer.SaveDate();
    }

    private bool CivilizationTable()
    {
        // Распределить цивилизации в порядке колиества доминирования
        switch (PlayerSettings.Instance.CurrentOpponents)
        {
            case OpponentsEnum.Two:
                _civilizationsPanelUI[2].SetActive(false);
                _civilizationsPanelUI[3].SetActive(false);
                break;
            case OpponentsEnum.Four:
                _civilizationsPanelUI[3].SetActive(false);
                break;
        }

        List<ICivilization> _civs = new List<ICivilization>(_civsAl);
        _civs.Add(_civPlayer);

        _civs.Sort((x, y) =>
        {
            if (x.CivData.DominationPoints > y.CivData.DominationPoints) return -1;
            return 1;
        });

        for (int i = 0; i < _civilizationEndGamelUI.Count; i++)
        {
            if (i < _civs.Count)
                _civilizationEndGamelUI[i].Assign(_civs[i]);
            else _civilizationEndGamelUI[i].gameObject.SetActive(false);
        }

        return _civs[0] is ICivilizationPlayer;
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
