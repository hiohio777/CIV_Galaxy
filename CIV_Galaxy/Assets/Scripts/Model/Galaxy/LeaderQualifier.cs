using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LeaderQualifier : MonoBehaviour
{
    private List<ICivilization> _civilizations = new List<ICivilization>();
    private ICivilizationPlayer _civPlayer;
    private List<ICivilizationAl> _civsAl;


    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer, ICivilizationPlayer civPlayer, List<ICivilizationAl> civsAl)
    {
        this._civPlayer = civPlayer;
        this._civsAl = civsAl;

        galaxyUITimer.ExecuteYears += ExecuteYears;
        _civilizations.Add(civPlayer);
        civsAl.ForEach(x => _civilizations.Add(x));

        Debug.Log("Inject");
    }

    private void ExecuteYears()
    {
        // определение отсталости или опережения в сравнении с игроком
        foreach (var item in _civsAl)
        {
            if (item.CivData.DominationPoints >= _civPlayer.CivData.DominationPoints)
                item.DefineLeader(LeaderEnum.Advanced);
            else
                item.DefineLeader(LeaderEnum.Lagging);
        }

        ICivilization lider = _civilizations[0];
        foreach (var item in _civilizations)
        {
            if (item.CivData.DominationPoints > lider.CivData.DominationPoints)
                lider = item;
        }

        lider.DefineLeader(LeaderEnum.Leader);
    }
}