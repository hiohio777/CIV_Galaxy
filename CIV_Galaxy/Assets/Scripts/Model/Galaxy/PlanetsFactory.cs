using System.Collections.Generic;

public class PlanetsFactory
{
    private readonly Stack<Planet> buffer = new Stack<Planet>();
    private readonly Planet.Factory factory;

    public PlanetsFactory(Planet.Factory factory)
    {
        this.factory = factory;
    }

    public void ClearBuffer() => buffer.Clear();
    private void Buffered(Planet obj) => buffer.Push(obj);

    public IPlanet GetNewPlanet(TypePlanetEnum typePlanet)
    {
        Planet planet;

        if (buffer.Count > 0) planet = buffer.Pop();
        else planet = factory.Create(Buffered);

        planet.Initialize(typePlanet);
        return planet;
    }
}
