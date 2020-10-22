using UnityEngine;

public class GalaxyData : RegisterMonoBehaviour
{
    [SerializeField] private int allPlanet = 1000;
    [SerializeField, Range(0, 600)] private int endGametime = 300; // Время после открытия всех планет до конца игры
    private int _openPlanets;
    private CanvasFonGalaxy _canvasFonGalaxy;
    private MessageGalaxy _messageWholeGalaxyExplored;
    private CounterEndGame _counterEndGame;

    public void Start()
    {
        _canvasFonGalaxy = GetRegisterObject<CanvasFonGalaxy>();
        _counterEndGame = GetRegisterObject<CounterEndGame>();
        _messageWholeGalaxyExplored = GetRegisterObject<MessageGalaxy>();

        _canvasFonGalaxy.ProgressEvent(allPlanet / 100);
    }

    public int CountAllPlanet => allPlanet - _openPlanets;

    public bool TakePlanetFromGalaxy()
    {
        _openPlanets++;
        _canvasFonGalaxy.ProgressEvent(_openPlanets / ((float)allPlanet / 100));

        if (_openPlanets >= allPlanet)
        {
            // Запуск счётчика конца игры
            _messageWholeGalaxyExplored.Show("Вся Галактика исследована!", () => _counterEndGame.Show(endGametime));
            return false;
        }

        return true;
    }
}
