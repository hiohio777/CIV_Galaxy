using System;

[Serializable]
public class StatisticsData
{
    public StatisticsData(DifficultEnum difficult, OpponentsEnum opponents)
    {
        Difficult = difficult;
        Opponents = opponents;
    }

    public int CountDomination { get; private set; }
    public int CountPlanets { get; private set; }
    public int CountDiscoveries { get; private set; }
    public int CountBombs { get; private set; }
    public int CountSpaceFleet { get; private set; }
    public int CountScientificMission { get; private set; }

    public bool isRecorded { get; private set; } = false;
    public DifficultEnum Difficult { get; private set; } = DifficultEnum.Easy;
    public OpponentsEnum Opponents { get; private set; } = OpponentsEnum.Two;

    public void Write(int countDomination, int countPlanets, int countDiscoveries, int countBombs, int countSpaceFleet, int countScientificMission)
    {
        (CountDomination, CountPlanets, CountDiscoveries, CountBombs, CountSpaceFleet, CountScientificMission)
        = (countDomination, countPlanets, countDiscoveries, countBombs, countSpaceFleet, countScientificMission);

        isRecorded = true;
    }
}