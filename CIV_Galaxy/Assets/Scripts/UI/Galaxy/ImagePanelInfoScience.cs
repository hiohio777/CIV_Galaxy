using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ImagePanelInfoScience : MonoBehaviour
{
    [SerializeField] private Image imageIcon;
    [SerializeField] private Text nameDiscovery, infoDiscovery;
    [SerializeField] private Button buttonStudy, buttonClose;

    private Animator _animator;
    private Science _scienceCiv;
    private DiscoveryCell _discoveryCell;
    private ICivilizationPlayer _civPlayer;

    [Inject]
    public void Inject(ICivilizationPlayer civPlayer)
    {
        this._civPlayer = civPlayer;
        this._scienceCiv = civPlayer.ScienceCiv;

        buttonStudy.onClick.AddListener(OnStudy);
        buttonClose.onClick.AddListener(OnClose);

        _animator = GetComponent<Animator>();
    }

    public void Show(DiscoveryCell discoveryCell)
    {
        this._discoveryCell = discoveryCell;

        imageIcon.sprite = discoveryCell.SpriteIcon;
        nameDiscovery.text = discoveryCell.name;
        infoDiscovery.text = discoveryCell.Description;

        if (_scienceCiv.SciencePoints >= discoveryCell.ResearchCost)
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
        buttonStudy.interactable = buttonClose.interactable = true;
        gameObject.SetActive(false);
    }

    private void OnStudy()
    {
        _animator.SetTrigger("CloseMessage");
        buttonStudy.interactable = buttonClose.interactable = false;

        _discoveryCell.Study(_civPlayer as ICivilizationBase);

        _scienceCiv.ExicuteSciencePointsPlayer(_discoveryCell);
    }

    private void OnClose()
    {
        _animator.SetTrigger("CloseMessage");
        buttonStudy.interactable = buttonClose.interactable = false;
    }
}
