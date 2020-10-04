using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class SciencePanelUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image indicator;
    [SerializeField] private GameObject IconAccessible;
    [SerializeField] private Text points;

    private SciencePlayerUI _sciencePlayerUI;

    [Inject]
    public void Inject(SciencePlayerUI sciencePlayerUI)
    {
        this._sciencePlayerUI = sciencePlayerUI;
    }

    public void SetSciencePoints(int points, bool isAccessible)
    {
        this.points.text = points.ToString();
        IconAccessible.SetActive(isAccessible);
    }

    public void ProgressEvent(float rogress)
    {
        indicator.fillAmount = rogress / 100;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _sciencePlayerUI.Enable();
    }
}
