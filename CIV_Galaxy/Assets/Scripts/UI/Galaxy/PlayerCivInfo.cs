using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerCivInfo : MonoBehaviour
{
    [SerializeField] private Image imageIconCiv;
    [SerializeField] private Button buttonClose;

    // Доминирование
    [SerializeField, Space(10)] private Text countDomination;
    [SerializeField] private Text countFromPlanet, countFromIndustry, countFromBonus, countAll;
    // Планеты
    [SerializeField, Space(10)] private Text countPlanets;


    private Animator _animator;
    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;

        buttonClose.onClick.AddListener(OnClose);
        _animator = GetComponent<Animator>();
    }

    public event Action UpdateCostDiscoveriesEvent;

    public void Show(ICivilization civ)
    {
        imageIconCiv.sprite = civ.DataBase.Icon;

        // Доминирование
        countDomination.text = ((int)civ.CivData.DominationPoints).ToString();

        countFromPlanet.text = $"<color=#ffff00ff>+{Math.Round(civ.CivData.GetPointsFromPlanets, 1)}</color> <color=#008000ff>rate: {Math.Round(civ.DataBase.Base.GrowthDominancePlanets + civ.CivData.GrowthDominancePlanetsBonus, 1)}</color>";
        countFromIndustry.text = $"<color=#ffff00ff>+{Math.Round(civ.CivData.GetPointsFromIndustry, 1)}</color> <color=#008000ff>rate: {Math.Round(civ.DataBase.Base.GrowthDominanceIndustry + civ.CivData.GrowthDominanceIndustryBonus, 1)}</color>";
        countFromBonus.text = $"<color=#ffff00ff>+{Math.Round(civ.CivData.GetPointsFromBonus, 1)}</color> <color=#008000ff>rate: {Math.Round(civ.DataBase.Base.GrowthDominanceOverall + civ.CivData.GrowthDominanceOverallBonus, 1)}</color>";
        countAll.text = $"<color=#ffff00ff>+{Math.Round(civ.CivData.GetPointsAll)}</color>";

        // Планеты


        gameObject.SetActive(true);
        _galaxyUITimer.SetPause(true);
        _animator.SetTrigger("DisplayMessage");
    }

    public void EndAnimation()
    {
        _galaxyUITimer.SetPause(false);
        buttonClose.interactable = true;
        gameObject.SetActive(false);
    }

    private void OnClose()
    {
        _animator.SetTrigger("CloseMessage");
        buttonClose.interactable = false;
    }
}