using UnityEngine;
using UnityEngine.UI;

public class ScanerPanelUI : MonoBehaviour
{
    [SerializeField] private Image indicator;

    public void ProgressEvent(float rogress)
    {
        indicator.fillAmount = rogress / 100;
    }
}
