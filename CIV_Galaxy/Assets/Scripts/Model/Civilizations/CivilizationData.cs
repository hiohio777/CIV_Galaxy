public class CivilizationData
{
    public CivilizationScriptable CivData { get; private set; }
    private int _planets;
    private ICivilizationDataUI _dataUI;

    public int Planets { get => _planets; set { _planets = value; _dataUI.SetCountPlanet(_planets); } }

    public void Initialize(CivilizationScriptable civData, ICivilizationDataUI dataUI)
    {
        (this.CivData, this._dataUI) = (civData, dataUI);
        Planets = civData.Planets;
    }

    public void AddPlanet(IPlanet planet)
    {
        Planets++;
        planet.Destroy();
    }
}
