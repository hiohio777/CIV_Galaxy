using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CivilizationFromPlayerSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image fon, art;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField] private CivilizationScriptable civData;

    private MainSceneUI _mainSceneUI;
    private PlayerSettings _playerData;

    [Inject]
    public void Inject(MainSceneUI mainSceneUI, PlayerSettings playerData)
    {
        this._mainSceneUI = mainSceneUI;
        this._playerData = playerData;

        art.sprite = civData.Icon;
        nameCiv.SetKey(civData.Name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _playerData.CurrentCivilization = civData.Name;
        _mainSceneUI.StartUI("GameSettings");
    }
}
