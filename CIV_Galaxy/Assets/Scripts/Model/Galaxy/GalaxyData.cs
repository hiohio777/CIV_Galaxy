public class GalaxyData
{
    private int idealPlanet, icePlanets, hotPlanets, gasGiantsPlanets;
    private GalaxyScriptableObject _galaxyScriptable;

    public GalaxyData(GalaxyScriptableObject galaxyScriptable)
    {
        this._galaxyScriptable = galaxyScriptable;

        icePlanets = galaxyScriptable.IcePlanets;
        hotPlanets = galaxyScriptable.HotPlanets;
        gasGiantsPlanets = galaxyScriptable.GasGiantsPlanets;
        idealPlanet = galaxyScriptable.IdealPlanet;
    }

    public int CountPlanet => idealPlanet + icePlanets + hotPlanets + gasGiantsPlanets;

    public TypePlanetEnum GetTypePlanet()
    {
        int rand = UnityEngine.Random.Range(1, CountPlanet);

        if (rand <= idealPlanet)
        {
            idealPlanet--;
            return TypePlanetEnum.Ideal;
        }

        if (rand <= idealPlanet + icePlanets)
        {
            icePlanets--;
            return TypePlanetEnum.Ice;
        }

        if (rand <= idealPlanet + icePlanets + hotPlanets)
        {
            hotPlanets--;
            return TypePlanetEnum.Hot;
        }

        gasGiantsPlanets--;
        return TypePlanetEnum.GasGiants;
    }
}
