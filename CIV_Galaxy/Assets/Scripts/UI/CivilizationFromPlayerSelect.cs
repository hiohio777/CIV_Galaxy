using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CivilizationFromPlayerSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image fon, art;
    [SerializeField] private Text nameCiv;
    [SerializeField] private CivilizationScriptable civData;

    private ChoiceCivilization _choiceCivilization;
    private PlayerSettings _playerData;

    [Inject]
    public void Inject(ChoiceCivilization choiceCivilization, PlayerSettings playerData)
    {
        this._choiceCivilization = choiceCivilization;
        this._playerData = playerData;

        art.sprite = civData.Icon;
        nameCiv.text = civData.Name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _playerData.CurrentCivilization = civData.Name;

        _choiceCivilization.AnimationClose();
    }
}
