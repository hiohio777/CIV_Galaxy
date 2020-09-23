using System.Collections.Generic;

public class AbilityFactory
{
    private UnitAbilityFactory _unitAbilityFactor;
    private PlanetsFactory _planetsFactory;
    private IGalaxyUITimer _galaxyUITimer;

    public AbilityFactory(PlanetsFactory planetsFactory, UnitAbilityFactory unitAbilityFactor, IGalaxyUITimer galaxyUITimer)
    {
        this._planetsFactory = planetsFactory;
        this._unitAbilityFactor = unitAbilityFactor;
        this._galaxyUITimer = galaxyUITimer;
    }

    public List<IAbility> GetAbilities(ICivilization civilization)
    {
        // Создать и инициировать абилки
        var abilities = new List<IAbility>();
        int id = 0;
        foreach (var item in civilization.DataBase.Abilities)
        {
            var prefab = UnityEngine.Object.Instantiate(item).GetComponent<IAbility>();
            prefab.Initialize(id, civilization, _galaxyUITimer);

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