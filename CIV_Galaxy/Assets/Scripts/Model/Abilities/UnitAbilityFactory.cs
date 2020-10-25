using UnityEngine;

public class UnitAbilityFactory : BaseFactory
{
    [SerializeField] private UnitAbility prefab;

    private IGalaxyUITimer _galaxyUITimer;

    private void Start()
    {
        this._galaxyUITimer = GetRegisterObject<IGalaxyUITimer>();
    }

    public IUnitAbility GetNewUnit(AttackerAbility ability, ICivilization civilization, ICivilization civilizationTarget)
    {
        UnitAbility unit;

        if (buffer.Count > 0) unit = buffer.Pop() as UnitAbility;
        else
        {
            unit = InstantiateObject(prefab);
            unit.Creat(_galaxyUITimer, Buffered);
        }

        TypeDisplayAbilityEnum type;
        if (civilization is ICivilizationPlayer)
        {
            type = TypeDisplayAbilityEnum.PlayerAttack;
        }
        else if (civilizationTarget is ICivilizationPlayer)
        {
            type = TypeDisplayAbilityEnum.PlayerTarget;
        }
        else
        {
            type = TypeDisplayAbilityEnum.Al;
        }

        return unit.Initialize(ability, civilization.PositionCiv, civilizationTarget, type);
    }
}

public enum TypeDisplayAbilityEnum
{
    PlayerAttack,
    PlayerTarget,
    Al
}
