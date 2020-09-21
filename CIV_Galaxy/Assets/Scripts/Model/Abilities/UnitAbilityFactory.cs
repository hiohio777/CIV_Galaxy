using UnityEngine;

public class UnitAbilityFactory : UnitBaseFactory
{
    private readonly UnitAbility.Factory factory;

    public UnitAbilityFactory(UnitAbility.Factory factory)
    {
        this.factory = factory;
    }

    public IUnitAbility GetNewUnit(AbilityBase ability, Vector3 startPosition, TypeCivEnum typeCiv, TypeCivEnum typeCivTarget)
    {
        UnitAbility unit;

        if (buffer.Count > 0) unit = buffer.Pop() as UnitAbility;
        else unit = factory.Create(Buffered);

        TypeDisplayAbilityEnum type;
        if (typeCiv == TypeCivEnum.Player)
        {
            type = TypeDisplayAbilityEnum.PlayerAttack;
        }
        else if (typeCivTarget == TypeCivEnum.Player)
        {
            type = TypeDisplayAbilityEnum.PlayerTarget;
        }
        else
        {
            type = TypeDisplayAbilityEnum.Al;
        }

        unit.Initialize(ability, startPosition, type);


        return unit;
    }
}

public enum TypeDisplayAbilityEnum
{
    PlayerAttack,
    PlayerTarget, 
    Al
}
