using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SciencePlayerUI : MonoBehaviour
{
    [SerializeField] private Image artCivPlayer;

    private bool _isInit = false; // Инизиализировано ли древо наук(его UI)
    private bool _isPause = false; // Бфла ли активирована пауза игры

    private ICivilizationPlayer _civPlayer;
    private IGalaxyUITimer _galaxyUITimer;
    private DiscoveryCellUI.Factory _factoryDiscoveryCellUI;
    private List<DiscoveryCellUI> _discoveries = new List<DiscoveryCellUI>();
    private MessageInfoScience _messageInfoScience;

    [Inject]
    public void Inject(ICivilizationPlayer civPlayer, IGalaxyUITimer galaxyUITimer, DiscoveryCellUI.Factory factoryDiscoveryCellUI)
    {
        (this._civPlayer, this._galaxyUITimer, this._factoryDiscoveryCellUI)
        = (civPlayer, galaxyUITimer, factoryDiscoveryCellUI);

        _messageInfoScience = GetComponentInChildren<MessageInfoScience>();
        _messageInfoScience.UpdateCostDiscoveriesEvent += UpdateCostDiscoveries;
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

        foreach (var item in _discoveries)
            item.CheckPrice(_civPlayer.ScienceCiv.Points);
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
        {
            var discoveryCellUI = _factoryDiscoveryCellUI.Create(item);
            discoveryCellUI.transform.SetParent(transform, false);
            discoveryCellUI.MessageInfoScience = _messageInfoScience;

            _discoveries.Add(discoveryCellUI);
        }

        _messageInfoScience.transform.SetAsLastSibling();
        _isInit = true;
    }

    private void UpdateCostDiscoveries()
    {
        foreach (var item in _discoveries)
            item.CheckPrice(_civPlayer.ScienceCiv.Points);
    }
}
