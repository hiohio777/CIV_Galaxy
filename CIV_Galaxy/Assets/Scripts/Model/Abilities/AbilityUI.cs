using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class AbilityUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image fon, artIndicator, frame;

    private ICivilizationPlayer _civilizationPlayer;
    private IAbility _ability;
    private Ability _abilityCiv;
    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(ICivilizationPlayer civilizationPlayer, IGalaxyUITimer galaxyUITimer)
    {
        this._civilizationPlayer = civilizationPlayer;
        this._galaxyUITimer = galaxyUITimer;
    }

    public IAbility Getability => _ability;

    public void Assing(IAbility ability, Ability abilityCiv)
    {
        this._ability = ability;
        this._abilityCiv = abilityCiv;

        abilityCiv.ProgressEvent += SetProgress;

        frame.sprite = _ability.Frame;
        fon.sprite = _ability.Fon;
        artIndicator.sprite = _ability.Art;

        SetReady();
        gameObject.SetActive(ability.IsActive);
    }

    public bool Apply(ICivilization civilizationTarget)
    {
        if (_ability.Apply(civilizationTarget))
        {
            Select(false);
            _galaxyUITimer.SetPause(false);
            return true;
        }

        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_ability.IsActive && _abilityCiv.IsReady)
        {
            if (_civilizationPlayer.SelectedAbility == this)
            {
                _civilizationPlayer.SelectedAbility = null;
                Select(false);
                _galaxyUITimer.SetPause(false);
            }
            else
            {
                _galaxyUITimer.SetPause(true);
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
