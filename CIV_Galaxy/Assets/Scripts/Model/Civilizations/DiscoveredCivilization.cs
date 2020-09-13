using System;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveredCivilization
{
    private int _chanceDiscoverAnotherCiv, _countDiscoveredCiv;
    private MessageDiscoveredCivilization _messageDiscovered;
    private PlanetsFactory _planetsFactory;

    public DiscoveredCivilization(MessageDiscoveredCivilization messageDiscovered, PlanetsFactory planetsFactory)
    {
        (this._messageDiscovered, _planetsFactory) = (messageDiscovered, planetsFactory);
    }

    // Попытаться открыть иную цивилизацию, если есть неоткрытые(цивилизации открываются по порядку с 1-вой)
    public bool DiscoverAnotherCiv(List<ICivilization> anotherCivilization)
    {
        if (_countDiscoveredCiv >= anotherCivilization.Count)
            return false; // все цивилизации открыты

        _chanceDiscoverAnotherCiv += 20; // Увеличить шанс открытия цивилизации
        if (UnityEngine.Random.Range(0, 100) < _chanceDiscoverAnotherCiv)
        {
            // Открыть новую цивилизацию
            var anotherCiv = anotherCivilization[_countDiscoveredCiv] as ICivilizationAl;

            _chanceDiscoverAnotherCiv = 0;
            _countDiscoveredCiv++;

            Action welcome = () =>
            {
                anotherCiv.Open();
            };

            Action offend = () =>
            {
                anotherCiv.Open();
            };

            _messageDiscovered.Show(anotherCiv.CivDataBase, welcome, offend);
            return true;
        }

        return false;
    }
}

