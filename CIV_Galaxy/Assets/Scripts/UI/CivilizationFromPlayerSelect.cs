using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CivilizationFromPlayerSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image fon, art;
    [SerializeField] private Text nameCiv;
    [SerializeField] private CivilizationScriptable civData;

    private GameUI gameUI;
    private ICivilizationPlayer civPlayer;

   [Inject]
    public void Inject(GameUI gameUI, ICivilizationPlayer civPlayer)
    {
        this.gameUI = gameUI;
        this.civPlayer = civPlayer;

        art.sprite = civData.Icon;
        nameCiv.text = civData.Name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        civPlayer.CurrentCivilization = civData.Name;
        gameUI.StartUI("Galaxy");
    }
}
