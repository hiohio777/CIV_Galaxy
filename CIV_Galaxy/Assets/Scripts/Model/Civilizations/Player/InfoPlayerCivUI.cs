using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InfoPlayerCivUI : MonoBehaviour
{
    [SerializeField] private Image artCivPlayer;
    [SerializeField] private Text infoScaner, infoIndustry, infoScience;
    [SerializeField] private Text infoAbility, infoBombs, infoSpaceFleet, infoScientificMission;
    [SerializeField] private Text сountDomination, countPlanets, infoDomination;

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

        // Базовая
        infoIndustry.text = _civPlayer.IndustryCiv.GetInfo();
        infoScience.text = _civPlayer.ScienceCiv.GetInfo();
        infoScaner.text = _civPlayer.ScanerCiv.GetInfo();
        // Скиллы
        infoAbility.text = _civPlayer.AbilityCiv.GetInfo();
        infoBombs.text = _civPlayer.AbilityCiv.Abilities[0].GetInfo();
        infoSpaceFleet.text = _civPlayer.AbilityCiv.Abilities[1].GetInfo(); 
        infoScientificMission.text = _civPlayer.AbilityCiv.Abilities[2].GetInfo();
        // Доминирование
        сountDomination.text = ((int)_civPlayer.CivData.DominationPoints).ToString();
        countPlanets.text = _civPlayer.CivData.Planets.ToString();
        infoDomination.text = _civPlayer.CivData.GetInfo();
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