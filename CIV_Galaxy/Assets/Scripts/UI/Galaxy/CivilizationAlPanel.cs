using UnityEngine;
using Zenject;

public class CivilizationAlPanel : MonoBehaviour
{
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}