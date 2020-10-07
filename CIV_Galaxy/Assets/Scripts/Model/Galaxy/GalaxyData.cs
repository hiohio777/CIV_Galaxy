using UnityEngine;
using Zenject;

public class GalaxyData : MonoBehaviour
{
    [SerializeField] private int allPlanet = 1000;
    [SerializeField, Range(0, 600)] private int endGametime = 300; // Время после открытия всех планет до конца игры
    private int _openPlanets;
    private CanvasFonGalaxy _canvasFonGalaxy;
    private MessageGalaxy _messageWholeGalaxyExplored;
    private CounterEndGame _counterEndGame;

    [Inject]
    public void Inject(CanvasFonGalaxy canvasFonGalaxy, MessageGalaxy messageWholeGalaxyExplored,
        CounterEndGame counterEndGame)
    {
        this._canvasFonGalaxy = canvasFonGalaxy;
        this._messageWholeGalaxyExplored = messageWholeGalaxyExplored;
        this._counterEndGame = counterEndGame;

        _canvasFonGalaxy.ProgressEvent(allPlanet / 100);
    }

    public int CountAllPlanet => allPlanet - _openPlanets;

    public SpriteUnitEnum GetTypePlanet()
    {
        SpriteUnitEnum spriteUnitEnum = (SpriteUnitEnum)UnityEngine.Random.Range(0, 4);
        _openPlanets++;

        _canvasFonGalaxy.ProgressEvent(_openPlanets / ((float)allPlanet / 100));
        if (_openPlanets >= allPlanet)
        {
            // Запуск счётчика конца игры
            _messageWholeGalaxyExplored.Show("Вся Галактика исследована!", () => _counterEndGame.Show(endGametime));
        }


        return spriteUnitEnum;
    }
}
