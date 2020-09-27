using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ImagePanelInfoScience : MonoBehaviour
{
    [SerializeField] private Image imageIcon;
    [SerializeField] private Text nameDiscovery, infoDiscovery;
    [SerializeField] private Button buttonStudy, buttonClose;

    private Animator _animator;
    private DiscoveryCell _discoveryCell;
    private ICivilizationPlayer _civPlayer;
    private bool isStudy = false;

    [Inject]
    public void Inject(ICivilizationPlayer civPlayer)
    {
        this._civPlayer = civPlayer;

        buttonStudy.onClick.AddListener(OnStudy);
        buttonClose.onClick.AddListener(OnClose);

        _animator = GetComponent<Animator>();
    }


    public event Action UpdateCostDiscoveriesEvent;

    public void Show(DiscoveryCell discoveryCell)
    {
        this._discoveryCell = discoveryCell;

        imageIcon.sprite = discoveryCell.SpriteIcon;
        nameDiscovery.text = discoveryCell.name;
        infoDiscovery.text = discoveryCell.Description;

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
