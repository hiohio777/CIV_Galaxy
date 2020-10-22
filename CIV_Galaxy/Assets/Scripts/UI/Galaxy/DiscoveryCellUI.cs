using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiscoveryCellUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text researchCost;
    [SerializeField] private Color colorResearch, colorAvailable, colorAvailablePrice;
    private Image imageDiscovery;
    private DiscoveryCell _discoveryCellData;
    private MessageInfoScience _messageInfoScience;

    public DiscoveryCellUI Creat(DiscoveryCell discoveryCell, MessageInfoScience messageInfoScience, Transform transformParent)
    {
        this._discoveryCellData = discoveryCell;
        _discoveryCellData.AvailableUI += DiscoveryCell_AvailableUI;
        _discoveryCellData.ResearchUI += DiscoveryCell_ResearchUI;

        _messageInfoScience = messageInfoScience;
        transform.transform.SetParent(transformParent, false);
        name = _discoveryCellData.name;

        return this;
    }

    public void Initialize()
    {
        transform.localPosition = _discoveryCellData.transform.localPosition;
        researchCost.text = _discoveryCellData.ResearchCost.ToString();
        imageDiscovery = GetComponent<Image>();
        imageDiscovery.sprite = _discoveryCellData.SpriteIcon;

        DiscoveryCell_AvailableUI(_discoveryCellData.IsAvailable);
        if (_discoveryCellData.IsResearch)
            DiscoveryCell_ResearchUI();
    }

    public void CheckPrice(int sciencePoints)
    {
        if (_discoveryCellData.IsAvailable == false || _discoveryCellData.IsResearch)
            return;

        if (sciencePoints >= _discoveryCellData.ResearchCost)
        {
            imageDiscovery.color = colorAvailablePrice;
            researchCost.color = colorAvailablePrice;
            return;
        }

        imageDiscovery.color = colorAvailable;
        researchCost.color = colorAvailable;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Посмотреть информацию и выбрать для изучения
        _messageInfoScience.Show(_discoveryCellData);
    }

    private void DiscoveryCell_AvailableUI(bool isAvailable)
    {
        // imageDiscovery.color = colorAvailable;
        gameObject.SetActive(isAvailable);
    }

    private void DiscoveryCell_ResearchUI()
    {
        imageDiscovery.color = colorResearch;
        researchCost.gameObject.SetActive(false);
    }
}