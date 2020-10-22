using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CivilizationFromPlayerSelect : MonoBehaviour
{
    [SerializeField] private Image fon, art;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField] private CivilizationScriptable civData;

    private MainSceneUI _mainSceneUI;

    private void Awake()
    {
        _mainSceneUI = GetComponentInParent<MainSceneUI>();
        GetComponent<Button>().onClick.AddListener(OnSelect);
        art.sprite = civData.Icon;
        nameCiv.SetKey(civData.Name);
    }

    private void OnSelect()
    {
        PlayerSettings.Instance.CurrentCivilization = civData.Name;
        _mainSceneUI.StartUI("GameSettings");
    }
}
