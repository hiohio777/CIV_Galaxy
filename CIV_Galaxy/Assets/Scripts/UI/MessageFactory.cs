using System;
using UnityEngine;

public class MessageFactory : BaseFactory
{
    [SerializeField] private MessageDiscoveredCivilization messageDiscoveredCivilizationPrefab;
    [SerializeField] private MessageStartGame messageStartGamePrefab;
    [SerializeField] private MessageBackMainMenu messageBackMainMenuPrefab;

    private IGalaxyUITimer _galaxyUITimer;

    private void Start()
    {
        this._galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
    }

    public MessageDiscoveredCivilization GetMessageDiscoveredCivilization(CivilizationScriptable civData, Action actWelcome) =>
        InstantiateObject(messageDiscoveredCivilizationPrefab).Show(_galaxyUITimer, civData, actWelcome);

    public MessageStartGame GetMessageStartGame(CivilizationScriptable civData, Action actOK = null) =>
        InstantiateObject(messageStartGamePrefab).Show(_galaxyUITimer, civData, actOK);

    public MessageBackMainMenu GetMessageBackMainMenu(Action actYes = null, Action actNo = null) =>
       InstantiateObject(messageBackMainMenuPrefab).Show(_galaxyUITimer, actYes, actNo);
}
