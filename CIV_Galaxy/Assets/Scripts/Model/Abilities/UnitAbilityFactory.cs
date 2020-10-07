public class UnitAbilityFactory : BaseFactory
{
    private readonly UnitAbility.Factory factory;

    public UnitAbilityFactory(UnitAbility.Factory factory)
    {
        this.factory = factory;
    }

    public IUnitAbility GetNewUnit(AttackerAbility ability, ICivilization civilization, ICivilization civilizationTarget)
    {
        UnitAbility unit;

        if (buffer.Count > 0) unit = buffer.Pop() as UnitAbility;
        else unit = factory.Create(Buffered);

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

        return unit.Initialize(ability, civilization.PositionCiv, type);
    }
}

public enum TypeDisplayAbilityEnum
{
    PlayerAttack,
    PlayerTarget,
    Al
}
