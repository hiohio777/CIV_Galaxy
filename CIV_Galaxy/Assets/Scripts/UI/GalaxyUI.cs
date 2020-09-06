using System;
using UnityEngine;
using Zenject;

public class GalaxyUI : PanelUI
{
    [SerializeField] private MessageStart messageStartGame;

    private GalaxyUITimer galaxyUITimer;
    private GalaxyGame galaxyGame;
    private ICivilizationPlayer civPlayer;
    private GalaxyFon galaxyFon;

    [Inject]
    public void Inject(GalaxyGame galaxyGame, ICivilizationPlayer civPlayer, GalaxyUITimer galaxyUITimer)
    {
        (this.galaxyGame, this.civPlayer, this.galaxyUITimer)
        = (galaxyGame, civPlayer, galaxyUITimer);
    }

    public override void Enable()
    {
        base.Enable();

        galaxyGame.InitializeNewGame();
        messageStartGame.Initialize(civPlayer.CivData, () => animator.SetTrigger("PlayerStart"));

        galaxyFon = Instantiate(Resources.Load<GalaxyFon>("GalaxyFon"));
    }

    public override void DisableFinish()
    {
        galaxyUITimer.StopTimer();
        galaxyFon.Destroy();

        base.DisableFinish();

        Debug.Log("StopTimer");
    }

    public void StartGame()
    {
        galaxyUITimer.StartTimer(galaxyGame.ExecuteOnTime);
        galaxyUITimer.OnPause();
    }
}