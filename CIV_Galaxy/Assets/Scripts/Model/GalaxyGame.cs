using System.Collections.Generic;
using UnityEngine;

public class GalaxyGame
{
    public Civilizations civilizations { get; }

    private ICivilizationPlayer civPlayer;
    private List<ICivilizationAl> civsAl;

    private List<ICivilization> civs;

    public GalaxyGame(Civilizations civilizations, ICivilizationPlayer civPlayer, List<ICivilizationAl> civsAl)
    {
        (this.civilizations, this.civPlayer, this.civsAl) = (civilizations, civPlayer, civsAl);

        civs = new List<ICivilization>(civsAl);
        civs.Add(civPlayer);
    }

    public void InitializeNewGame()
    {
        civilizations.Reset();

        // Создание цивилизации игрока
        civPlayer.Assign(civilizations.GetCivilizationPlayer(civPlayer.CurrentCivilization));
        // Создание других цивилизаций
        civsAl.ForEach(x => x.Assign(civilizations.GetCivilizationEnemy()));
    }

    public void ExecuteOnTime(float secondTime)
    {
        civs.ForEach(x => x.ExecuteOnTime(secondTime));
    }
}
