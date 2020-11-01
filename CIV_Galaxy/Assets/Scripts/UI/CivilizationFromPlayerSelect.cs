using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class CivilizationFromPlayerSelect : NoRegisterMonoBehaviour
{
    [SerializeField] private Image fon, art;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField] private CivilizationScriptable civData;

    private MainSceneUI _mainSceneUI;
    [HideInInspector] public Button button;

    private void Start()
    {
        _mainSceneUI = GetComponentInParent<MainSceneUI>();
        art.sprite = civData.Icon;
        nameCiv.SetKey(civData.Name);

        button = GetComponent<Button>();
        button.onClick.AddListener(OnSelect);
    }

    private void OnSelect()
    {
        PlayerSettings.Instance.CurrentCivilization = civData.Name;
        _mainSceneUI.StartUI("GameSettings");
    }
}
