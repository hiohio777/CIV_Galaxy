using System.Collections.Generic;

public class PlanetsFactory : UnitBaseFactory
{
    private readonly Planet.Factory factory;

    public PlanetsFactory(Planet.Factory factory)
    {
        this.factory = factory;
    }

    public IPlanet GetNewUnit(SpriteUnitEnum typePlanet)
    {
        Planet unit;

        if (buffer.Count > 0) unit = buffer.Pop() as Planet;
        else unit = factory.Create(Buffered);

        unit.Initialize(typePlanet);
        return unit;
    }
}

public abstract class UnitBaseFactory
{
    protected readonly Stack<UnitBase> buffer = new Stack<UnitBase>();
    public void ClearBuffer() => buffer.Clear();
    protected void Buffered(UnitBase obj) => buffer.Push(obj);
}
