using Zenject;

public class LeaderQualifier
{
    private ICivilization _civPlayer;
    private ICivilization _lider;

    [Inject]
    public void Inject(ICivilizationPlayer civPlayer)
    {
        _lider = this._civPlayer = civPlayer;
    }

    public void DefineLeader(ICivilization civilization)
    {
        if (civilization == _lider) return;

        ComparePlayer(civilization);
        CompareLider(civilization);
    }

    private void ComparePlayer(ICivilization civilization)
    {
        if (civilization is ICivilizationAl)
        {
            if (civilization.CivData.DominationPoints > _civPlayer.CivData.DominationPoints)
                civilization.DefineLeader(LeaderEnum.Advanced);
            else
                civilization.DefineLeader(LeaderEnum.Lagging);
        }
        else civilization.DefineLeader(LeaderEnum.Lagging);
    }

    private void CompareLider(ICivilization civilization)
    {
        if (civilization != _lider && civilization.CivData.DominationPoints > _lider.CivData.DominationPoints)
        {
            civilization.DefineLeader(LeaderEnum.Leader);
            var oldLider = _lider;
            _lider = civilization;

            ComparePlayer(oldLider);
        }
    }
}