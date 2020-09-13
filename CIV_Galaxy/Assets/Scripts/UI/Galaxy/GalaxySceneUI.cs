using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GalaxySceneUI : MonoBehaviour
{
    private Animator _animator;

    private IGalaxyUITimer _galaxyUITimer;
    private GalaxyGame _galaxyGame;
    private ICivilizationPlayer _civPlayer;

    private MessageStartGame _messageStartGame;
    private MessageBackMainMenu _messageBackMainMenu;

    [Inject]
    public void Inject(GalaxyGame galaxyGame, ICivilizationPlayer civPlayer, IGalaxyUITimer galaxyUITimer,
     MessageStartGame messageStartGame, MessageBackMainMenu messageBackMainMenu, SciencePlayerUI sciencePlayerUI)
    {
        (this._galaxyGame, this._civPlayer, this._galaxyUITimer, this._messageStartGame, this._messageBackMainMenu)
        = (galaxyGame, civPlayer, galaxyUITimer, messageStartGame, messageBackMainMenu);

        sciencePlayerUI.gameObject.SetActive(false);
    }

    public void BackMainMenu()
    {
        _messageBackMainMenu.Show(_civPlayer.DataBase, BackMainScene, () => { });
    }

    public void ShowMessageStart()
    {
        _messageStartGame.Show(_civPlayer.DataBase, () => _animator.SetTrigger("PlayerStart"));
    }

    private void BackMainScene()
    {
        Debug.Log("BackMainScen!");
        SceneManager.LoadScene("MainScene");
    }

    private void Start()
    {
        _galaxyGame.InitializeNewGame();
        _galaxyUITimer.StartTimer(_galaxyGame.ExecuteOnTime);

        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Start");
    }
}
