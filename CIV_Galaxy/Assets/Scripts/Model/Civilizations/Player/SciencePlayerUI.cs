using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SciencePlayerUI : RegisterMonoBehaviour
{
    [SerializeField] private Image artCivPlayer;
    [SerializeField] private Text textCountPoints;
    [SerializeField] private DiscoveryCellUI discoveryCellUIPrefab;

    private bool _isInit = false; // Инизиализировано ли древо наук(его UI)
    private bool _isPause = false; // Бфла ли активирована пауза игры

    private ICivilizationPlayer _civPlayer;
    private IGalaxyUITimer _galaxyUITimer;
    private List<DiscoveryCellUI> _discoveries = new List<DiscoveryCellUI>();
    private MessageInfoScience _messageInfoScience;

    public void Start()
    {
        _civPlayer = GetRegisterObject<ICivilizationPlayer>();
        _galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();

        _messageInfoScience = GetComponentInChildren<MessageInfoScience>(true);
        _messageInfoScience.UpdateCostDiscoveriesEvent += UpdateCostDiscoveries;

        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);

        if (_galaxyUITimer.IsPause == false)
        {
            _galaxyUITimer.SetPause(true);
            _isPause = true;
        }

        if (_isInit == false) InitiateUIScience();

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
        artCivPlayer.sprite = _civPlayer.DataBase.Icon;

        foreach (var item in _civPlayer.ScienceCiv.TreeOfScienceCiv.Discoveries)
            _discoveries.Add(Instantiate(discoveryCellUIPrefab).Creat(item, _messageInfoScience, transform));

        _discoveries.ForEach(x => x.Initialize()); // Инициализировать

        _messageInfoScience.transform.SetAsLastSibling();
        _isInit = true;
    }

    private void UpdateCostDiscoveries()
    {
        foreach (var item in _discoveries)
            item.CheckPrice(_civPlayer.ScienceCiv.Points);

        textCountPoints.text = _civPlayer.ScienceCiv.Points.ToString();
    }
}
