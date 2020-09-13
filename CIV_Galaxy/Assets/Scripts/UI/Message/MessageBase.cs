using UnityEngine;

public abstract class MessageBase : MonoBehaviour
{
    protected Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("DisplayMessage");
    }
}