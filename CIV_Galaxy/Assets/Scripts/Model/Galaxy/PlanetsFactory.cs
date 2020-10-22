using System.Collections.Generic;
using UnityEngine;

public class PlanetsFactory : BaseFactory
{
    [SerializeField] private Planet planetprefab;

    public IPlanet GetNewUnit()
    {
        Planet unit;

        if (buffer.Count > 0) unit = buffer.Pop() as Planet;
        else
        {
            unit = InstantiateObject(planetprefab);
            unit.Creat(Buffered);
        }

        unit.Initialize();
        return unit;
    }
}

public abstract class BaseFactory : RegisterMonoBehaviour
{
    protected readonly Stack<object> buffer = new Stack<object>();
    public void ClearBuffer() => buffer.Clear();
    protected void Buffered(object obj) => buffer.Push(obj);
    protected T InstantiateObject<T>(string pathPrefab) where T : Object => Instantiate(Resources.Load<T>(pathPrefab));
    protected T InstantiateObject<T>(T gameObject) where T : Object => Instantiate(gameObject);
}
