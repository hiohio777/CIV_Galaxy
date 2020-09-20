using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SciencePlayerUI : MonoBehaviour
{
    [SerializeField] private Image artCivPlayer;

    private bool _isInit = false; // Инизиализировано ли древо наук(его UI)

    private ICivilizationPlayer _civPlayer;
    private IGalaxyUITimer _galaxyUITimer;
    private DiscoveryCellUI.Factory _factoryDiscoveryCellUI;
    private Science _scienceCiv;
    private List<DiscoveryCellUI> _discoveries = new List<DiscoveryCellUI>();

    [Inject]
    public void Inject(ICivilizationPlayer civPlayer, IGalaxyUITimer galaxyUITimer, DiscoveryCellUI.Factory factoryDiscoveryCellUI,
        ImagePanelInfoScience imagePanelInfoScience)
    {
        (this._civPlayer, this._scienceCiv, this._galaxyUITimer, this._factoryDiscoveryCellUI)
        = (civPlayer, civPlayer.ScienceCiv, galaxyUITimer, factoryDiscoveryCellUI);

        imagePanelInfoScience.UpdateCostDiscoveriesEvent += UpdateCostDiscoveries;
    }

    public void Enable()
    {
        gameObject.SetActive(true);

        _galaxyUITimer.SetPause(true);
        if (_isInit == false) InitiateUIScience();

        foreach (var item in _discoveries)
            item.CheckPrice(_scienceCiv.Points);
    }

    public void Disable()
    {
        _galaxyUITimer.SetPause(false);

        gameObject.SetActive(false);
    }

    // Настроить отображение древа наук и его UI
    private void InitiateUIScience()
    {
        artCivPlayer.sprite = _civPlayer.DataBase.Icon;

        foreach (var item in _scienceCiv.TreeOfScienceCiv.Discoveries)
        {
            var discoveryCellUI = _factoryDiscoveryCellUI.Create(item);
            discoveryCellUI.transform.SetParent(transform, false);

            _discoveries.Add(discoveryCellUI);
        }

        _isInit = true;
    }

    private void UpdateCostDiscoveries()
    {
        foreach (var item in _discoveries)
            item.CheckPrice(_civPlayer.ScienceCiv.Points);
    }
}
