using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GalaxySceneUI : MonoBehaviour
{
    private Animator _animator;
    private ICivilizationPlayer _civPlayer;
    private List<ICivilizationAl> _civsAl;
    private Civilizations _allCivilizations;
    private PlayerSettings _playerData;

    private MessageStartGame _messageStartGame;
    private MessageBackMainMenu _messageBackMainMenu;

    [Inject]
    public void Inject(Civilizations allCivilizations, ICivilizationPlayer civPlayer, List<ICivilizationAl> civsAl, PlayerSettings playerData,
        MessageStartGame messageStartGame, MessageBackMainMenu messageBackMainMenu, SciencePlayerUI sciencePlayerUI)
    {
        (this._allCivilizations, this._civPlayer, _civsAl, _playerData, this._messageStartGame, this._messageBackMainMenu)
        = (allCivilizations, civPlayer, civsAl, playerData, messageStartGame, messageBackMainMenu);

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

    public void BackMainScene()
    {
        Debug.Log("BackMainScen!");
        SceneManager.LoadScene("MainScene");
    }

    private void Start()
    {
        InitializeNewGame();

        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Start");
    }

    public void InitializeNewGame()
    {
        _allCivilizations.Refresh();

        // Создание цивилизаций
        _civPlayer.Assign(_allCivilizations.GetCivilizationPlayer(_playerData.CurrentCivilization));
        _civsAl.ForEach(x => x.Assign(_allCivilizations.GetCivilizationEnemy()));
    }
}
