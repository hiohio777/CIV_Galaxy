using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private JustRotateArtGalaxy imageArtGalaxy;
    private Dictionary<string, PanelUI> panelsUI = new Dictionary<string, PanelUI>();
    private PanelUI current;
    private string nextPanalName;
    private static bool isFirstStartGame = true;

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

        Application.Quit();
    }

    private void StartGame()
    {
        if (isFirstStartGame)
        {
            current = panelsUI["Screensaver"];
            isFirstStartGame = false;
        }
        else
            current = panelsUI["GameSettings"];

        current.Enable();
    }

    private void DisableFinishUI()
    {
        current = panelsUI[nextPanalName];
        current.Enable();
    }

    private void Start()
    {
        imageArtGalaxy.transform.localScale = new Vector3(0, 0, 0);

        var panels = new List<PanelUI>();
        GetComponentsInChildren<PanelUI>(true, panels);
        PanelUI.startNewPanelUI = StartUI;
        PanelUI.finishDisableUI = DisableFinishUI;
        panels.ForEach(x => panelsUI.Add(x.name, x.Initialize()));

        StartGame();
    }
}