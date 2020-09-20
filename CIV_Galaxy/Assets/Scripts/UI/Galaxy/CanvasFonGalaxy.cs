using UnityEngine;
using UnityEngine.UI;

public class CanvasFonGalaxy : MonoBehaviour
{
    [SerializeField] private Image galaxyExploredArea;

    public void ProgressEvent(float rogress)
    {
        galaxyExploredArea.fillAmount = rogress / 100;
    }

    private void Awake()
    {
        galaxyExploredArea.fillAmount = 0;
    }
}