using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InfoPlayerCivUI : MonoBehaviour
{
    [SerializeField] private Image artCivPlayer;
    [SerializeField] private Text infoIndustry;

    private bool _isPause = false; // Была ли активирована пауза игры

    private ICivilizationPlayer _civPlayer;
    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(ICivilizationPlayer civPlayer, IGalaxyUITimer galaxyUITimer)
    {
        (this._civPlayer, this._galaxyUITimer) = (civPlayer, galaxyUITimer);
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

        artCivPlayer.sprite = _civPlayer.DataBase.Icon;
        // Обновить информацию
        infoIndustry.text = _civPlayer.IndustryCiv.GetInfo();
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
}