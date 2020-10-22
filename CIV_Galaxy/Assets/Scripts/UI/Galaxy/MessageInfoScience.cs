using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageInfoScience : RegisterMonoBehaviour
{
    [SerializeField] private Image imageIcon;
    [SerializeField] private LocalisationText nameDiscovery;
    [SerializeField] private Text cost, infoDiscovery;
    [SerializeField] private Button buttonStudy, buttonClose;

    private Animator _animator;
    private DiscoveryCell _discoveryCell;
    private ICivilizationPlayer _civPlayer;
    private bool isStudy = false;

    public void Start()
    {
        this._civPlayer = GetRegisterObject<ICivilizationPlayer>();

        buttonStudy.onClick.AddListener(OnStudy);
        buttonClose.onClick.AddListener(OnClose);

        _animator = GetComponent<Animator>();
    }


    public event Action UpdateCostDiscoveriesEvent;

    public void Show(DiscoveryCell discoveryCell)
    {
        this._discoveryCell = discoveryCell;

        imageIcon.sprite = discoveryCell.SpriteIcon;
        nameDiscovery.SetKey(discoveryCell.name);
        cost.text = discoveryCell.ResearchCost.ToString();

        infoDiscovery.text = string.Empty;
        // Вывод информации о бонусах
        foreach (var item in discoveryCell.Boneses)
            infoDiscovery.text += item.GetInfo();

        if (discoveryCell.IsResearch == false && _civPlayer.ScienceCiv.Points >= discoveryCell.ResearchCost)
        {
            buttonStudy.gameObject.SetActive(true);
        }
        else
        {
            buttonStudy.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
        _animator.SetTrigger("DisplayMessage");
    }

    public void EndAnimation()
    {
        if (isStudy)
        {
            _discoveryCell.Study(_civPlayer);
            _civPlayer.ScienceCiv.ExicuteSciencePointsPlayer(_discoveryCell.ResearchCost);
            UpdateCostDiscoveriesEvent.Invoke();
        }

        buttonStudy.interactable = buttonClose.interactable = true;
        gameObject.SetActive(false);
    }

    private void OnStudy()
    {
        _animator.SetTrigger("CloseMessage");
        buttonStudy.interactable = buttonClose.interactable = false;
        isStudy = true;
    }

    private void OnClose()
    {
        _animator.SetTrigger("CloseMessage");
        buttonStudy.interactable = buttonClose.interactable = false;
        isStudy = false;
    }
}
