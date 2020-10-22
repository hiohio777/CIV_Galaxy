using System.Collections.Generic;

public class AbilityFactory : RegisterMonoBehaviour
{
    private UnitAbilityFactory _unitAbilityFactor;
    private PlanetsFactory _planetsFactory;

    public void Start()
    {
        this._planetsFactory = GetRegisterObject<PlanetsFactory>();
        this._unitAbilityFactor = GetRegisterObject<UnitAbilityFactory>(); 
    }

    public List<IAbility> GetAbilities(ICivilization civilization)
    {
        // Создать и инициировать абилки
        var abilities = new List<IAbility>();

        foreach (var item in civilization.DataBase.Abilities)
        {
            var prefab = UnityEngine.Object.Instantiate(item).GetComponent<IAbility>();
            prefab.Initialize(civilization);

            // Инициировать в зависимости от типа абилки
            switch (prefab)
            {
                case SpaceFleet ability: ability.Initialize(_planetsFactory, _unitAbilityFactor); break;
                case ScientificMission ability: ability.Initialize(_unitAbilityFactor); break;
                case Bombs ability: ability.Initialize(_unitAbilityFactor); break;
            }

            abilities.Add(prefab);
        }

        return abilities;
    }
}