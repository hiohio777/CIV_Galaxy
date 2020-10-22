using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageDiscoveredCivilization : MonoBehaviour
{
    [SerializeField] private Image artDiscoveredCivilization;
    [SerializeField] private LocalisationText nameCiv, descriptionCiv;
    [SerializeField] private Button welcome;

    private Action _actWelcome;
    private IGalaxyUITimer _galaxyUITimer;
    private Animator _animator;

    public MessageDiscoveredCivilization Show(IGalaxyUITimer galaxyUITimer, CivilizationScriptable civData, Action actWelcome)
    {
        this._galaxyUITimer = galaxyUITimer;

        _animator = GetComponent<Animator>();
        welcome.onClick.AddListener(OnWelcome);

        this._actWelcome = actWelcome;
        nameCiv.SetKey(civData.Name);
        descriptionCiv.SetKey(civData.Description);
        artDiscoveredCivilization.sprite = civData.Icon;

        _galaxyUITimer.SetPause(true, string.Empty);
        _animator.SetTrigger("DisplayMessage");

        return this;
    }

    public void EndAnimation()
    {
        _actWelcome.Invoke();
        welcome.interactable = true;

        _galaxyUITimer.SetPause(false);
        Destroy(gameObject);
    }

    private void OnWelcome()
    {
        _animator.SetTrigger("CloseMessage");

        welcome.interactable = false;
    }
}
