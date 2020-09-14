using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class IndustryPanelUI : MonoBehaviour
{
    [SerializeField] private Image indicator, imageFullIndustry;

    public void SetIndustryPoints(float points)
    {
        indicator.fillAmount = points;
        if (points >= 1) imageFullIndustry.gameObject.SetActive(true);
        else imageFullIndustry.gameObject.SetActive(false);
    }
}