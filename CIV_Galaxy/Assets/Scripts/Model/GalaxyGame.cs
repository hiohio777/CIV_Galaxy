using System;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyGame
{
    public Civilizations civilizations { get; }

    private ICivilizationPlayer _civPlayer;
    private List<ICivilizationAl> _civsAl;
    private PlayerData _playerData;

    private List<ICivilization> _civs;

    public GalaxyGame(Civilizations civilizations, ICivilizationPlayer civPlayer, List<ICivilizationAl> civsAl, PlayerData playerData)
    {
        (this.civilizations, this._civPlayer, this._civsAl, this._playerData)
        = (civilizations, civPlayer, civsAl, playerData);

        _civs = new List<ICivilization>(civsAl);
        _civs.Add(civPlayer);
    }

    public void InitializeNewGame()
    {
        civilizations.Reset();

        // Создание цивилизации игрока
        _civPlayer.Assign(civilizations.GetCivilizationPlayer(_playerData.CurrentCivilization));
        // Создание других цивилизаций
        _civsAl.ForEach(x => x.Assign(civilizations.GetCivilizationEnemy()));
    }

    public void ExecuteOnTime(float secondTime)
    {
        for (int i = _civs.Count - 1; i >= 0; i--)
            _civs[i].ExecuteOnTime(secondTime);
    }
}
