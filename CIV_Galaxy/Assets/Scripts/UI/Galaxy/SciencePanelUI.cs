using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SciencePanelUI : MonoBehaviour, IPointerClickHandler
{
    private SciencePlayerUI _sciencePlayerUI;

    [Inject]
    public void Inject(SciencePlayerUI sciencePlayerUI)
    {
        this._sciencePlayerUI = sciencePlayerUI;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _sciencePlayerUI.Enable();
    }
}

