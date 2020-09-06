using UnityEngine;

public abstract class MessageBase : MonoBehaviour
{
    protected Animator _animator;

    private void Awake()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Default";

        _animator = GetComponent<Animator>();
        _animator.SetTrigger("DisplayMessage");
    }
}