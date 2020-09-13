using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainSceneUI : MonoBehaviour
{
    private Dictionary<string, PanelUI> panelsUI = new Dictionary<string, PanelUI>();
    private PanelUI current;
    private string nextPanalName;

    private LoaderDataGame loaderDataGame;

    [Inject]
    public void Inject(LoaderDataGame loaderDataGame)
    {
        this.loaderDataGame = loaderDataGame;
    }

    public void StartUI(string panalName)
    {
        if (panelsUI.ContainsKey(panalName) == false)
        {
            Debug.Log($"StartUI -> Нет такого UI: {panalName}");
            return;
        }

        nextPanalName = panalName;
        if (current == panelsUI[nextPanalName]) return;

        current.Disable();
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame!");

        loaderDataGame.Save(Application.Quit);
    }

    private void StartGame()
    {
        current = panelsUI["Screensaver"];
        current.Enable();
    }

    private void RestartMainScene()
    {
        current = panelsUI["MainMenu"];
        current.Enable();
    }

    private void DisableFinishUI()
    {
        current = panelsUI[nextPanalName];
        current.Enable();
    }

    private void Start()
    {
        var panels = new List<PanelUI>();
        GetComponentsInChildren<PanelUI>(true, panels);

        PanelUI.startNewPanelUI = StartUI;
        PanelUI.finishDisableUI = DisableFinishUI;
        panels.ForEach(x => panelsUI.Add(x.name, x.Initialize()));

        loaderDataGame.Load(StartGame, RestartMainScene);
    }
}