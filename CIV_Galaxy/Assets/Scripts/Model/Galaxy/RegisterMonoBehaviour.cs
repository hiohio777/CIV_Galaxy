using System.Collections.Generic;
using UnityEngine;

public abstract class RegisterMonoBehaviour : NoRegisterMonoBehaviour
{
    [SerializeField] public bool isRegister = true;

    protected virtual void Awake()
    {
        if(isRegister) Register(this);
    }
}
