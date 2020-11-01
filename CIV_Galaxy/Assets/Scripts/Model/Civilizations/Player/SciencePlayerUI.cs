using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SciencePlayerUI : NoRegisterMonoBehaviour
{
    [SerializeField] private Image artCivPlayer;
    [SerializeField] private Text textCountPoints;
    [SerializeField] private DiscoveryCellUI discoveryCellUIPrefab;

    private bool _isPause = false; // Бфла ли активирована пауза игры
    private bool isActive = false; // Был ли обект активирован

    private ICivilizationPlayer _civPlayer;
    private IGalaxyUITimer _galaxyUITimer;
    private List<DiscoveryCellUI> _discoveries = new List<DiscoveryCellUI>();
    private MessageInfoScience _messageInfoScience;

    public void Start() => gameObject.SetActive(isActive);

    public void Enable()
    {
        if (isActive == false) InitiateUIScience();

        if (_galaxyUITimer.IsPause == false)
        {
            _galaxyUITimer.SetPause(true);
            _isPause = true;
        }

        gameObject.SetActive(true);
        UpdateCostDiscoveries();
    }

    public void Disable()
    {
        if (_isPause)
        {
            _isPause = false;
            _galaxyUITimer.SetPause(false);
        }

        gameObject.SetActive(false);
    }

    // Настроить отображение древа наук и его UI
    private void InitiateUIScience()
    {
        _civPlayer = GetRegisterObject<ICivilizationPlayer>();
        _galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();

        _messageInfoScience = GetComponentInChildren<MessageInfoScience>(true);
        _messageInfoScience.UpdateCostDiscoveriesEvent += UpdateCostDiscoveries;

        artCivPlayer.sprite = _civPlayer.DataBase.Icon;

        foreach (var item in _civPlayer.ScienceCiv.TreeOfScienceCiv.Discoveries)
            _discoveries.Add(Instantiate(discoveryCellUIPrefab).Creat(item, _messageInfoScience, transform));

        _discoveries.ForEach(x => x.Initialize()); // Инициализировать

        _messageInfoScience.transform.SetAsLastSibling();
        isActive = true;
    }

    private void UpdateCostDiscoveries()
    {
        foreach (var item in _discoveries)
            item.CheckPrice(_civPlayer.ScienceCiv.Points);

        textCountPoints.text = _civPlayer.ScienceCiv.Points.ToString();
    }
}
