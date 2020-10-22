using UnityEngine;
using UnityEngine.UI;

public class InfoCivilizationPanelUI : RegisterMonoBehaviour
{
    [SerializeField] private Image artCiv;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField] private Text infoScaner, infoIndustry, infoScience;
    [SerializeField] private Text infoAbility, infoBombs, infoSpaceFleet, infoScientificMission;
    [SerializeField] private Text сountDomination, countPlanets, infoDomination;

    private bool _isPause = false; // Была ли активирована пауза игры

    private IGalaxyUITimer _galaxyUITimer;

    public void Start()
    {
        this._galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
        gameObject.SetActive(false);
    }

    public void Show(ICivilization civilization)
    {
        gameObject.SetActive(true);

        if (_galaxyUITimer.IsPause == false)
        {
            _galaxyUITimer.SetPause(true);
            _isPause = true;
        }

        nameCiv.SetKey(civilization.DataBase.Name);
        artCiv.sprite = civilization.DataBase.Icon;

        // Базовая
        infoIndustry.text = civilization.IndustryCiv.GetInfo();
        infoScience.text = civilization.ScienceCiv.GetInfo();
        infoScaner.text = civilization.ScanerCiv.GetInfo();
        // Скиллы
        infoAbility.text = civilization.AbilityCiv.GetInfo();
        infoBombs.text = civilization.AbilityCiv.Abilities[0].GetInfo();
        infoSpaceFleet.text = civilization.AbilityCiv.Abilities[1].GetInfo();
        infoScientificMission.text = civilization.AbilityCiv.Abilities[2].GetInfo();
        // Доминирование
        сountDomination.text = ((int)civilization.CivData.DominationPoints).ToString();
        countPlanets.text = civilization.CivData.Planets.ToString();
        infoDomination.text = civilization.CivData.GetInfo();
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