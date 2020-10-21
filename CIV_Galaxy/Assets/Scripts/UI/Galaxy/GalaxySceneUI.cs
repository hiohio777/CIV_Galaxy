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
    private MessageFactory _messageFactory;
    private CanvasFonGalaxy _fonGalaxy;

    [Inject]
    public void Inject(Civilizations allCivilizations, ICivilizationPlayer civPlayer, List<ICivilizationAl> civsAl, PlayerSettings playerData,
        MessageFactory messageFactory, SciencePlayerUI sciencePlayerUI, CanvasFonGalaxy fonGalaxy)
    {
        (this._allCivilizations, this._civPlayer, _civsAl, _playerData, this._messageFactory, this._fonGalaxy)
        = (allCivilizations, civPlayer, civsAl, playerData, messageFactory, fonGalaxy);
    }

    public void BackMainMenu()
    {
        _messageFactory.GetMessageBackMainMenu(BackMainScene);
    }

    public void DisplayUI()
    {
        _fonGalaxy.DisplayGalaxy();
        _civsAl.ForEach(x => x.DisplayCloseCiv());
        _civPlayer.DisplayCiv();
    }

    public void BackMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void Start()
    {
        InitializeNewGame();

        _animator = GetComponent<Animator>();
        _messageFactory.GetMessageStartGame(_civPlayer.DataBase, () => _animator.SetTrigger("Start"));
    }

    public void InitializeNewGame()
    {
        _allCivilizations.Refresh();

        // Создание цивилизаций
        _civPlayer.Assign(_allCivilizations.GetCivilizationPlayer(_playerData.CurrentCivilization));
        _civsAl.ForEach(x => x.Assign(_allCivilizations.GetCivilizationEnemy()));
    }
}
