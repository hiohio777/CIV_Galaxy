using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class CanvasFonGalaxy : MonoBehaviour
{
    [SerializeField] private GameObject galaxyExploredAreaObject;
    [SerializeField] private Image galaxyExploredArea;
    [SerializeField] private Image galaxyFon;
    [SerializeField] private Sprite[] galaxyFonSprite;

    private MovingObject _moving;

    [Inject]
    public void Inject(PlayerSettings playerSettings)
    {
        galaxyFon.sprite = galaxyFonSprite[(int)playerSettings.CurrentDifficult];
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