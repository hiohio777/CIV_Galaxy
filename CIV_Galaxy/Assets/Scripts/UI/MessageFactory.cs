using System;

public class MessageFactory
{
    private MessageDiscoveredCivilization.Factory _messageDiscoveredCivilizationFactory;
    private MessageStartGame.Factory _messageStartGameFactory;
    private MessageBackMainMenu.Factory _messageBackMainMenuFactory;

    public MessageFactory(MessageDiscoveredCivilization.Factory messageDiscoveredCivilizationFactory, 
        MessageStartGame.Factory messageStartGameFactory, 
        MessageBackMainMenu.Factory messageBackMainMenuFactory)
    {
        _messageDiscoveredCivilizationFactory = messageDiscoveredCivilizationFactory ?? throw new ArgumentNullException(nameof(messageDiscoveredCivilizationFactory));
        _messageStartGameFactory = messageStartGameFactory ?? throw new ArgumentNullException(nameof(messageStartGameFactory));
        _messageBackMainMenuFactory = messageBackMainMenuFactory ?? throw new ArgumentNullException(nameof(messageBackMainMenuFactory));
    }

    public MessageDiscoveredCivilization GetMessageDiscoveredCivilization(CivilizationScriptable civData, Action actWelcome, Action actOffend) =>
        _messageDiscoveredCivilizationFactory.Create().Show(civData, actWelcome, actOffend);

    public MessageStartGame GetMessageStartGame(CivilizationScriptable civData, Action actOK) =>
        _messageStartGameFactory.Create().Show(civData, actOK);

    public MessageBackMainMenu GetMessageBackMainMenu(Action actYes, Action actNo) =>
        _messageBackMainMenuFactory.Create().Show(actYes, actNo);
}
