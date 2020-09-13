using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class DiscoveryCellUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text researchCost;
    [SerializeField] private GameObject frameCost;
    [SerializeField] private Color colorResearch, colorAvailable;
    private Image imageDiscovery;
    private ImagePanelInfoScience _imagePanelInfoScience;
    private DiscoveryCell _discoveryCellData;

    public class Factory : PlaceholderFactory<DiscoveryCell, DiscoveryCellUI> { }

    [Inject]
    public void Inject(ImagePanelInfoScience imagePanelInfoScience, DiscoveryCell discoveryCell)
    {
        this._imagePanelInfoScience = imagePanelInfoScience;

        this._discoveryCellData = discoveryCell;
        _discoveryCellData.AvailableUI += DiscoveryCell_AvailableUI;
        _discoveryCellData.ResearchUI += DiscoveryCell_ResearchUI;

        name = _discoveryCellData.name;
    }

    public void CheckPrice(int sciencePoints)
    {
        if (_discoveryCellData.IsAvailable == false || _discoveryCellData.IsResearch)
            return;
   
        if (sciencePoints >= _discoveryCellData.ResearchCost) 
        {
            researchCost.color = Color.green; 
            return;
        }

        researchCost.color = Color.red;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Посмотреть информацию и выбрать для изучения
        _imagePanelInfoScience.Show(_discoveryCellData);
    }

    private void DiscoveryCell_AvailableUI(bool isAvailable)
    {
        imageDiscovery.color = colorAvailable;
        gameObject.SetActive(isAvailable);
    }

    private void DiscoveryCell_ResearchUI()
    {
        imageDiscovery.color = colorResearch;
        frameCost.gameObject.SetActive(false);
    }

    private void Awake()
    {
        transform.localPosition = _discoveryCellData.transform.localPosition;
        researchCost.text = _discoveryCellData.ResearchCost.ToString();
        imageDiscovery = GetComponent<Image>();
        imageDiscovery.sprite = _discoveryCellData.SpriteIcon;

        DiscoveryCell_AvailableUI(_discoveryCellData.IsAvailable);
        if (_discoveryCellData.IsResearch)
            DiscoveryCell_ResearchUI();
    }
}