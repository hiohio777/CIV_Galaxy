using System.Collections.Generic;

public class AbilityFactory
{
    private UnitAbilityFactory _unitAbilityFactor;
    private PlanetsFactory _planetsFactory;

    public AbilityFactory(PlanetsFactory planetsFactory, UnitAbilityFactory unitAbilityFactor)
    {
        this._planetsFactory = planetsFactory;
        this._unitAbilityFactor = unitAbilityFactor;
    }

    public List<IAbility> GetAbilities(ICivilization civilization)
    {
        // Создать и инициировать абилки
        var abilities = new List<IAbility>();
        int id = 0;
        foreach (var item in civilization.DataBase.Abilities)
        {
            var prefab = UnityEngine.Object.Instantiate(item).GetComponent<IAbility>();
            prefab.Initialize(id, civilization);

            // Инициировать в зависимости от типа абилки
            switch (prefab)
            {
                case AbilityFleet ability: ability.Initialize(_planetsFactory, _unitAbilityFactor); break;
            }

            abilities.Add(prefab);

            id++;
        }

        if (abilities.Count > 0)
            abilities[0].IsActive = true;

        return abilities;
    }
}