public class StatisticsData
{
    public StatisticsData() { }

    public StatisticsData(string nameCiv, int opponents, int difficult)
    {
        NameCiv = nameCiv;
        Opponents = (OpponentsEnum)opponents;
        Difficult = (DifficultEnum)difficult;
    }

    public string NameCiv;
    public int Years;
    public int CountDomination;
    public int CountPlanets;
    public int CountDiscoveries;
    public int CountBombs;
    public int CountSpaceFleet;
    public int CountScientificMission;
    public bool IsRecorded;

    public DifficultEnum Difficult { get; set; }
    public OpponentsEnum Opponents { get; set; }

    public void Write(int years, int countDomination, int countPlanets, int countDiscoveries, int countBombs, int countSpaceFleet, int countScientificMission)
    {
        (Years, CountDomination, CountPlanets, CountDiscoveries, CountBombs, CountSpaceFleet, CountScientificMission)
        = (years, countDomination, countPlanets, countDiscoveries, countBombs, countSpaceFleet, countScientificMission);

        IsRecorded = true;
    }
}