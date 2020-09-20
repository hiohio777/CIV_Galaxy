using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class AbilityUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image indicatorFon, indicator, frame;

    private ICivilizationPlayer _civilizationPlayer;
    private IAbility _ability;

    [Inject]
    public void Inject(ICivilizationPlayer civilizationPlayer)
    {
        this._civilizationPlayer = civilizationPlayer;
    }

    public void Assing(IAbility ability)
    {
        this._ability = ability;

        indicatorFon.sprite = ability.Art;
        indicatorFon.sprite = ability.Fon;
        frame.sprite = ability.Frame;

        ability.ProgressEvent += ProgressEvent;

        Select(false);
        gameObject.SetActive(ability.IsActive);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_ability.IsReady && _ability.IsActive)
        {
            if (_civilizationPlayer.SelectAbility == this)
            {
                Select(false);
                _civilizationPlayer.SelectAbility = null;
            }
            else
            {
                _civilizationPlayer.SelectAbility?.Select(false);
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
            _civilizationPlayer.SelectAbility = this;
        }
        else
        {
            if (_ability.IsReady)
            {
                frame.enabled = true;
                frame.color = new Color(0, 0.7f, 1, 1);
            }
            else
            {
                frame.enabled = false;
                indicator.fillAmount = 0;
            }
        }
    }

    private void ProgressEvent(float rogress)
    {
        indicator.fillAmount = rogress / 100;
    }
}
