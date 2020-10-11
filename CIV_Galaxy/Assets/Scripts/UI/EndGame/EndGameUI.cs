using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private Image art;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField] private Button OK;
    [SerializeField] private Text yearGame, countPlanet, countDominationPoints, 
        countDiscoveries, countBombs, countSpaceFleet, countScientificMission;
    [SerializeField] private Image dominatorIcon;
    [SerializeField] private List<CivilizationEndGamelUI> _civilizationEndGamelUI;

    private IGalaxyUITimer _galaxyUITimer;
    private GalaxySceneUI _galaxySceneUI;
    private ICivilizationPlayer _civPlayer;
    private List<ICivilizationAl> _civsAl;

    private Animator _animator;
    public class Factory : PlaceholderFactory<EndGameUI> { }

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer, GalaxySceneUI galaxySceneUI, ICivilizationPlayer civPlayer,
        List<ICivilizationAl> civsAl)
    {
        (this._galaxyUITimer, this._galaxySceneUI, this._civPlayer, this._civsAl)
        = (galaxyUITimer, galaxySceneUI, civPlayer, civsAl);
    }

    public EndGameUI Show()
    {
        _galaxyUITimer.SetPause(true);
        OK.onClick.AddListener(OnOK);

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
                dominatorIcon.enabled = false;
                countDominationPoints.color = new Color(0.6f, 0.6f, 0, 1);
                break;
            case LeaderEnum.Leader:
                dominatorIcon.enabled = true;
                countDominationPoints.color = new Color(1, 1, 0, 1);
                dominatorIcon.color = new Color(1, 1, 0, 0.75f);
                break;
        }

        // Распределить цивилизации в порядке колиества доминирования
        List<ICivilization> _civs = new List<ICivilization>(_civsAl);
        _civs.Add(_civPlayer);
        _civs.Sort(CompareTo);
        for (int i = 0; i < _civilizationEndGamelUI.Count; i++)
        {
            if (i < _civs.Count)
                _civilizationEndGamelUI[i].Assign(_civs[i]);
            else _civilizationEndGamelUI[i].gameObject.SetActive(false);
        }

        _animator = GetComponent<Animator>();
        _animator.SetTrigger("DisplayEndGameUI");

        return this;
    }

    public void EndAnimation()
    {
        OK.interactable = true;
    }

    private void OnOK()
    {
        _galaxySceneUI.BackMainScene();
    }

    private int CompareTo(ICivilization x, ICivilization y)
    {
        if (x.CivData.DominationPoints > y.CivData.DominationPoints)
            return -1;
        return 1;
    }
}
