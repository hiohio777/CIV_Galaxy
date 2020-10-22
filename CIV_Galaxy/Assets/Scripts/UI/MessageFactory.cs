using System;

public class MessageFactory : BaseFactory
{
    private IGalaxyUITimer _galaxyUITimer;

    private void Start()
    {
        this._galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
    }

    public MessageDiscoveredCivilization GetMessageDiscoveredCivilization(CivilizationScriptable civData, Action actWelcome) =>
        InstantiateObject<MessageDiscoveredCivilization>("Message/MessageDiscoveredCivilization").Show(_galaxyUITimer, civData, actWelcome);

    public MessageStartGame GetMessageStartGame(CivilizationScriptable civData, Action actOK = null) =>
        InstantiateObject<MessageStartGame>("Message/MessageStartGame").Show(_galaxyUITimer, civData, actOK);

    public MessageBackMainMenu GetMessageBackMainMenu(Action actYes = null, Action actNo = null) =>
       InstantiateObject<MessageBackMainMenu>("Message/MessageBackMainMenu").Show(_galaxyUITimer, actYes, actNo);
}
