using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SciencePanelUI : RegisterMonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image indicator;
    [SerializeField] private GameObject IconAccessible;
    [SerializeField] private Text points;

    private SciencePlayerUI _sciencePlayerUI;

    public void Start()
    {
        this._sciencePlayerUI = GetRegisterObject<SciencePlayerUI>();
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
