using UnityEngine;

public class MyCanvas : MonoBehaviour
{
    [SerializeField] private SortingLayerEnum sortingLayerName = SortingLayerEnum.Default;

    private void Awake()
    {
        var canvas = GetComponent<Canvas>();

        canvas.sortingLayerName = sortingLayerName.ToString();
        canvas.worldCamera = Camera.main;
    }
}

public enum SortingLayerEnum
{
    Default,
    UI,
}