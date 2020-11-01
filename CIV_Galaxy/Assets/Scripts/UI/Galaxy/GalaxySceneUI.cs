using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GalaxySceneUI : RegisterMonoBehaviour
{
    [SerializeField, Space(10)] private MessageGalaxy messageGalaxy;
    [SerializeField] private CounterEndGame counterEndGame;
    [SerializeField] private InfoCivilizationPanelUI infoCivilizationPanelUI;
    [SerializeField] private SciencePlayerUI sciencePlayerUI;

    [SerializeField, Space(10)] private AudioClip musicScene;
    private Animator _animator;
    private ICivilizationPlayer _civPlayer;
    private List<ICivilizationAl> _civsAl;
    private MessageFactory _messageFactory;
    private CanvasFonGalaxy _fonGalaxy;

    protected override void Awake()
    {
        base.Awake();
        Register(messageGalaxy, counterEndGame, infoCivilizationPanelUI, sciencePlayerUI);
    }

    private void Start()
    {
        _civPlayer = GetRegisterObject<ICivilizationPlayer>();
        _civsAl = GetRegisterObjects<ICivilizationAl>();
        _messageFactory = GetRegisterObject<MessageFactory>();
        _fonGalaxy = GetRegisterObject<CanvasFonGalaxy>();

        InitializeNewGame();

        _animator = GetComponent<Animator>();
        _messageFactory.GetMessageStartGame(_civPlayer.DataBase, () => _animator.SetTrigger("Start"));
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
        ClearRegisteScene(); // Очистить регистр обьектов сцены
        SceneManager.LoadScene("MainScene");
    }

    public void InitializeNewGame()
    {
        PlayNewMusic(musicScene);

        Civilizations.Instance.Refresh();

        // Создание цивилизаций
        _civPlayer.Assign(Civilizations.Instance.GetCivilizationPlayer(PlayerSettings.Instance.CurrentCivilization));
        _civsAl.ForEach(x => x.Assign(Civilizations.Instance.GetCivilizationEnemy()));
    }
}
