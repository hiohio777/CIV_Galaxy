using System.Collections.Generic;

public class PlanetsFactory : BaseFactory
{
    private readonly Planet.Factory _factory;

    public PlanetsFactory(Planet.Factory factory)
    {
        this._factory = factory;
    }

    public IPlanet GetNewUnit()
    {
        Planet unit;

        if (buffer.Count > 0) unit = buffer.Pop() as Planet;
        else unit = _factory.Create(Buffered);

        unit.Initialize();
        return unit;
    }
}

public abstract class BaseFactory
{
    protected readonly Stack<object> buffer = new Stack<object>();
    public void ClearBuffer() => buffer.Clear();
    protected void Buffered(object obj) => buffer.Push(obj);
}
