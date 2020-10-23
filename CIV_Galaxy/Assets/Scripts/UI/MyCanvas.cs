using UnityEngine;

public class MyCanvas : MonoBehaviour
{
    [SerializeField] private SortingLayerEnum sortingLayerName = SortingLayerEnum.Default;

    private void Awake()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = sortingLayerName.ToString();
    }
}

public enum SortingLayerEnum
{
    Default,
    UI,
}