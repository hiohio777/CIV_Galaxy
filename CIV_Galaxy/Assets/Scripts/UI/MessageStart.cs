using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageStart : MonoBehaviour
{
    [SerializeField] private Image fon, art;
    [SerializeField] private LocalisationText nameCiv;
    [SerializeField] private LocalisationText descriptionCiv;
    [SerializeField] private Button OK;

    private Action actOK;

    public void Initialize(CivilizationScriptable civData, Action actOK)
    {
        this.actOK = actOK;
        nameCiv.SetKey(civData.Name);
        descriptionCiv.SetKey(civData.Description);
        art.sprite = civData.Icon;

        OK.onClick.AddListener(OnOK);
    }

    public void OnOK() => actOK.Invoke();

}