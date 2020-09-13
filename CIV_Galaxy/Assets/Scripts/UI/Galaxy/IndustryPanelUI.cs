using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class IndustryPanelUI : MonoBehaviour
{
    [SerializeField] private Image indicator;
    [SerializeField] private Text points;

    public void SetIndustryPoints(int points, float pointProc)
    {
        this.points.text = points.ToString();
        indicator.fillAmount = pointProc / 100;
    }

    private void Start()
    {
        indicator.fillAmount = 0;
        points.text = "0";
    }
}