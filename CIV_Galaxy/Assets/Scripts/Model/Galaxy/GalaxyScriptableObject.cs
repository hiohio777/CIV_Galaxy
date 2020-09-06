using UnityEngine;

[CreateAssetMenu(fileName = "GalaxyScriptableObject", menuName = "Data/Galaxy", order = 50)]
public class GalaxyScriptableObject: ScriptableObject
{
    [SerializeField] private int icePlanets, hotPlanets, gasGiantsPlanets, idealPlanet;

    public int IcePlanets => icePlanets;
    public int HotPlanets => hotPlanets;
    public int GasGiantsPlanets => gasGiantsPlanets;
    public int IdealPlanet => idealPlanet;
}
