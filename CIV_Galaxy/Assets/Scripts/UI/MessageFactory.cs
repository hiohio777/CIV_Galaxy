using System;

public class MessageFactory
{
    private MessageDiscoveredCivilization.Factory _messageDiscoveredCivilizationFactory;
    private MessageStartGame.Factory _messageStartGameFactory;
    private MessageBackMainMenu.Factory _messageBackMainMenuFactory;
    private EndGameUI.Factory _endGameUIFactory;

    public MessageFactory(MessageDiscoveredCivilization.Factory messageDiscoveredCivilizationFactory, 
        MessageStartGame.Factory messageStartGameFactory, 
        MessageBackMainMenu.Factory messageBackMainMenuFactory,
        EndGameUI.Factory endGameUIFactory)
    {
        _messageDiscoveredCivilizationFactory = messageDiscoveredCivilizationFactory ?? throw new ArgumentNullException(nameof(messageDiscoveredCivilizationFactory));
        _messageStartGameFactory = messageStartGameFactory ?? throw new ArgumentNullException(nameof(messageStartGameFactory));
        _messageBackMainMenuFactory = messageBackMainMenuFactory ?? throw new ArgumentNullException(nameof(messageBackMainMenuFactory));
        _endGameUIFactory = endGameUIFactory ?? throw new ArgumentNullException(nameof(endGameUIFactory));
    }

    public MessageDiscoveredCivilization GetMessageDiscoveredCivilization(CivilizationScriptable civData, Action actWelcome) =>
        _messageDiscoveredCivilizationFactory.Create().Show(civData, actWelcome);

    public MessageStartGame GetMessageStartGame(CivilizationScriptable civData, Action actOK = null) =>
        _messageStartGameFactory.Create().Show(civData, actOK);

    public MessageBackMainMenu GetMessageBackMainMenu(Action actYes = null, Action actNo = null) =>
        _messageBackMainMenuFactory.Create().Show(actYes, actNo);
    public EndGameUI GetEndGameUI() =>
        _endGameUIFactory.Create().Show();
}
