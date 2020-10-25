using System.Collections.Generic;
using UnityEngine;

public abstract class RegisterMonoBehaviour : NoRegisterMonoBehaviour
{
    [SerializeField] private bool isRegister = true;

    protected virtual void Awake()
    {
        if(isRegister) GameManager.Instance.Register(this);
    }
}
