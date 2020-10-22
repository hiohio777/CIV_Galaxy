using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectRegister : IRegister, IFinder
{
    private static readonly ObjectRegister _instance = new ObjectRegister();
    public static IRegister InstanceReg => _instance;
    public static IFinder InstanceFinder => _instance;
    private ObjectRegister()
    { }

    private List<object> registrObject = new List<object>();

    public void Clear() => registrObject.Clear();

    public void Register(object obj) => registrObject.Add(obj);

    public T GetRegisterObject<T>()
    {
        foreach (var item in registrObject)
        {
            if (item is T obj)
                return obj;
        }

        throw new ArgumentNullException(typeof(T).FullName);
    }
    public List<T> GetRegisterObjects<T>()
    {
        var listObject = new List<T>();
        foreach (var item in registrObject)
        {
            if (item is T obj)
                listObject.Add(obj);
        }

        return listObject;
    }
}

public interface IRegister
{
    void Clear();
    void Register(object obj);
}

public interface IFinder
{
    List<T> GetRegisterObjects<T>();
    T GetRegisterObject<T>();
}

public abstract class RegisterMonoBehaviour : MonoBehaviour, IFinder
{
    [SerializeField] private bool isRegister = true;

    protected virtual void Awake()
    {
        if(isRegister) ObjectRegister.InstanceReg.Register(this);
    }

    public T GetRegisterObject<T>() => ObjectRegister.InstanceFinder.GetRegisterObject<T>();
    public List<T> GetRegisterObjects<T>() => ObjectRegister.InstanceFinder.GetRegisterObjects<T>();
}