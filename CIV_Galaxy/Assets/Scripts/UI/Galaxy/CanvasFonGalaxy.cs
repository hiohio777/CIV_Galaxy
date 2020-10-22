using UnityEngine;
using UnityEngine.UI;

public class CanvasFonGalaxy : RegisterMonoBehaviour
{
    [SerializeField] private GameObject galaxyExploredAreaObject;
    [SerializeField] private Image galaxyExploredArea;
    [SerializeField] private Image galaxyFon;
    [SerializeField] private Sprite[] galaxyFonSprite;

    private MovingObject _moving;

    private void Start()
    {
        galaxyFon.sprite = galaxyFonSprite[(int)PlayerSettings.Instance.CurrentDifficult];
        _moving = GetComponentInChildren<MovingObject>().AssignScale(0);
    }

    public void ProgressEvent(float rogress)
    {
        galaxyExploredArea.fillAmount = rogress / 100;
    }

    public void DisplayGalaxy()
    {
        galaxyExploredArea.fillAmount = 0;

        galaxyExploredAreaObject.SetActive(true);
        _moving.SetScale(1, 1f).Run();
    }
}