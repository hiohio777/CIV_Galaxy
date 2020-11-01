using UnityEngine;
using UnityEngine.UI;

public class InfoCivilizationPanelUI : NoRegisterMonoBehaviour
{
    [SerializeField] private Image artCiv;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField] private Text infoScaner, infoIndustry, infoScience;
    [SerializeField] private Text infoAbility, infoBombs, infoSpaceFleet, infoScientificMission;
    [SerializeField] private Text сountDomination, countPlanets, infoDomination;

    private bool isActive = false; // Был ли обект активирован
    private bool _isPause = false; // Была ли активирована пауза игры

    private IGalaxyUITimer _galaxyUITimer;

    public void Start() => gameObject.SetActive(isActive);

    public void Show(ICivilization civilization)
    {
        if (isActive == false)
        {
            _galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
            isActive = true;
        }

        if (_galaxyUITimer.IsPause == false)
        {
            _galaxyUITimer.SetPause(true);
            _isPause = true;
        }

        gameObject.SetActive(true);
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