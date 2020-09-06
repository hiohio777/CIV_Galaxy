using System;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveredCivilization
{
    private int _chanceDiscoverAnotherCiv, _countDiscoveredCiv;
    private MessageDiscoveredCivilization.Factory _factoryMessageDiscovered;

    public DiscoveredCivilization(MessageDiscoveredCivilization.Factory factoryMessageDiscovered)
    {
        this._factoryMessageDiscovered = factoryMessageDiscovered;
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
                Debug.Log("welcome!");
                anotherCiv.Open();
            };

            Action offend = () =>
            {
                Debug.Log("offend!");
                anotherCiv.Open();
            };

            _factoryMessageDiscovered.Create().Show(anotherCiv.CivData, welcome, offend);
            return true;
        }

        return false;
    }
}

