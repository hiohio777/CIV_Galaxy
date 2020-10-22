using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUI : RegisterMonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image fon, artIndicator, frame;

    private ICivilizationPlayer _civilizationPlayer;
    private Ability _abilityCiv;
    private IGalaxyUITimer _galaxyUITimer;

    public void Start()
    {
        this._civilizationPlayer = GetRegisterObject<ICivilizationPlayer>();
        this._galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();

        artIndicator.fillAmount = 0;
        frame.enabled = true;
    }

    public IAbility Ability { get; private set; }

    public void Assing(IAbility ability, Ability abilityCiv)
    {
        this.Ability = ability;
        this._abilityCiv = abilityCiv;

        abilityCiv.ProgressEvent += SetProgress;

        frame.sprite = Ability.Frame;
        fon.sprite = Ability.Fon;
        artIndicator.sprite = Ability.Art;

        SetReady();
        gameObject.SetActive(ability.IsActive);
    }

    public bool Apply(ICivilization civilizationTarget)
    {
        if (Ability.Apply(civilizationTarget))
        {
            Select(false);
            _galaxyUITimer.SetPause(false);
            return true;
        }

        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Ability.IsActive && _abilityCiv.IsReady)
        {
            if (_civilizationPlayer.SelectedAbility == this)
            {
                _civilizationPlayer.SelectedAbility = null;
                Select(false);
                _galaxyUITimer.SetPause(false);
            }
            else
            {
                _galaxyUITimer.SetPause(true, "choose_target");
                _civilizationPlayer.SelectedAbility?.Select(false);
                Select(true);
            }
        }
    }

    public void Select(bool isSelect)
    {
        if (isSelect)
        {
            frame.enabled = true;
            frame.color = new Color(0, 1, 0, 1);
            _civilizationPlayer.SelectedAbility = this;
        }
        else
        {
            SetReady();
        }
    }


    public void SetReady()
    {
        if (_abilityCiv.IsReady)
        {
            frame.enabled = true;
            frame.color = new Color(0, 0.7f, 1, 1);
        }
        else
        {
            frame.enabled = false;
        }
    }

    public void SetProgress(float progress)
    {
        artIndicator.fillAmount = progress / 100;
        SetReady();
    }
}
