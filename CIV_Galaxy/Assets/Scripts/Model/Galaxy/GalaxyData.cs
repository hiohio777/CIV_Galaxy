public class GalaxyData
{
    private int _idealPlanet, _icePlanets, _hotPlanets, _gasGiantsPlanets, _allPlanet, _openPlanets;
    private CanvasFonGalaxy _canvasFonGalaxy;
    private MessageWholeGalaxyExplored _messageWholeGalaxyExplored;

    public GalaxyData(GalaxyScriptableObject galaxyScriptable, CanvasFonGalaxy canvasFonGalaxy, MessageWholeGalaxyExplored messageWholeGalaxyExplored)
    {
        this._canvasFonGalaxy = canvasFonGalaxy;
        this._messageWholeGalaxyExplored = messageWholeGalaxyExplored;

        _icePlanets = galaxyScriptable.IcePlanets;
        _hotPlanets = galaxyScriptable.HotPlanets;
        _gasGiantsPlanets = galaxyScriptable.GasGiantsPlanets;
        _idealPlanet = galaxyScriptable.IdealPlanet;

        _allPlanet = _idealPlanet + _icePlanets + _hotPlanets + _gasGiantsPlanets;
        _canvasFonGalaxy.ProgressEvent(_allPlanet / 100);
    }

    public int CountAllPlanet => _allPlanet - _openPlanets;

    public TypePlanetEnum GetTypePlanet()
    {
        int rand = UnityEngine.Random.Range(1, CountAllPlanet);
        TypePlanetEnum typePlanetEnum;

        if (rand <= _idealPlanet)
        {
            _idealPlanet--;
            typePlanetEnum = TypePlanetEnum.Ideal;
        }
        else if (rand <= _idealPlanet + _icePlanets)
        {
            _icePlanets--;
            typePlanetEnum = TypePlanetEnum.Ice;
        }
        else if (rand <= _idealPlanet + _icePlanets + _hotPlanets)
        {
            _hotPlanets--;
            typePlanetEnum = TypePlanetEnum.Hot;
        }
        else
        {
            _gasGiantsPlanets--;
            typePlanetEnum = TypePlanetEnum.GasGiants;
        }

        _openPlanets++;

        _canvasFonGalaxy.ProgressEvent(_openPlanets / ((float)_allPlanet / 100));
        if (_openPlanets >= _allPlanet)
            _messageWholeGalaxyExplored.Show("Вся Галактика исследована!");

        return typePlanetEnum;
    }
}
