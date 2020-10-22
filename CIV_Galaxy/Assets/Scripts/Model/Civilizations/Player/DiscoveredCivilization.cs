using System.Collections.Generic;

public class DiscoveredCivilization
{
    private int _chanceDiscoverAnotherCiv, _countDiscoveredCiv;
    private MessageFactory _messageFactory;

    public DiscoveredCivilization(MessageFactory messageFactory)
    {
        this._messageFactory = messageFactory;
    }

    // Попытаться открыть иную цивилизацию, если есть неоткрытые(цивилизации открываются по порядку с 1-вой)
    public bool DiscoverAnotherCiv(ICivilizationPlayer civilizationPlayer, List<ICivilizationAl> anotherCivilization)
    {
        if (_countDiscoveredCiv >= anotherCivilization.Count)
            return false; // все цивилизации открыты

        _chanceDiscoverAnotherCiv += 20; // Увеличить шанс открытия цивилизации
        if (UnityEngine.Random.Range(0, 101) < _chanceDiscoverAnotherCiv)
        {
            // Открыть новую цивилизацию
            var anotherCiv = anotherCivilization[_countDiscoveredCiv];

            _chanceDiscoverAnotherCiv = 0;
            _countDiscoveredCiv++;

            _messageFactory.GetMessageDiscoveredCivilization(anotherCiv.DataBase, anotherCiv.Open);
            return true;
        }

        return false;
    }
}

