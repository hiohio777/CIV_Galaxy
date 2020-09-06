using System;
using UnityEngine;
using Zenject;

public class GalaxyUI : PanelUI
{
    private IGalaxyUITimer _galaxyUITimer;
    private GalaxyGame _galaxyGame;
    private ICivilizationPlayer _civPlayer;
    private GalaxyFon _galaxyFon;
    private CivilizationAlPanel _civilizationAlPanel;

    private MessageStartGame.Factory _factoryMessageStartGame;

    [Inject]
    public void Inject(GalaxyGame galaxyGame, ICivilizationPlayer civPlayer, IGalaxyUITimer galaxyUITimer,
     MessageStartGame.Factory factoryMessageStartGame, CivilizationAlPanel civilizationAlPanel)
    {
        (this._galaxyGame, this._civPlayer, this._galaxyUITimer, _factoryMessageStartGame, this._civilizationAlPanel)
        = (galaxyGame, civPlayer, galaxyUITimer, factoryMessageStartGame, civilizationAlPanel);
    }

    public override void Enable()
    {
        base.Enable();

        _galaxyGame.InitializeNewGame();

        _galaxyFon = Instantiate(Resources.Load<GalaxyFon>("GalaxyFon"));
        _civilizationAlPanel.SetActive(true);

        _galaxyUITimer.StartTimer(_galaxyGame.ExecuteOnTime);
    }

    public override void DisableFinish()
    {
        _galaxyUITimer.SetPause(true);
        _galaxyFon.Destroy();
        _civilizationAlPanel.SetActive(false);

        base.DisableFinish();
    }

    public void ShowMessageStart()
    {
        _factoryMessageStartGame.Create().Show(_civPlayer.CivData, () => animator.SetTrigger("PlayerStart"));
    }
}
